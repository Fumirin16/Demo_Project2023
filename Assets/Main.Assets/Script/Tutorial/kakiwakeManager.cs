using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kakiwakeManager : MonoBehaviour
{
    static List<GameObject> hitolist = new List<GameObject>();
    // �J�E���g�̃e�L�X�g
    public TextMeshProUGUI _Text;
    // ������I�������
    private bool isAudioEnd;

    bool SEflag = true;
    // audio�t����
    [SerializeField] AudioManager _audioManager;
    // �p�l�����\���ɂ���
    [SerializeField] GameObject _kakiwakePanel;


    void OnEnable()
    {
        // ���Ⴊ��ł݂܂��傤�{�C�X
        _audioManager.PlaySESound(SEData.SE.KakiwakeVoice);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hitolist.Count);
        _Text.text = hitolist.Count.ToString();

        if(hitolist.Count > 5)
        {
            _Text.text = "OK";
        }
        if (SEflag && hitolist.Count > 5)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _kakiwakePanel.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            hitolist.Add(other.gameObject);
        }
    }
}
