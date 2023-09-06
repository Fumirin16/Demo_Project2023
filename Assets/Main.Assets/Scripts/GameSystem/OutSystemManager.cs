// �쐬�ҁF�R����
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OutSystemManager : MonoBehaviour
{
    // �I�[�o�[�܂��̓N���A�̃t���O���utrue�v�ɂȂ����Ƃ��ɕ\������p�l��
    [SerializeField, Tooltip("�u���[�ނ��肠�v��\������p�l�����A�^�b�`����")] private GameObject clearPanel;
    [SerializeField, Tooltip("�u���[�ނ��[�΁[�v��\������p�l�����A�^�b�`����")] private GameObject overPanel;

    // �uSystemEventManager�v���Ăяo��
    [SerializeField, Tooltip("�uInGameSystem�v�̃I�u�W�F�N�g���A�^�b�`����")] SystemEventManager systemManager;


    [SerializeField] UnityEvent systemEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (systemManager == null)
        {
            systemEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �I�[�o�[�܂��̓N���A�̃t���O���utrue�v�������ꍇ�A�I�[�o�[�܂��̓N���A�̉�ʂ�\��������
        if (systemManager.gameClear)
        {
            clearPanel.SetActive(true);
        }
        else if (systemManager.gameOver)
        {
            overPanel.SetActive(true);
        }
    }

    // �Q�[���I�[�o�[�܂��̓Q�[���N���A�̉�ʂɔz�u����Ă���{�^���������ꂽ�Ƃ��̏���
    public void OnClick_Button()
    {
        // �I�[�o�[�܂��̓N���A�̃t���O���utrue�v�������ꍇ�A�I�[�o�[�܂��̓N���A�̉�ʂ��\���ɂ����ăt���O���ufalse�v�ɂ���
        if (systemManager.gameClear)
        {
            clearPanel.SetActive(false);
            systemManager.gameClear = false;
        }
        else if (systemManager.gameOver)
        {
            overPanel.SetActive(false);
            systemManager.gameOver = false;
        }

        // �Q�[�����̎��Ԃ��쓮�h����
        Time.timeScale = 1;

        //����̃V�[���ɑJ�ڂ�����B
        systemEvent.Invoke();
    }
}
