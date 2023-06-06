//�v���C���[�̋���
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Rot = 0.0f;
    public int RotateSpeed = 0;
    public GameObject ArmLeft;
    public GameObject ArmRight;

    public float Speed = 0.0f;
    public float PositionSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rot = Input.GetAxis("Horizontal");
        //Debug.Log(Rot);

        //A,D�L�[�ō��E�ɐU�����
        //RotateSpeed�ŉ�]�̑��x��ς����
        //�O�i�͐i�߂�G���A���ł�����i�ނɂ����ق��������Ǝv��������L�[���͂őO��ړ��͂���ĂȂ�

        //�f�o�b�O�p�ɑO��ړ�����ς����
        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * PositionSpeed;
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * PositionSpeed;
        }

        //����͉�]
        if (Rot > 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot + RotateSpeed, 0));
        }
        if(Rot < 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot - RotateSpeed, 0));
        }

        //����͘r
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ArmLeft.transform.Rotate(new Vector3(0, -Speed, 0));
            ArmRight.transform.Rotate(new Vector3(0, Speed, 0));
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ArmLeft.transform.Rotate(new Vector3(0, Speed, 0));
            ArmRight.transform.Rotate(new Vector3(0, -Speed, 0));
        }
    }
}
