using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// �쐬�ҁF�n����

public class FadeController : MonoBehaviour
{
    // �s�v�Ȃ̂ŏ����܂����Bby�R����
    //AudioSource audioSource;
    //[Tooltip("�����Ƀu�U�[��������")]
    //[SerializeField] AudioClip buzzerClip;
    [Tooltip("������Ȃ�")]
    [SerializeField] UITimer _timer;
    [Tooltip("������Ȃ�")]
    [SerializeField] AroundGuardsmanController _controller;

    // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
    [Tooltip("�t�F�[�h�C���ɂ����鎞��")]
    [SerializeField] const float fade_time = 1.0f;

    // ���[�v�񐔁i0�̓G���[�j���ύX��
    [Tooltip("���[�v�񐔁A���������Ɗ��炩�ɂȂ�")]
    [SerializeField] const int loop_count = 60;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] Image countdownImage;
    [SerializeField] Image fadePanel;

    public NavMeshAgent guardsman;

    float countdown = 4f;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        guardsman = guardsman.GetComponent<NavMeshAgent>();

        //UITimer,AroundGuardsmanController���ꎞ��~����
        _timer.enabled = false;
        _controller.enabled = false;
        fadePanel.enabled = true;
        guardsman.enabled = false;


        // �s�v�Ȃ̂ŏ����܂����Bby�R����
        //audioSource = GetComponent<AudioSource>();

        //�t�F�[�h�C���R���[�`���X�^�[�g
        StartCoroutine("Color_FadeIn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Color_FadeIn()
    {
        //���y��炷
        // SE�̃u�U�[�����Đ����܂��Bby�R����
        //audioSource.PlayOneShot(buzzerClip);
        AudioManager.Instance.Play_SESound(SESoundData.SE.Buzzer);

        //�I���܂őҋ@
        // �Ȃ�����Ă��邩�`�F�b�N����֐����ĂсA�Ȃ�����I������炱�̊֐��́ufalse�v�̒l�����̂ł��̏������ɂ��Ă��܂��Bby �R����
        yield return new WaitWhile(() => (!AudioManager.Instance.CheckPlaySound(AudioManager.Instance.seAudioSource)));

        // ��ʂ��t�F�[�h�C��������R�[���`��

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h���̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l��������������
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }

        countdownText.gameObject.SetActive(true);
        countdownImage.gameObject.SetActive(true);

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            countdownImage.fillAmount = countdown % 1.0f;
            count = (int)countdown;
            countdownText.text = count.ToString();

            if (countdown <= 0)
            {
                //UITimer,AroundGuardsmanController���Đ�����
                _timer.enabled = true;
                _controller.enabled = true;
                guardsman.enabled = true;

                countdownText.gameObject.SetActive(false);
                countdownImage.gameObject.SetActive(false);

                // BGM���Đ����� by�R����
                AudioManager.Instance.Play_BGMSound(BGMSoundData.BGM.Main);

                yield break;
            }
            yield return null;
        }

    }
}
