using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�@�쐬�Ғn����
//�@���Ԑ���UI

public class UITimer : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// Slider�I�u�W�F�N�g�ϐ�
    /// </summary>
    [SerializeField] Slider timeSlider;

    float maxTime;

    [SerializeField]
    private ValueSettingManager settingManager;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = settingManager.GameLimitTime;

        timeSlider = GetComponent<Slider>();

        //�X���C�_�[�̍ő�l�̐ݒ�
        timeSlider.maxValue = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        //�X���C�_�[�̌��ݒl�̐ݒ�
        timeSlider.value += Time.deltaTime;

        if(timeSlider.value == maxTime)
        {
            // �Q�[���I�[�o�[�̔����true�ɂ���
            settingManager.gameOver = true;
            Debug.Log("���Ԃł�");
        }
    }
}
