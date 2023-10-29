using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁF�n����

public class PlayerController : MonoBehaviour
{
    // ���E��]�̐��l�擾����ϐ�
    float Rot = 0.0f;
    // ��]�X�s�[�h���擾����ϐ�
    public float RotateSpeed = 0.0f;
    // �O��ړ��X�s�[�h���擾����ϐ�
    public float PositionSpeed = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���E��]�̐��l�擾
        Rot = Input.GetAxis("Horizontal");
        //Debug.Log(Rot);

        // ��]
        transform.Rotate(new Vector3(0, Rot * RotateSpeed, 0));

        // �O��ړ�
        // �O
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * PositionSpeed;
        }
        // ���
        if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * PositionSpeed;
        }
    }
}
