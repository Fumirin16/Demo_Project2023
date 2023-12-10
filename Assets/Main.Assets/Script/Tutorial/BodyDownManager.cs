using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//�@���Ⴊ�񂾎��̔���

public class BodyDownManager : MonoBehaviour
{
    // �o�ߎ���
    float _count;
    // ���Ⴊ�ݏo���Ă��邩�ł��Ă��Ȃ���
    bool _active = true;
    // �J�E���g�̃e�L�X�g
    public TextMeshProUGUI _countText;
    // �����T�E���h
    [SerializeField] AudioClip _correctAudio;
    // �A�i�E���X�T�E���h
    [SerializeField] AudioClip _announceAudio;
    // �T�E���h�̂�������
    AudioSource _audioSource;
    // ������I�������
    private bool isAudioEnd;
    // ������x�����Đ�����t���O
    bool SEflag = true;
    [SerializeField] GameObject _crouchPanel;

    void OnEnable()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
        // ���Ⴊ��ł݂܂��傤�{�C�X
        _audioSource.PlayOneShot(_announceAudio);
    }

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

        if(_count >3)
        {
            _countText.text = "OK";
        }

        if (SEflag && _count > 3)
        {
            _audioSource.PlayOneShot(_correctAudio);
            SEflag = false;

            isAudioEnd = true;
        }
        if (!_audioSource.isPlaying && isAudioEnd)
        {
            _crouchPanel.SetActive(false);
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
