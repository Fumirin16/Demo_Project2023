using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// フレームレート数の表示
// 作成者：地引翼

public class FPSDisplay : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// フレームレート数を取得する変数
    /// </summary>
    float _fps;

    float _timeFPS = 1.0f;

    #endregion ---Fields---

    void Update()
    {
        // Time.deltaTimeは前回のフレームからの経過時間（秒）を表す変数
        _fps = _timeFPS / Time.deltaTime;
        //Debug.Log(_fps.ToString("F2"));
    }
}