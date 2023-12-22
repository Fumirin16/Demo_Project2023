using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁF�n����
// �v���C���[�̓���

public class PlayerController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// ���E��]�̐��l���擾����ϐ�
    /// </summary>
    float _rot;

    /// <summary>
    /// ��]�X�s�[�h���擾����ϐ��������傫���قǑ����Ȃ�
    /// </summary>
    float _rotateSpeed;

    /// <summary>
    /// �O��ړ��X�s�[�h���擾����ϐ��������傫���قǑ����Ȃ�
    /// </summary>
    float _positionSpeed;

    /// <summary>
    /// �J�������؂�ւ�����
    /// </summary>
    bool _cameraActive;

    /// <summary>
    /// �J�����I�u�W�F�N�g�Q�Ƃ���ϐ�
    /// </summary>
    [SerializeField] GameObject _mainCamera;
    [SerializeField] GameObject _subCamera;

    /// <summary>
    /// ValueSettingManager�Q�Ƃ���ϐ�
    /// </summary>
    public ValueSettingManager settingManager;

    /// <summary>
    /// AudioManager�Q�Ƃ���ϐ�
    /// </summary>
    public AudioManager audioManager;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        // �l���Q�Ƃ������̂�ۑ�����
        _rotateSpeed = settingManager.PlayerRotateSpeed;
        _positionSpeed = settingManager.JOYSTIC_PlayerMoveSpeed;

        //�T�u�J�������A�N�e�B�u�ɂ���
        _subCamera.SetActive(false);
    }

    void Update()
    {
        // ���E��]�̐��l�擾
        _rot = Input.GetAxis("Horizontal");

        // ��]
        transform.Rotate(new Vector3(0, _rot * -_rotateSpeed, 0));

        // �O��ړ�
        // �O
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * _positionSpeed;

            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.Walk);
            }
        }
        // ���
        if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * _positionSpeed;

            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.Walk);
            }
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            if (!audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.StopSound(audioManager.seAudioSource);
            }
        }

        // �W���C�R���̉E�X�e�B�b�N�������ƃ��C���J�����ƃT�u�J������؂�ւ���
        if (Input.GetKeyDown(KeyCode.JoystickButton11) || Input.GetKeyDown(KeyCode.Space))
        {
            if(_cameraActive)
            {
                _mainCamera.SetActive(false);
                _subCamera.SetActive(true);
                _cameraActive = false;
            }
            else
            {
                _mainCamera.SetActive(true);
                _subCamera.SetActive(false);
                _cameraActive = true;
            }
        }
    }
    #endregion ---Methods---
}