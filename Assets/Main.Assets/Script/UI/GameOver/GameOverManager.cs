using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �쐬�ҁF�R����
// �Q�[���I�[�o�[�Ɋւ���\�[�X�R�[�h

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �uFadeManager�v���Q��
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    /// �w�i�摜�̃t�F�[�h��ݒ�
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeIn;

    /// <summary>
    /// �A�C�h���摜���擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _idolImage;

    /// <summary>
    /// �ϋq�摜���擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _audienceImage;

    /// <summary>
    /// �v���C���[�摜���擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// �߂�{�^�����擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _returnButton;

    /// <summary>
    /// �^�C�g���{�^�����擾����ϐ�
    /// </summary>
    [SerializeField]
    private GameObject _backButton;

    /// <summary>
    /// �{�^�����I�����Ă��Ȃ��Ƃ��̉摜���擾����
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButton = new GameObject[2];

    /// <summary>
    /// �{�^�����I���ł���󋵂��𔻒肷��ϐ�
    /// </summary>
    private bool _isButton = false;

    /// <summary>
    /// ���ݑI�����Ă���I�u�W�F�N�g��ۑ�����ϐ�
    /// </summary>
    private GameObject _button;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // �{�^���̃I�u�W�F�N�g���\���ɂ���
        _returnButton.SetActive(false);
        _backButton.SetActive(false);

        // �L�����N�^�[�摜���\���ɂ���
        _idolImage.SetActive(false);
        _audienceImage.SetActive(false);
        _playerImage.SetActive(false);

        // �I���摜���\���ɂ���
        foreach (GameObject selectImage in _selectButton)
        {
            selectImage.SetActive(false);
        }

        // �����őI����Ԃɂ��Ă����I�u�W�F�N�g�̐ݒ�
        EventSystem.current.SetSelectedGameObject(_returnButton);
    }

    // Update is called once per frame
    void Update()
    {
        // ���ݑI�����Ă���I�u�W�F�N�g��ۑ�����ϐ�
        _button = EventSystem.current.currentSelectedGameObject;

        // �t�F�[�h�C�����I������ꍇ
        if (FadeManager.fadeIn)
        {
            // �L�����N�^�[�摜��\������
            _idolImage.SetActive(true);
            _audienceImage.SetActive(true);
            _playerImage.SetActive(true);

            // �{�^���̑I�����ł���悤�ɂ���
            _isButton = true;
        }
        else if(!FadeManager.fadeIn)
        {
            _fadeSystem.FadeIn(_backGroundFadeIn);
        }

        // �{�^���̑I�����o
        if (_isButton)
        {
            _returnButton.SetActive(true);
            _backButton.SetActive(true);
        }

        if (_button == _returnButton && _isButton)
        {
            _selectButton[0].SetActive(false);
            _selectButton[1].SetActive(true);
        }
        if (_button == _backButton && _isButton)
        {
            _selectButton[0].SetActive(true);
            _selectButton[1].SetActive(false);
        }
    }

    #endregion ---Methods---
}
