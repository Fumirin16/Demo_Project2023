using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁF�R����
// �v���C���[�𑫓��݂ňړ����鏈���̃\�[�X�R�[�h

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---
    /// <summary>
    /// Rigidbody���擾����ϐ�
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// �J�����̃I�u�W�F�N�g���擾����
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody���Q�Ƃ���
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �i�ޓ��͂�ۑ�����
        float moveSpeed = StandStill.powerSource;

        // �X�e�B�b�N�̓��͂�ۑ�����
        float stickHorizontal = Input.GetAxis("Horizontal");

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * stickHorizontal;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        _rb.velocity = moveForward * moveSpeed + new Vector3(0, _rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    #endregion ---Methods---
}
