using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kakiwakeManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// �G���A�ɓ�������
    /// </summary>
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

    [SerializeField] GameObject _panel;
    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.KakiwakeVoice);
        _panel.gameObject.SetActive(true);
        _vcam.SetActive(true);
    }

    void Start()
    {
        _tutorialManager = obj.GetComponent<TutorialManager>();
    }

    void Update()
    {
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

    void OnTriggerEnter(Collider other)
    {
        // �^�O�FEnemy
        if(other.gameObject.CompareTag("Enemy"))
        {
            hitolist.Add(other.gameObject);
        }
    }
    #endregion ---Methods---
}