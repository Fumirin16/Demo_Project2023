using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OutGameManager : MonoBehaviour
{
    // �t�F�[�h�A�E�g������p�l������mage�Ƃ��Ď擾
    [SerializeField] Image fadePanel;

    [Space(6)]

    // �t�F�[�h�A�E�g���铧���x�̏����ۑ�����ϐ�
    [Range(0f, 1f)] public float constAlpha;
    // �t�F�[�h�A�E�g������X�s�[�h��ۑ�����ϐ�
    [Range(0f, 1f)] public float fadeSpeed;

    [Space(10)]

    // �Q�[���I�[�o�[�A�Q�[���N���A��\������text���擾
    [SerializeField] TextMeshProUGUI titleText;

    [Space(6)]

    // �Q�[���I�[�o�[�̎��ɕ\�������镶����ۑ�����ϐ�
    public string overText;
    // �Q�[���N���A�̎��ɕ\�������镶����ۑ�����ϐ�
    public string clearText;

    [Space(10)]

    // �v���C���[�̈ړ��ƃJ�����𓮂����Ă���script���Q�Ƃ��邽�߁A�I�u�W�F�N�g���擾
    public GameObject[] playerDontMove=new GameObject[2];
    
    // �x�����̈ړ����~�߂邽�߁A�I�u�W�F�N�g���擾
    public GameObject guardDontMove;

    // �t�F�[�h�A�E�g������p�l���̓����x��ۑ�����ϐ�
    private float _imageAlpha;

    private void Start()
    {
        // ������
        fadePanel.color = new Color(0f, 0f, 0f, 0f);
        fadePanel.enabled = false;
        _imageAlpha = 0.0f;
    }

    private void Update()
    {
        // �Q�[���I�[�o�[�̎��̏���
        if (VariablesController.gameOverControl)
        {
            // �v���C���[�A�x�����̓������~�߂�
            DontMove_AntherScript();

            // �t�F�[�h�A�E�g������p�l����\������
            fadePanel.enabled = true;

            // �����x�����Z���ďグ��
            _imageAlpha += fadeSpeed;
            Debug.Log("_imageAlpha : " + _imageAlpha);

            // �p�l���ɓ����x��ݒ肷�� 
            fadePanel.color = new Color(0f, 0f, 0f, _imageAlpha);

            // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
            if (_imageAlpha >= constAlpha)
            {
                // �p�l���̓����x���Œ肷��
                _imageAlpha = constAlpha;

                // �Q�[���I�[�o�[��text��\������
                Display_TitleText(overText);
            }
        }

        // �Q�[���N���A�̎��̏���
        if (VariablesController.gameClearControl)
        {
            // �v���C���[�A�x�����̓������~�߂�
            DontMove_AntherScript();

            // �t�F�[�h�A�E�g������p�l����\������
            fadePanel.enabled = true;

            // �����x�����Z���ďグ��
            _imageAlpha += fadeSpeed;
            Debug.Log("_imageAlpha : " + _imageAlpha);

            // �p�l���ɓ����x��ݒ肷�� 
            fadePanel.color = new Color(0f, 0f, 0f, _imageAlpha);

            // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
            if (_imageAlpha >= constAlpha)
            {
                // �p�l���̓����x���Œ肷��
                _imageAlpha = constAlpha;

                // �Q�[���I�[�o�[��text��\������
                Display_TitleText(clearText);
            }
        }

        // debug
        if (Input.GetKeyDown(KeyCode.M))
        {
            VariablesController.gameOverControl = true;
            Debug.Log("�Q�[���I�[�o�[������");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            VariablesController.gameOverControl = false;
            fadePanel.enabled = false;
            _imageAlpha = 0.0f;
        }
    }

    // �t�F�[�h�A�E�g�̉��o������֐�
    private void FadeOut()
    {
        // �t�F�[�h�A�E�g������p�l����\������
        fadePanel.enabled = true;

        // �����x�����Z���ďグ��
        _imageAlpha += fadeSpeed;

        // �t�F�[�h�A�E�g������p�l���̓����x��ݒ肷��
        fadePanel.color = new Color(0f, 0f, 0f, _imageAlpha);

        // �p�l���̓����x���w�肵�������x�̒l�ɂȂ������̏���
        if (_imageAlpha >= constAlpha)
        {
            // �p�l���̓����x���Œ肷��
            _imageAlpha = constAlpha;
        }
    }

    // �X�v���b�g�C���̉��o������֐�
    private void SplitIn()
    {

    }

    // text��\������֐�
    private void Display_TitleText(string textWord)
    {
        titleText.text = textWord;
    }

    // �v���C���[�̈ړ��ƃJ�����̓����A�x�����̈ړ����~�߂鏈���̊֐�
    private void DontMove_AntherScript()
    {
        // �v���C���[�̈ړ���script�𖳌��ɂ���
        playerDontMove[0].GetComponent<MocopiAvatar>().enabled = false;

        // �v���C���[�̃J������script�𖳌��ɂ���
        playerDontMove[1].GetComponent<CameraController>().enabled = false;

        // �x�����̈ړ���script�𖳌��ɂ���
        guardDontMove.GetComponent<AroundGuardsmanController>().enabled = false;

        // �x�����̈ړ���NavMeshAgent�𖳌��ɂ���
        guardDontMove.GetComponent<NavMeshAgent>().enabled = false;

        // �x�����̈ړ���Animator�𖳌��ɂ���
        guardDontMove.GetComponent<Animator>().enabled = false;
    }
}
