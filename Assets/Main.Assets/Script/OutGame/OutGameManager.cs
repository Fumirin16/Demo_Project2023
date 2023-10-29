using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// �Q�[���I�[�o�[��Q�[���N���A�̔��肪�L���ɂȂ����ꍇ�ɉ��o������\�[�X�R�[�h
// �쐬�ҁF�R����
public class VariablesController
{
    // �Q�[���I�[�o�[�̔�����Ǘ�����ϐ�
    public static bool gameOverControl;

    // �Q�[���N���A�̔�����Ǘ�����ϐ�
    public static bool gameClearControl;

    public static void Initi_Game()
    {
        gameOverControl = false;
        gameClearControl = false;
    }
}

public class OutGameManager : MonoBehaviour
{
    //�uFadeSystem�v�̃C���X�^���X�𐶐�
    private FadeManager fadeSystem=new FadeManager();
    //�uTranstionScene�v�̃C���X�^���X�𐶐�
    public TranstionScenes transSystem;

    [Space(10)]

    // �Q�[���I������UI�̐e�I�u�W�F�N�g�Ƃ��ĕۑ�����ϐ�
    //[SerializeField]private GameObject finishTag;
    // �Q�[����ʂ���Â��Ȃ�摜�̃R���|�[�l���g��ۑ�����ϐ�
    [SerializeField] private Image splitImage;
    // �Q�[���I�����̃e�L�X�g����Â��Ȃ�摜�̃R���|�[�l���g��ۑ�����ϐ�
    [SerializeField] private Image fadeImage;

    // �Q�[���I�[�o�[�A�Q�[���N���A��\������text���擾
    [SerializeField] private TextMeshProUGUI titleText;
    // �Q�[���I�[�o�[�̎��ɕ\�������镶����ۑ�����ϐ�
    [SerializeField]private string overText;
    // �Q�[���N���A�̎��ɕ\�������镶����ۑ�����ϐ�
    [SerializeField] private string clearText;

    [Space(10)]

    // �v���C���[�̈ړ��ƃJ�����𓮂����Ă���script���Q�Ƃ��邽�߁A�I�u�W�F�N�g���擾
    [SerializeField]private GameObject[] playerDontMove=new GameObject[2];

    // �x�����̈ړ����~�߂邽�߁A�I�u�W�F�N�g���擾
    [SerializeField]private GameObject guardDontMove;

    [Space(10)]

    // �Q�[���I�����̃e�L�X�g����Â��Ȃ�܂ł̑҂����Ԃ�ۑ�����ϐ�
    [SerializeField, Range(0f, 10f)] private int _waitTime = 3;

    // �摜�̃t�F�[�h�̑�����ۑ�����ϐ�
    [SerializeField, Range(0f, 10f)] private float _fadeOutSpeed = 0.1f;

    // �e�L�X�g�̃t�F�[�h�̑�����ۑ�����ϐ�
    [SerializeField, Range(0f, 10f)] private float _textOutSpeed=0.1f;

    [SerializeField, Range(0f, 1f)] private float _fadeImageAlpha;

    private void Start()
    {
        FadeVariables.Initi_Fade();

        VariablesController.Initi_Game();
    }

    private void Update()
    {
        // �Q�[���I�[�o�[�̎��̏���
        if (VariablesController.gameOverControl)
        {
            AudioManager.Instance.Stop_Sound(AudioManager.Instance.bgmAudioSource);
            Direction_UI(overText, 4);
        }

        // �Q�[���N���A�̎��̏���
        if (VariablesController.gameClearControl)
        {
            AudioManager.Instance.Stop_Sound(AudioManager.Instance.bgmAudioSource);
            Direction_UI(clearText, 3);
        }
    }

    // UI�̉��o�̏���������֐�
    private void Direction_UI(string textWord, int sceneNumber)
    {
        // �v���C���[�A�x�����̓������~�߂�
        DontMove_AntherScript();

        Debug.Log("�������߂�");

        // �t�F�[�h�A�E�g�̉��o���Ăяo��
        fadeSystem.FadeOut(splitImage, splitImage.color.a,_fadeOutSpeed, _defaultValue: _fadeImageAlpha);

        // �t�F�[�h�A�E�g���I������ꍇ
        if (FadeVariables.FadeOut)
        {
            // �e�L�X�g��\�����Ă���̃V�[���J�ڂ�����R���[�`�����Ăяo��
            StartCoroutine(Display_TitleText(textWord, sceneNumber));
        }
    }

    // �e�L�X�g��\�����Đ��b�o�������ʂ��Â����鉉�o�̃R���[�`��
    private IEnumerator Display_TitleText(string textWord,int sceneNumber)
    {
        // �w�肵���e�L�X�g��\��
        titleText.text = textWord;

        //�uFadeOut�v�̔���𖳌��ɂ���
        FadeVariables.FadeOut = false;

        // �w�肵���b����҂�
        yield return new WaitForSeconds(_waitTime);

        //�uFadeOut�v���Ăяo���B
        fadeSystem.FadeOut(fadeImage, fadeImage.color.a, _textOutSpeed);

        // ��ʂ��Â��Ȃ����ꍇ
        if (FadeVariables.FadeOut)
        {
            Debug.Log("fadeOut");
            // �w�肵���ԍ��̃V�[���ɑJ�ڂ���
            transSystem.Trans_Scene(sceneNumber);
        }
    }

    // �v���C���[�̈ړ��ƃJ�����̓����A�x�����̈ړ����~�߂鏈���̊֐�
    private void DontMove_AntherScript()
    {
        // �v���C���[�̈ړ���script�𖳌��ɂ���
        playerDontMove[0].GetComponent<MocopiAvatar>().enabled = false;

        // �v���C���[�̃R���g���[���[�̈ړ��p��script�𖳌��ɂ���
        playerDontMove[0].GetComponent<PlayerController>().enabled = false;

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
