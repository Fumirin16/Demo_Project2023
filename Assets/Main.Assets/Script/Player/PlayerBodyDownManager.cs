using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// しゃがんだ時にSEを再生する

public class PlayerBodyDownManager : MonoBehaviour
{
    #region ---Fields---
    private const int _space = 4;

    [Header("=== Object ===")]
    /// <summary>
    /// Hipsのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _hip;

    /// <summary>
    /// ステージの床のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _stageFloor;

    [Space(_space),Header("=== Distance ===")]
    /// <summary>
    /// 床と腰の距離の判定用値
    /// </summary>
    [SerializeField]
    private float _cheackDis=1f;

    /// <summary>
    /// しゃがんだかを判定する
    /// </summary>
    private bool _isDown=false;

    [Space(_space),Header("=== Script ===")]
    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    #endregion ---Fields---

    // Update is called once per frame
    void Update()
    {
        // 腰と床の距離を測定
        float _distance = _hip.transform.position.y - _stageFloor.transform.position.y;

        // 距離が_cheackDisより短くなった場合
        if (_distance <= _cheackDis&&!_isDown)
        {
            // SEを再生する
            _audioSystem.PlaySESound(SEData.SE.Squwat);

            // しゃがんだ判定をオンにする
            _isDown = true;
        }

        // 距離が_cheackDisより長くなった場合
        if (_distance >= _cheackDis && _isDown)
        {
            // しゃがんだ判定をオフにする
            _isDown = false;
        }
    }
}
