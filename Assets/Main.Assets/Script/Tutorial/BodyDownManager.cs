using UnityEngine;
using TMPro;

//　しゃがんだ時の判定

public class BodyDownManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// しゃがむ時間取得
    /// </summary>
    [Tooltip("しゃがむ時間秒")]
    [SerializeField] int _clearCount = 3;

    /// <summary>
    /// 経過時間を表示するText型
    /// </summary>
    [Tooltip("経過時間を表示するText")] 
    [SerializeField] TextMeshProUGUI _countTimeText;

    /// <summary>
    /// パネルオブジェクト取得
    /// </summary>
    [Tooltip("BodyDownPanelアタッチ")]
    [SerializeField] GameObject _bodyDownPanel;

    /// <summary>
    /// チュートリアルしゃがみパネル取得
    /// </summary>
    [Tooltip("BodyDownPanel2アタッチ")]
    [SerializeField] GameObject _panel;

    /// <summary>
    /// AudioManager参照するための変数
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// TutorialManager参照するための変数
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// 経過時間を格納
    /// </summary>
    float _count;

    /// <summary>
    /// 視界に入ったか判定をとる
    /// </summary>
    bool _active = true;

    /// <summary>
    /// 音が鳴り終わったか判定するbool
    /// </summary>
    bool _isAudioEnd;

    /// <summary>
    /// SEを一度だけ再生させるbool
    /// </summary>
    bool _SEflag = true;

    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.BodyDownVoice);
        _panel.gameObject.SetActive(true);
    }

    void Update()
    {
        // 視界から外れてたら時間加算
        if (_active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }

        // 経過時間表示
        _countTimeText.text = _count.ToString("F1");

        // 指定した回数以上足踏み出来たらずっとOK表示
        // OKサウンドを鳴らす
        if (_count > _clearCount && _SEflag)
        {
            _countTimeText.text = "OK";

            // OKサウンドを鳴らす
            _audioManager.PlaySESound(SEData.SE.Correct);
            _SEflag = false;
            _isAudioEnd = true;
        }

        // SEが鳴り終わったら
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && _isAudioEnd)
        {
            _bodyDownPanel.SetActive(false);
            gameObject.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _active = false;
    }
    void OnTriggerExit(Collider other)
    {
        _active = true;
    }
    #endregion ---Methods---
}