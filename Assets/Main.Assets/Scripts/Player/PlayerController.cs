//�v���C���[�̓���
//�O��ړ��ƍ��E��],���Ⴊ�ޑ���ɃI�u�W�F�N�g������
//�f�o�b�O�p
//�쐬�҂΂�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Rot = 0.0f;
    public float rotateSpeed = 0; //��]�X�s�[�h
    public float PositionSpeed = 0.0f; //�O��ړ��X�s�[�h
    public Vector3 scale; //�������������̑傫��


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
        if (Input.GetKey(KeyCode.JoystickButton2))
        {
            transform.position -= transform.forward * PositionSpeed;

        }
        if (Input.GetKey(KeyCode.JoystickButton1))
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

        //�X�y�[�X�L�[���������Ƃ��ɑ傫���ς��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
