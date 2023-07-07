//�v���C���[�̓���
//�O��ړ��ƍ��E��]
//�f�o�b�O�p
//�쐬�҂΂�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Rot = 0.0f;
    public int rotateSpeed = 0; //��]�X�s�[�h
    public float PositionSpeed = 0.0f; //�O��ړ��X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���E��]�̐��l�擾
        Rot = Input.GetAxis("Horizontal");

        //�O��ړ�
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * PositionSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * PositionSpeed;
        }

        //����͉�]
        if (Rot > 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot + rotateSpeed, 0));
        }
        if (Rot < 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot - rotateSpeed, 0));
        }
    }
}
