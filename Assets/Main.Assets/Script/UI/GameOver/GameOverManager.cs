using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �쐬�ҁF�R���� 
// �Q�[���I�[�o�[��UI���o����

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  FadeManager
    /// </summary>
    [SerializeField]
    private FadeManager _fadeSystem;

    /// <summary>
    /// �w�i���t�F�[�h�C������ݒ�
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeIn;

    /// <summary>
    ///  �A�C�h���̉摜
    /// </summary>
    [SerializeField]
    private GameObject _idolImage;

    /// <summary>
    /// �@�ϋq�̉摜
    /// </summary>
    [SerializeField]
    private GameObject _audienceImage;

    /// <summary>
    /// �v���C���[�̉摜
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// ���C����ʂɖ߂�{�^���̃I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    private GameObject _returnButton;

    /// <summary>
    /// �^�C�g����ʂɖ߂�{�^���̃I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    private GameObject _backButton;

    /// <summary>
    /// �{�^�����I������ĂȂ��Ƃ��ɕ\������摜
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButton = new GameObject[2];

    /// <summary>
    /// �{�^������\�����锻��
    /// </summary>
    private bool _isButton = false;

    /// <summary>
    /// ���ݑI�����Ă���I�u�W�F�N�g��ۑ�
    /// </summary>
    private GameObject _button;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // �{�^�����\���ɂ���
        _returnButton.SetActive(false);
        _backButton.SetActive(false);

        // �摜���\���ɂ���
        _idolImage.SetActive(false);
        _audienceImage.SetActive(false);
        _playerImage.SetActive(false);

        // �I���摜���\���ɂ���
        foreach (GameObject selectImage in _selectButton)
        {
            selectImage.SetActive(false);
        }

        // �����ɑI����Ԃɂ���I�u�W�F�N�g��ݒ肷��
        EventSystem.current.SetSelectedGameObject(_returnButton);
    }

    // Update is called once per frame
    void Update()
    {
        // ���ݑI�����Ă���{�^����ۑ�����
        _button = EventSystem.current.currentSelectedGameObject;

        // �t�F�[�h�C�����I������ꍇ
        if (FadeManager.fadeIn)
        {
            // �摜��\������
            _idolImage.SetActive(true);
            _audienceImage.SetActive(true);
            _playerImage.SetActive(true);

            // �{�^����\������悤�ɂ���
            _isButton = true;
        }
        else if (!FadeManager.fadeIn)
        {
            _fadeSystem.FadeIn(_backGroundFadeIn);
        }

        // �{�^�����\������锻��true�ɂȂ����ꍇ
        if (_isButton)
        {
            // �{�^����\���ɂ���
            _returnButton.SetActive(true);
            _backButton.SetActive(true);
        }

        // �{�^�����I������Ă����Ԃ�\������
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