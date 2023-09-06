//�쐬�n��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip buzzerClip;
    [SerializeField] UITimer _timer;
    [SerializeField] AroundGuardsmanController _controller;

    // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
    [SerializeField] const float fade_time = 1.0f;

    // ���[�v�񐔁i0�̓G���[�j���ύX��
    [SerializeField] const int loop_count = 50;


    // Start is called before the first frame update
    void Start()
    {
        //UITimer,AroundGuardsmanController���ꎞ��~����
        _timer.enabled = false;
        _controller.enabled = false;

        audioSource = GetComponent<AudioSource>();

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
        audioSource.PlayOneShot(buzzerClip);

        //�I���܂őҋ@
        yield return new WaitWhile(() => audioSource.isPlaying);

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
        //UITimer,AroundGuardsmanController���Đ�����
        _timer.enabled = true;
        _controller.enabled = true;
    }
}
