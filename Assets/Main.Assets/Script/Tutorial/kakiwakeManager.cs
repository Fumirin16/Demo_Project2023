using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kakiwakeManager : MonoBehaviour
{
    public static List<GameObject> hitolist = new List<GameObject>();
    // �J�E���g�̃e�L�X�g
    public TextMeshProUGUI _Text;
    // ������I�������
    private bool isAudioEnd;

    bool SEflag = true;
    // audio�t����
    [SerializeField] AudioManager _audioManager;
    // �p�l�����\���ɂ���
    [SerializeField] GameObject _kakiwakePanel;

    public TutorialManager _tutorialManager;

    public GameObject obj;

    public GameObject _vcam;


    private void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.KakiwakeVoice);
        Debug.Log("kakiwake");
        _vcam.SetActive(true);
    }

    private void Start()
    {
        //_audioManager.PlaySESound(SEData.SE.KakiwakeVoice);
        _tutorialManager = obj.GetComponent<TutorialManager>();
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
            _tutorialManager._phaseCount++;
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
