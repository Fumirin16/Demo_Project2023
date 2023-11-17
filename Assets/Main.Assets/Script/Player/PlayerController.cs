using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁF�n����

public class PlayerController : MonoBehaviour
{
    // ���E��]�̐��l�擾����ϐ�
    float _rot = 0.0f;
    // ��]�X�s�[�h���擾����ϐ�
    [Tooltip("��]�X�s�[�h�������傫���قǑ����Ȃ�")]
    public float rotateSpeed = 0.0f;
    // �O��ړ��X�s�[�h���擾����ϐ�
    [Tooltip("�O��X�s�[�h�������傫���قǑ����Ȃ�")]
    public float positionSpeed = 0.5f;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce(new Vector3(0, 0, 0));
        _rb.velocity = Vector3.zero;
        // ���E��]�̐��l�擾
        _rot = Input.GetAxis("Horizontal");
        //Debug.Log(_rot);

        // ��]
        transform.Rotate(new Vector3(0, _rot * -rotateSpeed, 0));

        // �O��ړ�
        // �O
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        {
            //_rb.AddForce(transform.forward * positionSpeed);
            transform.position += transform.forward * positionSpeed;
        }
        // ���
        if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        {
            //_rb.AddForce(-transform.forward * positionSpeed);
            transform.position -= transform.forward * positionSpeed;
        }
    }
}
