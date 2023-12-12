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
    public TextMeshProUGUI _countTimeText;
    // ������I�������
    private bool isAudioEnd;
    // ������x�����Đ�����t���O
    bool SEflag = true;
    // �p�l�����\���ɂ���
    [SerializeField] GameObject _bodyDownPanel;
    // audio�t����
    [SerializeField] AudioManager _audioManager;

    // �A�N�e�B�u�ɂȂ������ɌĂяo�����
    void OnEnable()
    {
        // ���Ⴊ��ł݂܂��傤�{�C�X
        _audioManager.PlaySESound(SEData.SE.BodyDownVoice);
    }

    // Update is called once per frame
    void Update()
    {
        _countTimeText.text = _count.ToString("F1");
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
            _countTimeText.text = "OK";
        }

        if (SEflag && _count > 3)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _bodyDownPanel.SetActive(false);
            gameObject.SetActive(false);
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
