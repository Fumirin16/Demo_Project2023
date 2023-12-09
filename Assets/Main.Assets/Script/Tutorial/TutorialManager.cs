using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

// �e�L�X�g�A�{�C�X�A�r�f�I�𗬂�

public class TutorialManager : MonoBehaviour
{
    // �T�E���h�̂�������
    [System.NonSerialized] public AudioSource _audioSource;
    // �A�i�E���X�̃e�L�X�g
    [SerializeField] TextMeshProUGUI _announceText;
    // �A�i�E���X�̃T�E���h
    [SerializeField] AudioClip[] _AnnounceSound;
    // �A�i�E���X���镶���̕\��
    [SerializeField] string[] _Text;

    // Start is called before the first frame update
    void Start()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
        AnnounceText(_Text[0]);
        _audioSource.PlayOneShot(_AnnounceSound[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnnounceText(string comment)
    {
        _announceText.text = comment;
    }
}
