// �쐬�ҁF�n�����i�p���\��j
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����^�x�����̓���

public class IncubationGuardsmanController : MonoBehaviour
{
    // �l���Ǘ�����A�Z�b�g����l���Q�Ƃ���
    [SerializeField]
    private ValueSettingManager settingManager;

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
        if (other.gameObject.CompareTag("Player"))
        {
            // �Q�[���I�[�o�[�̔����true�ɂ���
            settingManager.gameOver = true;
            Debug.Log("�Q�[���I�[�o�[");
        }
    }
}
