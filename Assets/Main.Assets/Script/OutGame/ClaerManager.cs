using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[���őO��ɍs�����Ƃ��ɃN���A����ɂ��鏈��
// �쐬�ҁF�R����

public class ClaerManager : MonoBehaviour
{
    // �v���C���[�ɓ����������̔���̏���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // �Q�[���N���A�̔����true�ɂ���
            VariablesController.gameClearControl = true;
            Debug.Log("�N���A�ɂȂ�����");
        }
    }
}
