using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

//�@���Ⴊ�񂾎��̔���

public class syagami : MonoBehaviour
{
    // �o�ߎ���
    float _count;
    // ���Ⴊ�ݏo���Ă��邩�ł��Ă��Ȃ���
    bool _active = true;
    // �J�E���g�̃e�L�X�g
    public TextMeshProUGUI _countText;
    // �����T�E���h
    public AudioClip _correct;
    // �T�E���h�̂�������
    AudioSource _audioSource;
    // ������I�������
    private bool isAudioEnd;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _countText.text = _count.ToString("F1");
        if (_active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }

        if (_count > 3)
        {
            _countText.text = "OK";

            //�����ɐ���SE
            _audioSource.PlayOneShot(_correct);
            isAudioEnd = true;
        }
        if (!_audioSource.isPlaying && isAudioEnd)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _active = false;
    }
    private void OnTriggerExit(Collider other)
    {
        _active = true;
    }
}
