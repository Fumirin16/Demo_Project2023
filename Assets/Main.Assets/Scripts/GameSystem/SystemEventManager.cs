//�S���ҁF�R��
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class SystemEventManager : MonoBehaviour
{
    // �Q�[���I�[�o�[�A�N���A�����̕ϐ��Ńt���O�Ǘ�
    [HideInInspector]public bool gameOver;
    [HideInInspector]public bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        // �t���O�̏�����
        gameClear = false;
        gameOver = false;

        // �t���O�̊m�F�p�f�o�b�N
        Debug.Log("gameOver:" + gameOver);
        Debug.Log("gameClear:" + gameClear);
    }

    // Update is called once per frame
    void Update()
    {
        // �t���O���utrue�v�ɂȂ������̏���
        if (gameOver || gameClear)
        {
            // �Q�[�����̎��Ԃ��~�߂�
            Time.timeScale = 0;

            // �Q�[�����̎��Ԃ��~�܂��Ă��邩�̊m�F�p�f�o�b�N
            Debug.Log("Time : " + Time.timeScale);
        }
    }
}
