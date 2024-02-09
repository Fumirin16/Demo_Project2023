using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class kakiwakeManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// カウントのテキスト
    /// </summary>
    [Tooltip("カウントするテキストをアタッチ")]
    [SerializeField] TextMeshProUGUI _Text;

    /// <summary>
    /// AudioManager取得
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// エリアに入ったら
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// kakiwakepanel取得
    /// </summary>
    [Tooltip("kakiwakeパネルをアタッチ")]
    [SerializeField] GameObject _kakiwakePanel;

    /// <summary>
    /// TutoerialSystemオブジェクト取得
    /// </summary>
    [Tooltip("TutorialSystemオブジェクトをアタッチ")]
    [SerializeField] GameObject _tutorialSystem;

    /// <summary>
    /// vcam取得
    /// </summary>
    [Tooltip("vcamをアタッチ")]
    [SerializeField] GameObject _vcam;

    /// <summary>
    /// パネル2を取得
    /// </summary>
    [Tooltip("パネル2をアタッチ")]
    [SerializeField] GameObject _panel;

    /// <summary>
    /// エリアに入ったらクリアできる人数
    /// </summary>
    [Tooltip("エリアに入ったらクリアできる人数")]
    [SerializeField] int _hitoCount;

    /// <summary>
    /// エリアに入った人を数えるリスト
    /// </summary>
    public static List<GameObject> hitolist = new List<GameObject>();

    /// <summary>
    /// 音が鳴り終わったか
    /// </summary>
    bool _isAudioEnd;

    /// <summary>
    /// 一度だけ音鳴らす
    /// </summary>
    bool _SEflag = true;

    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.KakiwakeVoice);
        _panel.gameObject.SetActive(true);
        _vcam.SetActive(true);
    }

    void Update()
    {
        // Text表示
        _Text.text = hitolist.Count.ToString();

        if (hitolist.Count > _hitoCount && _SEflag)
        {
            _Text.text = "OK";

            // OKサウンドを鳴らす
            _audioManager.PlaySESound(SEData.SE.Correct);
            _SEflag = false;
            _isAudioEnd = true;
        }

        // 音が鳴り終わったら次のフェーズへ
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && _isAudioEnd)
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
            // リストに追加
            hitolist.Add(other.gameObject);
        }
    }
    #endregion ---Methods---
}