using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �^�C�g����ʂ̉��o�������L�q�����X�N���v�g
// �쐬�ҁF�R����

public class TitleUIManager : MonoBehaviour
{
    // �摜��\������Ԋu�̎��Ԃ�ۑ�
    [SerializeField, Range(0f, 10f)] private float[] _intervalTIme = new float[2];

    [Space(10)]

    // �J�������ړ����鑬����ۑ�
    [SerializeField, Range(0f, 10f)] private float _cameraMoveSpeed = 1f;
   
    // �J�����̏����ʒu��ۑ�
    private Vector3 _startPosition;
    // �J�����̈ړ���̈ʒu��ۑ�
    [SerializeField] private Vector3 _endPosition;

    [Space(10)]

    //// �t�F�[�h�C���̑�����ۑ�
    //[SerializeField, Range(0f, 10f)] private float _fadeSpeed = 0.1f;

    //// �t�F�[�h�A�E�g�̑�����ۑ�
    //[SerializeField, Range(0f, 10f)] private float _logoFadeSpeed = 0.1f;


    //�uFadeSystem�v�̃C���X�^���X�𐶐�
    private FadeManager _fadeSystem ;
    //�uTranstionScenes�v�̃C���X�^���X�𐶐�
    [HideInInspector] public TranstionScenes transSystem;

    //�uui_fadeImage�v�̃R���|�[�l���g��ۑ�
    private Image _fadeImage;
    //�uui_titleImage�v�̃R���|�[�l���g��ۑ�
    private Image _titleImage;
    // �uui_startButton�v�Ɓuui_endButton�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�
    private GameObject[] _buttonObj = new GameObject[2];
    //�uTitleUI�v��e�I�u�W�F�N�g�Ƃ��ĕۑ�
    private GameObject _parent;
    // �J�����𓮂������߂ɁuMainCamera�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�
    private GameObject _cameraObj;
    //�uTitleCanvas�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�
    private GameObject _titleCanvas;

    // �u_distance�v�͏����ʒu�ƈړ���̋�����ۑ�
    // �u_positionValue�v�͂Q�_�Ԃ̈ړ�����ʒu�̒l��ۑ�
    // �u&& _isInputButton�v�͌��݂̃Q�[�����Ԃ�ۑ�
    private float _distance, _positionValue, _time;

    // �X�^�[�g�{�^���������ꂽ���̔����ۑ�
    private bool _isClickButton = false;

    // �{�^���̓��͂��󂯕t���锻���ۑ�
    private bool _isInputButton = false;

    private bool _isStepScene = false;

    private GameObject _saveButton;

    // Start is called before the first frame update
    void Start()
    {
        // �^�C�g����ʂ�UI�̏�����
        Initi_TitleUI();

        // �X�^�[�g�{�^���������ꂽ�����֌W�̏����� 
        Initi_TransFunction();

        FadeVariables.Initi_Fade();
    }

    // Update is called once per frame
    void Update()
    {
        // �I�𒆂̃{�^���̏���ۑ�����
        _saveButton = EventSystem.current.currentSelectedGameObject;

        // �^�C�g����ʂ�UI�̉��o������R���[�`�����Ăяo��
        StartCoroutine("Fade_UI");

        if (_isClickButton&& _isInputButton)
        {
            Move_CameraObj(1);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) )
        {
            _isStepScene = true;

            // ���݂̃Q�[�����̎��Ԃ�ϐ��ɕۑ�����
            _time = Time.time;

            // �^�C�g����ʂ�UI�\�����\���ɂ���
            _titleCanvas.SetActive(false);
        }

        if (_isStepScene && _isInputButton)
        {
            Move_CameraObj(2);
        }
    }


    // �^�C�g����ʂ�UI�̉��o������R���[�`��
    private IEnumerator Fade_UI()
    {
        // ��Ԗڂɕ\�������鉉�o����
        if (!FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // �t�F�[�h�C��������֐����Ăяo��
            _fadeSystem.FadeIn(_fadeImage, _fadeImage.color.a);
        }

        // ��Ԗڂɕ\�������鉉�o����
        if (FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // ������҂�
            yield return new WaitForSeconds(_intervalTIme[0]);

            // �t�F�[�h�A�E�g������֐����Ăяo��
            _fadeSystem.FadeOut(_titleImage, _titleImage.color.a);
        }

        // �O�Ԗڂɕ\�������鉉�o����
        if (FadeVariables.FadeIn && FadeVariables.FadeOut)
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
    void Initi_TransFunction()
    {
        // �J�����̏����ʒu��ϐ��ɕۑ�����
        _startPosition = _cameraObj.transform.position;

        // �����ʒu�ƈړ���̈ʒu���m�̋����̒�����ϐ��ɕۑ�����
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // �X�^�[�g�{�^���������ꂽ����𖳌��ɂ���
        _isClickButton = false;
    }

    void Initi_TitleUI()
    {
        //�uTitleCanvas�v���^�O��������擾����
        _titleCanvas = GameObject.Find("TitleCanvas").gameObject;

        //�uui_fadeImage�v�̃R���|�[�l���g���擾����
        _fadeImage = GameObject.Find("ui_fadeImage").GetComponent<Image>();

        // �^�C�g����ʂ�UI�̐e�I�u�W�F�N�g�uTitleUI�v���^�O��������擾����
        _parent = GameObject.FindWithTag("TitleUI");

        // �uui_titleImage�v�̃R���|�[�l���g���擾����
        _titleImage = _parent.GetComponentInChildren<Image>();

        //�uui_startButton�v���Q�[���I�u�W�F�N�g���Ď擾����
        _buttonObj[0] = _parent.transform.GetChild(1).gameObject;

        //�uui_endButton�v���Q�[���I�u�W�F�N�g�Ƃ��Ď擾����
        _buttonObj[1] = _parent.transform.GetChild(2).gameObject;

        //�uMainCamera�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�����
        _cameraObj = GameObject.Find("Main Camera").gameObject;

        // �{�^���̕\���𖳌��ɂ���
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);
    }

    public void OnClick_StartButton()
    {
        // �^�C�g����ʂ�UI�\�����\���ɂ���
        _titleCanvas.SetActive(false);

        // �X�^�[�g�{�^���������ꂽ�����L���ɂ���
        _isClickButton = true;

        // ���݂̃Q�[�����̎��Ԃ�ϐ��ɕۑ�����
        _time = Time.time;
    }

    private void Move_CameraObj(int _seceneNumber)
    {
        // �����ʒu�ƈړ���̋����̊������v�Z���鏈��
        // �u(&& _isInputButton.&& _isInputButton - && _isInputButton) / _distance�v�͋����̒�����100�Ƃ��Č��Ď��Ԍo�߂ŋ����̒��������邱�ƂłQ�_�̈ړ��������w�肷��l�����߂�B
        _positionValue = ((Time.time-_time) / _distance) * _cameraMoveSpeed;

        // �J�����̈ʒu�𓮂�������
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // �J�����̈ʒu���w�肵���ʒu�ɗ����ꍇ
        if (_cameraObj.transform.position == _endPosition)
        {
            // �X�^�[�g�{�^���������ꂽ����𖳌��ɂ���
            _isClickButton = false;

            // �`���[�g���A���̃V�[���ɑJ�ڂ���
            transSystem.Trans_Scene(_seceneNumber);
        }
    }
}
