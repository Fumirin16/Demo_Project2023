using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// �쐬�ҁF�R����
// �Q�[���N���A�Ɋւ���\�[�X�R�[�h

public class GameClearManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �uFadeManager�v���Q��
    /// </summary>
    private FadeManager _fadeManager;

    /// <summary>
    /// �uTranstionScenes�v���Q��
    /// </summary>
    private TranstionScenes _transSystem;

    /// <summary>
    /// �w�i�̃t�F�[�h�A�E�g�̐ݒ�
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeOut;

    /// <summary>
    /// �`�F�L�̃t�F�[�h�A�E�g�̐ݒ�
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _chekiFadeOut;

    /// <summary>
    /// ��ʏI�����̃t�F�[�h�A�E�g�̐ݒ�
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    /// �A�C�h���̉摜��ۑ�����ϐ�
    /// </summary>
    [SerializeField]
    private RectTransform[] _idolImage = new RectTransform[2];

    /// <summary>
    /// �A�C�h���摜�̏����ʒu��ۑ�����ϐ�
    /// </summary>
    private Vector2[] _startPosition = new Vector2[2];

    /// <summary>
    /// �A�C�h���摜�̈ړ����ۑ�����ϐ�
    /// </summary>
    [SerializeField]
    private  Vector2[] _endPosition = new Vector2[2];

    /// <summary>
    /// �A�C�h���摜�̏����ʒu�ƈړ���̋�����ۑ�����ϐ�
    /// </summary>
    private float[] _distance = new float[2];

    /// <summary>
    /// ���Ԃ�ۑ�����ϐ�
    /// </summary>
    private float _time;

    /// <summary>
    /// �A�C�h���摜�𓮂���������ۑ�����ϐ�
    /// </summary>
    [SerializeField,Range(0f, 100f)]
    private float _moveSpeed;

    /// <summary>
    /// �v���C���[�̉摜���擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// �J�E���g�_�E���̎��Ԃ�ۑ�����ϐ�
    /// </summary>
    [SerializeField,Range(0,10f)]
    private float _countDownTime;

    /// <summary>
    /// �J�E���g�_�E����\������e�L�X�g�I�u�W�F�N�g���擾����ϐ�
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _countText;

    /// <summary>
    /// �J�E���g�_�E���̎��Ԃ�int�^�ŕۑ�����ϐ�
    /// </summary>
    private int _uiCount;

    /// <summary>
    /// �I�Ղɕ\������e�L�X�g�̃I�u�W�F�N�g���擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _lastText;

    /// <summary>
    /// �`�F�L���B������̑ҋ@���Ԃ�ۑ�����ϐ�
    /// </summary>
    [SerializeField,Range(0f,10f)]
    private float _changeSpeed;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        Initi_UI();
    }

    // Update is called once per frame
    void Update()
    {
        direction_UI();
    }

    /// <summary>
    /// �����̊֐�
    /// </summary>
    private void Initi_UI()
    {
        // �A�C�h���̈ʒu��������
        for (int i = 0; i < _idolImage.Length; i++)
        {
            _startPosition[i] = _idolImage[i].anchoredPosition;
            _distance[i] = Vector2.Distance(_startPosition[i], _endPosition[i]);
        }

        // �J�E���g�_�E����������
        _uiCount = 0;
    }

    /// <summary>
    /// UI�̉��o�֐�
    /// </summary>
    private void direction_UI()
    {
        switch (_uiCount)
        {
            // �w�i�摜���t�F�[�h����
            case 0: 
                _fadeManager.FadeOut(_backGroundFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _uiCount++;
                }
                break;
            
            // �`�F�L���t�F�[�h����
            case 1:               
                _fadeManager.FadeOut(_chekiFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _time = Time.time;
                    _uiCount++;
                }
                break;

            // �A�C�h���摜�����ɃX���C�h����
            case 2:               
                Move_IdolImage();
                break;

            // �v���C���[�摜��\������
            case 3:
                _playerImage.SetActive(true);
                _uiCount++;
                break;

            // �J�E���g�_�E����\������
            case 4:
                CountDown_Text();
                break;

            // �`�F�L���B�e����
            case 5:
                StartCoroutine(ShotPhoto());
                break;

            // ��ʏI�����̃t�F�[�h����
            case 6:
                _fadeManager.FadeOut(_endFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _transSystem.Trans_Scene(0);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �A�C�h���摜�𓮂����֐�
    /// </summary>
    private void Move_IdolImage()
    {
        float _positionValue;

        for (int i = 0; i < _idolImage.Length; i++)
        {
            // �����ʒu�ƈړ���̋����̊������v�Z���鏈��
            // �u(Time.time - time) / _distance�v�͋����̒�����100�Ƃ��Č��Ď��Ԍo�߂ŋ����̒��������邱�ƂłQ�_�̈ړ��������w�肷��l�����߂�B
            _positionValue = ((Time.time - _time) / _distance[i]) * _moveSpeed;

            // �A�C�h���摜�̈ʒu�𓮂�������
            _idolImage[i].anchoredPosition = Vector2.Lerp(_startPosition[i], _endPosition[i], _positionValue);

            // �A�C�h���摜�̈ʒu���w�肵���ʒu�ɗ����ꍇ
            if ((_idolImage[0].anchoredPosition == _endPosition[0]) && (_idolImage[1].anchoredPosition == _endPosition[1]))
            {
                _lastText.SetActive(false);
                _uiCount++;
            }
        }
    }

    /// <summary>
    /// �J�E���g�_�E�������o����֐�
    /// </summary>
    private void CountDown_Text()
    {
        // ���Ԃ�ۑ�����ϐ�
        int _countDownText;

        // ���Ԃ�ۑ�����
        _countDownTime -= Time.deltaTime;
        
        // �����t���̎��Ԃ𐮐��ɕϊ�����
        _countDownText = (int)_countDownTime;

        // �J�E���g�_�E���̎��Ԃ�\��
        _countText.text = (_countDownText + 1).ToString();

        // �J�E���g�_�E���̎��Ԃ��O��菬�����Ȃ����ꍇ
        if ((int)_countDownTime < 0)
        {
            _countText.enabled = false;
            _uiCount++;
        }
    }

    /// <summary>
    /// �`�F�L���B�鉉�o�̊֐�
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotPhoto()
    {
        // SE�����Ă��Ȃ������ꍇ
        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            AudioManager.audioManager.Play_SESound(SESoundData.SE.Shutters);
        }

        // �҂�����
        yield return new WaitForSeconds(_changeSpeed);

        // �e�L�X�g��\������
        _lastText.SetActive(true);

        // UI�̃J�E���g��i�߂�
        if (_uiCount == 5)
        {
            _uiCount++;
        }
        
        // �I��
        yield return null;
    }

    #endregion ---Methods---
}
