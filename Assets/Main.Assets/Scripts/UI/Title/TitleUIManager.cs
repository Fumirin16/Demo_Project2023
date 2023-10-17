using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// �^�C�g����ʂ̉��o�������L�q�����X�N���v�g
// �쐬�ҁF�R����

public class TitleUIManager : MonoBehaviour
{
    [Header("��ʕ\���̏���")]
    // �摜��\������Ԋu�̎��Ԃ�ۑ�����ϐ�
    [SerializeField, Range(0f, 10f)] private float[] _waitTime = new float[2];

    [Space(10)]

    //�uFadeSystem�v�̃C���X�^���X�𐶐��B
    [HideInInspector] public FadeManager _fadeSystem;

    //�uui_fadeImage�v�̃R���|�[�l���g��ۑ�����ϐ�
    private Image fadeImage;
    //�uui_titleImage�v�̃R���|�[�l���g��ۑ�����ϐ�
    private Image titleImage;
    // �uui_startButton�v�Ɓuui_endButton�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�����ϐ�
    private GameObject[] buttonObj = new GameObject[2];

    //�uTitleUI�v��e�I�u�W�F�N�g�Ƃ��ĕۑ�����ϐ�
    private GameObject parent;

    [Header("�X�^�[�g�{�^����̏���")]
    [SerializeField, Range(0f, 10f)] private float _moveSpeed = 1f;

    //�uTranstionScenes�v�̃C���X�^���X�𐶐�
    [HideInInspector] public TranstionScenes transSystem;

    // �J�����𓮂������߂ɁuMainCamera�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�����ϐ�
    private GameObject cameraObj;

    //�uTitleCanvas�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�����ϐ�
    private GameObject titleCanvas;

    // �J�����̏����ʒu��ۑ�����ϐ�
    private Vector3 _startPosition;
    // �J�����̈ړ���̈ʒu��ۑ�����ϐ�
    [SerializeField] private Vector3 _endPosition;

    // �u_distance�v�͏����ʒu�ƈړ���̋�����ۑ�����ϐ�
    // �u_positionValue�v�͂Q�_�Ԃ̈ړ�����ʒu�̒l��ۑ�����ϐ�
    // �utime�v�͌��݂̃Q�[�����Ԃ�ۑ�����ϐ�
    private float _distance, _positionValue, time;
    
    // �X�^�[�g�{�^���������ꂽ���̔����ۑ�����ϐ�
    private bool _isClickButton;

    // Start is called before the first frame update
    void Start()
    {
        // �^�C�g����ʂ�UI�̏�����
        Initi_TitleUI();

        // �X�^�[�g�{�^���������ꂽ�����֌W�̏����� 
        Initi_TransFunction();

        Debug.Log("_fadeSystem : " + _fadeSystem);
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�g����ʂ�UI�̉��o������R���[�`�����Ăяo��
        StartCoroutine("Fade_UI");

        if (_isClickButton)
        {
            Move_CameraObj();
        }
    }

    // �^�C�g����ʂ�UI�̉��o������R���[�`��
    private IEnumerator Fade_UI()
    {
        // ��Ԗڂɕ\�������鉉�o����
        if (!FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // �t�F�[�h�C��������֐����Ăяo��
            _fadeSystem.FadeIn(fadeImage, fadeImage.color.a);
        }

        // ��Ԗڂɕ\�������鉉�o����
        if (FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // ������҂�
            yield return new WaitForSeconds(_waitTime[0]);

            // �t�F�[�h�A�E�g������֐����Ăяo��
            _fadeSystem.FadeOut(titleImage, titleImage.color.a);
        }

        // �O�Ԗڂɕ\�������鉉�o����
        if (FadeVariables.FadeIn && FadeVariables.FadeOut)
        {
            // ������҂�
            yield return new WaitForSeconds(_waitTime[1]);

            // �{�^����\�������鏈��
            for (int i = 0; i < buttonObj.Length; i++)
            {
                buttonObj[i].SetActive(true);
            }
        }
    }
    void Initi_TransFunction()
    {
        // �J�����̏����ʒu��ϐ��ɕۑ�����
        _startPosition = cameraObj.transform.position;

        // �����ʒu�ƈړ���̈ʒu���m�̋����̒�����ϐ��ɕۑ�����
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // �X�^�[�g�{�^���������ꂽ����𖳌��ɂ���
        _isClickButton = false;
    }

    void Initi_TitleUI()
    {
        //�uTitleCanvas�v���^�O��������擾����
        titleCanvas = GameObject.Find("TitleCanvas").gameObject;

        //�uui_fadeImage�v�̃R���|�[�l���g���擾����
        fadeImage = GameObject.Find("FadeSystem/ui_fadeImage").GetComponentInChildren<Image>();

        // �^�C�g����ʂ�UI�̐e�I�u�W�F�N�g�uTitleUI�v���^�O��������擾����
        parent = GameObject.FindWithTag("TitleUI");

        // �uui_titleImage�v�̃R���|�[�l���g���擾����
        titleImage = parent.GetComponentInChildren<Image>();

        //�uui_startButton�v���Q�[���I�u�W�F�N�g���Ď擾����
        buttonObj[0] = parent.transform.GetChild(1).gameObject;

        //�uui_endButton�v���Q�[���I�u�W�F�N�g�Ƃ��Ď擾����
        buttonObj[1] = parent.transform.GetChild(2).gameObject;

        //�uMainCamera�v���Q�[���I�u�W�F�N�g�Ƃ��ĕۑ�����
        cameraObj = GameObject.Find("Main Camera").gameObject;

        // �{�^���̕\���𖳌��ɂ���
        for (int i = 0; i < buttonObj.Length; i++)
        {
            buttonObj[i].SetActive(false);
        }
    }

    public void OnClick_StartButton()
    {
        // �^�C�g����ʂ�UI�\�����\���ɂ���
        titleCanvas.SetActive(false);

        // �X�^�[�g�{�^���������ꂽ�����L���ɂ���
        _isClickButton = true;
        
        // ���݂̃Q�[�����̎��Ԃ�ϐ��ɕۑ�����
        time = Time.time;
    }

    private void Move_CameraObj()
    {
        // �����ʒu�ƈړ���̋����̊������v�Z���鏈��
        // �u(Time.time - time) / _distance�v�͋����̒�����100�Ƃ��Č��Ď��Ԍo�߂ŋ����̒��������邱�ƂłQ�_�̈ړ��������w�肷��l�����߂�B
        _positionValue = ((Time.time - time) / _distance) * _moveSpeed;

        // �J�����̈ʒu�𓮂�������
        cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // �J�����̈ʒu���w�肵���ʒu�ɗ����ꍇ
        if (cameraObj.transform.position == _endPosition)
        {
            // �X�^�[�g�{�^���������ꂽ����𖳌��ɂ���
            _isClickButton = false;

            // �`���[�g���A���̃V�[���ɑJ�ڂ���
            transSystem.Trans_Scene(1);
        }
    }
}
