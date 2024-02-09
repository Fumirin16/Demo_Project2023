using UnityEngine;
using UnityEngine.UI;

//　作成者地引翼
//　時間制限UI

public class UITimer : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// Sliderオブジェクト変数
    /// </summary>
    [Tooltip("時間制限のUIをアタッチ")]
    [SerializeField] Slider _timeSlider;

    // スクリプト参照変数
    [SerializeField]　ValueSettingManager _settingManager;
    [SerializeField] OutGameManager _gameManager;

    /// <summary>
    /// 時間制限変数
    /// </summary>
    float _maxTime;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        _maxTime = _settingManager.GameLimitTime;

        //スライダーの最大値の設定
        _timeSlider.maxValue = _maxTime;
    }

    void Update()
    {
        //スライダーの現在値の設定
        _timeSlider.value += Time.deltaTime;

        if (_timeSlider.value >= _maxTime)
        {
            GameEndFunc();
        }
    }

    void GameEndFunc()
    {
        if (_settingManager.gameClear)
        {
            _gameManager.GameClear();
        }
        else
        {
            // ゲームオーバーの判定をtrueにする
            _settingManager.gameOver = true;
        }
    }
    #endregion ---Methods---
}