using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // �T�E���h�̂�������
    AudioSource _audioSource;
    // �A�i�E���X�̃e�L�X�g
    [SerializeField] TextMeshProUGUI _announceText;
    // �A�i�E���X�̃T�E���h
    [SerializeField] AudioClip _AnnounceSound;
    // �A�i�E���X���镶���̕\��
    [SerializeField] string _Text;

    public int phase = 0;

    void OnEnable()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
        AnnounceText(_Text);
        _audioSource.PlayOneShot(_AnnounceSound);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void AnnounceText(string comment)
    {
        _announceText.text = comment;
    }
}
