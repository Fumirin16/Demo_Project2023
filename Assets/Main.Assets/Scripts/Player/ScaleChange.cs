//���Ⴊ�ޑ���ɃI�u�W�F�N�g����������
//�쐬�҂΂�
//�f�o�b�O�p
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    //�������������̑傫��
    public Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[���������Ƃ��ɑ傫���ς��
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
