//�쐬�Ғn����
//���Ԑ���UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public Slider timeSlider;
    public float maxTime = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
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
            VariablesController.gameOverControl = true;
            Debug.Log("���Ԃł�");
        }
    }
}
