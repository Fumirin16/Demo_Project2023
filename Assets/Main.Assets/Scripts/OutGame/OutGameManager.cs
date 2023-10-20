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
    private FadeManager fadeSystem;
    //�uTranstionScene�v�̃C���X�^���X�𐶐�
    public TranstionScenes transSystem;

    [Space(10)]

    // �Q�[���I������UI�̐e�I�u�W�F�N�g�Ƃ��ĕۑ�����ϐ�
    private GameObject finishTag;
    // �Q�[����ʂ���Â��Ȃ�摜�̃R���|�[�l���g��ۑ�����ϐ�
    private Image splitImage;
    // �Q�[���I�����̃e�L�X�g����Â��Ȃ�摜�̃R���|�[�l���g��ۑ�����ϐ�
    private Image fadeImage;

    // �Q�[���I�[�o�[�A�Q�[���N���A��\������text���擾
    private TextMeshProUGUI titleText;
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

    //// �摜�̃t�F�[�h�̑�����ۑ�����ϐ�
    //[SerializeField, Range(0f, 10f)] private float _fadeOutSpeed = 0.1f;

    //// �e�L�X�g�̃t�F�[�h�̑�����ۑ�����ϐ�
    //[SerializeField, Range(0f, 10f)] private float _textOutSpeed=0.1f;

    private void Start()
    {
        Initi_UI();

        FadeVariables.Initi_Fade();

        VariablesController.Initi_Game();
    }

    private void Update()
    {
        // �Q�[���I�[�o�[�̎��̏���
        if (VariablesController.gameOverControl)
        {
            Direction_UI(overText, 4);
        }

        // �Q�[���N���A�̎��̏���
        if (VariablesController.gameClearControl)
        {
            Direction_UI(clearText, 3);
        }
    }

    // UI�̉��o�̏���������֐�
    private void Direction_UI(string textWord, int sceneNumber)
    {
        // �v���C���[�A�x�����̓������~�߂�
        DontMove_AntherScript();

        // �t�F�[�h�A�E�g�̉��o���Ăяo��
        fadeSystem.FadeOut(splitImage, splitImage.color.a);

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
        fadeSystem.FadeOut(fadeImage,fadeImage.color.a);

        // ��ʂ��Â��Ȃ����ꍇ
        if (FadeVariables.FadeOut)
        {
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

    // UI�̃I�u�W�F�N�g��R���|�[�l���g���擾����֐�
    void Initi_UI()
    {
        // �Q�[���I������UI�̐e�I�u�W�F�N�g�uFinishCanvas�v���^�O�����Ŏ擾����
        finishTag = GameObject.FindWithTag("FinishUI");

        // �uFinishCanvas�v�̎q�I�u�W�F�N�g�uui_SplitImage�v���w�肵�uImage�v�̃R���|�[�l���g���擾����
        splitImage = finishTag.transform.GetChild(0).GetComponentInChildren<Image>();

        //�uFinishCanvas�v�̎q�I�u�W�F�N�g�uui_TilteText�v���w�肵�uTextMeshProUGI�v�̃R���|�[�l���g���擾����
        titleText = finishTag.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();

        //�uFinishCanvas�v�̎q�I�u�W�F�N�g�uui_FadeImage�v���w�肵�uImage�v�̃R���|�[�l���g���擾����
        fadeImage = finishTag.transform.GetChild(2).GetComponentInChildren<Image>();
    }
}
