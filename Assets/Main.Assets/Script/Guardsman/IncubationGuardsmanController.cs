// �쐬�ҁF�n�����i�p���\��j
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����^�x�����̓���

public class IncubationGuardsmanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // �Q�[���I�[�o�[�����蔻��
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // �Q�[���I�[�o�[�̔����true�ɂ���
            OutGameManager.gameOver = true;
            Debug.Log("�Q�[���I�[�o�[");
        }
    }
}
