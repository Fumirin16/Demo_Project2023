using Cinemachine;
using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// �Q�[���I�[�o�[��Q�[���N���A�̔��肪�L���ɂȂ����ꍇ�ɉ��o������\�[�X�R�[�h
// �쐬�ҁF�R����

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �Q�[���I�[�o�[�̔����ۑ�����ϐ�
    /// </summary>
    public static bool gameOver;

    /// <summary>
    /// �Q�[���N���A�̔����ۑ�����ϐ�
    /// </summary>
    public static bool gameClear;

    /// <summary>
    /// �uFadeSystem�v�̃C���X�^���X�𐶐�
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    /// �Q�[���N���A�E�I�[�o�[��ʂɓ��������̃t�F�[�h�A�E�g��ݒ�
    /// </summary>
    private FadeManager.FadeSetting _blackFadeOut;

    /// <summary>
    /// ��ʏI�����̃t�F�[�h�A�E�g�̐ݒ�
    /// </summary>
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    /// �uTranstionScene�v�̃C���X�^���X�𐶐�
    /// </summary>
    private TranstionScenes _transSystem;

    /// <summary>
    /// �Q�[���I�[�o�[�A�Q�[���N���A��\������text���擾
    /// </summary>
    [SerializeField] 
    private TextMeshProUGUI _logoText;

    /// <summary>
    /// �Q�[���I�[�o�[�̎��ɕ\�������镶����ۑ�����ϐ�
    /// </summary>
    [SerializeField]
    private string _overText;

    /// <summary>
    /// �Q�[���N���A�̎��ɕ\�������镶����ۑ�����ϐ�
    /// </summary>
    [SerializeField] 
    private string _clearText;

    [Space(10)]

    /// <summary>
    /// �v���C���[�̃R���|�[�l���g���Q�Ƃ��邽�߁A�I�u�W�F�N�g���擾
    /// </summary>
    [SerializeField]
    private GameObject _playerObj;

    /// <summary>
    /// �J�����̃R���|�[�l���g���Q�Ƃ��邽�߁A�I�u�W�F�N�g���擾
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    /// �x�����̃R���|�[�l���g���Q�Ƃ��邽�߁A�I�u�W�F�N�g���擾
    /// </summary>
    [SerializeField]
    private GameObject _guardObj;

    [Space(10)]

    // �Q�[���I�����̃e�L�X�g����Â��Ȃ�܂ł̑҂����Ԃ�ۑ�����ϐ�
    [SerializeField, Range(0f, 10f)]
    private int _waitTime = 3;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {

    }

    private void Update()
    {
        // �Q�[���I�[�o�[�̎��̏���
        if (gameOver)
        {
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);
            Direction_UI(_overText, 4);
        }

        // �Q�[���N���A�̎��̏���
        if (gameClear)
        {
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);
            Direction_UI(_clearText, 3);
        }
    }

    /// <summary>
    /// UI�̉��o�̏���������֐�
    /// </summary>
    /// <param name="textWord"> �\������e�L�X�g </param>
    /// <param name="sceneNumber"> �J�ڂ������V�[���̔ԍ� </param>
    /// <returns> �҂����� </returns>
    private IEnumerator Direction_UI(string textWord, int sceneNumber)
    {
        // �v���C���[�A�x�����̓������~�߂�
        DontMove_AntherScript();

        // �t�F�[�h�A�E�g�̉��o���Ăяo��
        _fadeSystem.FadeOut(_blackFadeOut);

        // �t�F�[�h�A�E�g���I������ꍇ
        if (FadeManager.fadeOut)
        {
            // �e�L�X�g��\�����Ă���̃V�[���J�ڂ�����R���[�`�����Ăяo��
            // �w�肵���e�L�X�g��\��
            _logoText.text = textWord;

            //�uFadeOut�v�̔���𖳌��ɂ���
            FadeManager.fadeOut = false;

            // �w�肵���b����҂�
            yield return new WaitForSeconds(_waitTime);

            //�uFadeOut�v���Ăяo���B
            _fadeSystem.FadeOut(_endFadeOut);

            // ��ʂ��Â��Ȃ����ꍇ
            if (FadeManager.fadeOut)
            {
                // �w�肵���ԍ��̃V�[���ɑJ�ڂ���
                _transSystem.Trans_Scene(sceneNumber);
            }
        }
    }

    /// <summary>
    /// �v���C���[�̈ړ��ƃJ�����̓����A�x�����̈ړ����~�߂鏈���̊֐�
    /// </summary>
    private void DontMove_AntherScript()
    {
        // �v���C���[�̈ړ���script�𖳌��ɂ���
        _playerObj.GetComponent<MocopiAvatar>().enabled = false;

        // �v���C���[�̃R���g���[���[�̈ړ��p��script�𖳌��ɂ���
        _playerObj.GetComponent<PlayerController>().enabled = false;

        // �v���C���[��Mocopi�̈ړ��p�X�N���v�g�𖳌��ɂ���
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        // �v���C���[�̃J������script�𖳌��ɂ���
        _cameraObj.GetComponent<CinemachineBrain>().enabled = false;

        // �x�����̈ړ���script�𖳌��ɂ���
        _guardObj.GetComponent<AroundGuardsmanController>().enabled = false;

        // �x�����̈ړ���NavMeshAgent�𖳌��ɂ���
        _guardObj.GetComponent<NavMeshAgent>().enabled = false;

        // �x�����̈ړ���Animator�𖳌��ɂ���
        _guardObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}
