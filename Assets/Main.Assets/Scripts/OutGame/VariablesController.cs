using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[���I�[�o�[�A�Q�[���N���A�̔�����Ǘ�����\�[�X�R�[�h
// �쐬�ҁF�R��

public class VariablesController : MonoBehaviour
{
    // �Q�[���I�[�o�[�̔�����Ǘ�����ϐ�
    [HideInInspector] public static bool gameOverControl;

    // �Q�[���N���A�̔�����Ǘ�����ϐ�
    [HideInInspector] public static bool gameClearControl;

    // �����������鏈��
    private void Awake()
    {
        gameOverControl = false;
        gameClearControl = false;
    }
}
