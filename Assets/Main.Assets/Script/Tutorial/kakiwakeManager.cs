using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kakiwakeManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// エリアに入ったら
    /// </summary>
    public static List<GameObject> hitolist = new List<GameObject>();
    // カウントのテキスト
    public TextMeshProUGUI _Text;
    // 音が鳴り終わったか
    private bool isAudioEnd;

    bool SEflag = true;
    // audio付ける
    [SerializeField] AudioManager _audioManager;
    // パネルを非表示にする
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
        // タグ：Enemy
        if(other.gameObject.CompareTag("Enemy"))
        {
            hitolist.Add(other.gameObject);
        }
    }
    #endregion ---Methods---
}