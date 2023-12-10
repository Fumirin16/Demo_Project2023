//作成者地引翼
//時間制限UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public Slider timeSlider;
    //public float maxTime = 90.0f;
    private float maxTime;

    [SerializeField]
    private ValueSettingManager settingManager;

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

        if(timeSlider.value == maxTime)
        {
            // ゲームオーバーの判定をtrueにする
            settingManager.gameOver = true;
            Debug.Log("時間です");
        }
    }
}
