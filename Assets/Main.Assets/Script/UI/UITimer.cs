using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Slider timeSlider;

    float maxTime;

    [SerializeField]
    private ValueSettingManager settingManager;

    [SerializeField] OutGameManager gameManager;
    #endregion ---Fields---

    // Start is called before the first frame update
    void Start()
    {
        maxTime = settingManager.GameLimitTime;

        timeSlider = GetComponent<Slider>();

        //スライダーの最大値の設定
        timeSlider.maxValue = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        //スライダーの現在値の設定
        timeSlider.value += Time.deltaTime;

        if (timeSlider.value == maxTime&& !settingManager.gameClear)
        {
            // ゲームオーバーの判定をtrueにする
            settingManager.gameOver = true;
            Debug.Log("時間です");
        }
        else if (timeSlider.value == maxTime && settingManager.gameClear)
        {
            gameManager.GameClear();
        }
    }
}
