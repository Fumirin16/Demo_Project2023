using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �쐬�ҁF�R���� 
// �X�e�[�W�̍őP�ɂ���Q�[�g�ƐڐG�����Ƃ��ɃQ�[���N���A�ɂ��鏈��

public class ClaerManager : MonoBehaviour
{
    //  �N���A����Ɠ��������ꍇ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //  �Q�[���N���A�̔��������
            OutGameManager.gameClear = true;

            //Debug.Log("�N���A�ɂȂ���");
        }
    }
}