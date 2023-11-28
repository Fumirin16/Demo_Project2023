using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �^�C�g����ʂ̉��o�������L�q�����X�N���v�g
// �쐬�ҁF�R����

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �摜��\������Ԋu�̎��Ԃ�ۑ�
    /// </summary>
    [SerializeField, Range(0f, 10f)] 
    private float[] _intervalTIme = new float[2];

    [Space(10)]

    /// <summary>
    /// �J�������ړ����鑬����ۑ�
    /// </summary>
    [SerializeField, Range(0f, 10f)] 
    private float _cameraMoveSpeed = 1f;
   
    /// <summary>
    /// �J�����̏����ʒu��ۑ�
    /// </summary>
    private Vector3 _startPosition;

    /// <summary>
    /// �J�����̈ړ���̈ʒu��ۑ�
    /// </summary>
    [SerializeField] 
    private Vector3 _endPosition;

    [Space(10)]

    /// <summary>
    /// �uFadeManager�v�̃C���X�^���X�𐶐�
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    /// �w�i�摜�̃t�F�[�h�A�E�g�̐ݒ�
    /// </summary>
    private FadeManager.FadeSetting _blackFadeIn;

    /// <summary>
    /// �^�C�g�����S�̃t�F�[�h�A�E�g�̐ݒ�
    /// </summary>
    private FadeManager.FadeSetting _logoFadeOut;

    /// <summary>
    /// �uTranstionScenes�v�̃C���X�^���X�𐶐�
    /// </summary>
    private TranstionScenes transSystem;

    /// <summary>
    /// �uui_startButton�v�Ɓuui_endButton�v���Q�[���I�u�W�F�N�g�Ƃ��Ď擾
    /// </summary>
    [SerializeField]
    private GameObject[] _buttonObj = new GameObject[2];

    /// <summary>
    /// �I������Ă��Ȃ��{�^�����Â��\�����邽�߂�Image���擾
    /// </summary>
    [SerializeField] 
    private GameObject[] _selectButtonImage = new GameObject[2];

    /// <summary>
    /// �J�����𓮂������߂ɁuMainCamera�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�
    /// </summary>
    [SerializeField] 
    private GameObject _cameraObj;

    /// <summary>
    /// �uTitleCanvas�v���Q�[���I�u�W�F�N�g�Ƃ��Ď擾
    /// </summary>
    [SerializeField]
    private GameObject _titleCanvas;

    /// <summary>
    /// �����ʒu�ƈړ���̋�����ۑ�
    /// </summary>
    private float _distance;

    /// <summary>
    /// �Q�_�Ԃ̈ړ�����ʒu�̒l��ۑ�
    /// </summary>
    private float _positionValue;

    /// <summary>
    /// ���݂̎��Ԃ�ۑ�����ϐ�
    /// </summary>
    private float _time;

    /// <summary>
    /// �X�^�[�g�{�^���������ꂽ���̔����ۑ�
    /// </summary>
    private bool _isClickButton = false;

    /// <summary>
    /// �{�^���̓��͂��󂯕t���锻���ۑ�
    /// </summary>
    private bool _isInputButton = false;

    /// <summary>
    /// �J�����𓮂�����ʂ��̔����ۑ�����ϐ�
    /// </summary>
    private bool _isStepScene = false;

    /// <summary>
    /// ���݂̑I�����Ă���I�u�W�F�N�g��ۑ�����ϐ�
    /// </summary>
    private GameObject _saveButton;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // �^�C�g����ʂ�UI�̏�����
        Initi_TitleUI();

        // �X�^�[�g�{�^���������ꂽ�����֌W�̏����� 
        Initi_TransFunction();
    }

    // Update is called once per frame
    void Update()
    {
        // �I�𒆂̃{�^���̏���ۑ�����
        _saveButton = EventSystem.current.currentSelectedGameObject;

        if (_saveButton == _buttonObj[0])
        {
            _selectButtonImage[0].SetActive(false);
            _selectButtonImage[1].SetActive(true);
        }
        if( _saveButton == _buttonObj[1])
        {
            _selectButtonImage[1].SetActive(false);
            _selectButtonImage[0].SetActive(true);
        }

        // �^�C�g����ʂ�UI�̉��o������R���[�`�����Ăяo��
        StartCoroutine("Fade_UI");

        // �{�^�����������ꍇ
        if (_isClickButton&& _isInputButton)
        {
            // �J�����𓮂���
            Move_CameraObj(1);
        }

        // ���{�^�����������ꍇ
        if (Input.GetKeyDown(KeyCode.JoystickButton3) )
        {
            // �r�d��炷
            AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

            // �J�����𓮂����锻��ɂ���
            _isStepScene = true;

            // ���݂̃Q�[�����̎��Ԃ�ϐ��ɕۑ�����
            _time = Time.time;

            // �^�C�g����ʂ�UI�\�����\���ɂ���
            _titleCanvas.SetActive(false);
        }

        // ���{�^�����������ꍇ�̃J�����ړ��̏���
        if (_isStepScene && _isInputButton)
        {
            Move_CameraObj(2);
        }
    }


    /// <summary>
    /// �^�C�g����ʂ�UI�̉��o������R���[�`��
    /// </summary>
    /// <returns> �҂����� </returns>
    private IEnumerator Fade_UI()
    {
        // ��Ԗڂɕ\�������鉉�o����
        if (!FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            // �t�F�[�h�C��������֐����Ăяo��
            _fadeSystem.FadeIn(_blackFadeIn);
        }

        // ��Ԗڂɕ\�������鉉�o����
        if (FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            // ������҂�
            yield return new WaitForSeconds(_intervalTIme[0]);

            if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.bgmAudioSource))
            {
                AudioManager.audioManager.Play_BGMSound(BGMSoundData.BGM.Title);
            }

            // �t�F�[�h�A�E�g������֐����Ăяo��
            _fadeSystem.FadeOut(_logoFadeOut);
        }

        // �O�Ԗڂɕ\�������鉉�o����
        if (FadeManager.fadeIn && FadeManager.fadeOut)
        {
            // ������҂�
            yield return new WaitForSeconds(_intervalTIme[1]);

            // �{�^����\�������鏈��
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
            }

            _isInputButton = true;
        }
    }

    /// <summary>
    /// �J�����ړ����o�֌W�̏������̊֐�
    /// </summary>
    void Initi_TransFunction()
    {
        // �J�����̏����ʒu��ϐ��ɕۑ�����
        _startPosition = _cameraObj.transform.position;

        // �����ʒu�ƈړ���̈ʒu���m�̋����̒�����ϐ��ɕۑ�����
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // �X�^�[�g�{�^���������ꂽ����𖳌��ɂ���
        _isClickButton = false;
    }

    /// <summary>
    /// UI���o�֌W�̏������̊֐�
    /// </summary>
    void Initi_TitleUI()
    {
        // �{�^���̕\���𖳌��ɂ���
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);

            _selectButtonImage[i].SetActive(false);
        }

        // �����ɑI����Ԃɂ��Ă����I�u�W�F�N�g��ݒ肷��
        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);
    }

    /// <summary>
    /// �X�^�[�g�{�^���������ꂽ���̏����̊֐�
    /// </summary>
    public void OnClick_StartButton()
    {
        AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

        // �^�C�g����ʂ�UI�\�����\���ɂ���
        _titleCanvas.SetActive(false);

        // �X�^�[�g�{�^���������ꂽ�����L���ɂ���
        _isClickButton = true;

        // ���݂̃Q�[�����̎��Ԃ�ϐ��ɕۑ�����
        _time = Time.time;
    }

    /// <summary>
    /// �G���h�{�^���������ꂽ���̏����̊֐�
    /// </summary>
    public void OnClick_EndButton()
    {
        AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            transSystem.Trans_EndGame();
        }
    }

    /// <summary>
    /// �J�����𓮂����֐�
    /// </summary>
    /// <param name="_seceneNumber"> �J�ڂ������V�[���̔ԍ� </param>
    private void Move_CameraObj(int _seceneNumber)
    {
        AudioManager.audioManager.Change_BGMVolume(0.01f);

        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            //AudioManager.audioManager.Play_SESound(SESoundData.SE.Audience);
            AudioManager.audioManager.Play_SESound(SESoundData.SE.Walk);
        }

        // �����ʒu�ƈړ���̋����̊������v�Z���鏈��
        // �u(Time.time - _time) / _distance�v�͋����̒�����100�Ƃ��Č��Ď��Ԍo�߂ŋ����̒��������邱�ƂłQ�_�̈ړ��������w�肷��l�����߂�B
        _positionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

        // �J�����̈ʒu�𓮂�������
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // �J�����̈ʒu���w�肵���ʒu�ɗ����ꍇ
        if (_cameraObj.transform.position == _endPosition)
        {
            // �X�^�[�g�{�^���������ꂽ����𖳌��ɂ���
            _isClickButton = false;

            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.seAudioSource);
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);

            // �`���[�g���A���̃V�[���ɑJ�ڂ���
            transSystem.Trans_Scene(_seceneNumber);
        }
    }

    #endregion ---Methods---
}
