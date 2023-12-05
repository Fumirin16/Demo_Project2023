using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

//�@�r�f�I�ƃe�L�X�g�{�C�X����

public class syagami : MonoBehaviour
{
    VideoClip _videoClip;
    GameObject screen;
    [SerializeField] TextMeshProUGUI _announceText;
    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = screen.AddComponent<VideoPlayer>();   // videoPlaye�R���|�[�l���g�̒ǉ�

        videoPlayer.source = VideoSource.VideoClip; // ����\�[�X�̐ݒ�
        videoPlayer.clip = _videoClip;

        videoPlayer.isLooping = true;   // ���[�v�̐ݒ�
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AnnounceText("���Ⴊ��ł݂܂��傤");
        audioSource.PlayOneShot(sound1);
    }
    public void VPControl()
    {
        var videoPlayer = GetComponent<VideoPlayer>();

        if (!videoPlayer.isPlaying) // �{�^�������������̏���
            videoPlayer.Play(); // ������Đ�����B
        else
            videoPlayer.Pause();    // ������ꎞ��~����B
    }
    public void AnnounceText(string comment)
    {
        _announceText.text = comment;
    }
}
