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

    public TutorialManager _tutorialManager;

    private void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.BodyDownVoice);
        Debug.Log("syagami");
    }

    void Update()
    {
        if (_active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }
        _countTimeText.text = _count.ToString("F1");

        if (_count >3)
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
            _tutorialManager._phaseCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _active = false;
            Debug.Log("ataltuteru");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _active = true;
            Debug.Log("nai");
        }
    }
}
