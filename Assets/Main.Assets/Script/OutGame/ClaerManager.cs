using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁF�R����
// �v���C���[���őO��ɍs�����Ƃ��ɃN���A����ɂ��鏈��

public class ClaerManager : MonoBehaviour
{
    // �v���C���[�ɓ����������̔���̏���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // �Q�[���N���A�̔����true�ɂ���
            OutGameManager.gameClear = true;

            //Debug.Log("�N���A�ɂȂ�����");
        }
    }
}
