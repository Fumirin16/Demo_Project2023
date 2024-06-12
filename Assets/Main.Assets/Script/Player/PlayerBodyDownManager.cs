using UnityEngine;

// 作成者：山﨑晶
// しゃがんだ時にSEを再生する

public class PlayerBodyDownManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Object ===")]
    /// <summary>
    /// Hipsのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _hipObj;

    /// <summary>
    /// ステージの床のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _stageFloor;

    [Header("=== Distance ===")]
    /// <summary>
    /// 床と腰の距離の判定用値
    /// </summary>
    private float _cheackDis;

    /// <summary>
    /// しゃがんだかを判定する
    /// </summary>
    private bool _isCrouch = false;

    [Header("=== Script ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _setSystem;

    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {
        // しゃがんだ判定の値を参照して保存する
        _cheackDis = _setSystem.downBorder;
    }

    // Update is called once per frame
    void Update()
    {
        // 腰と床の距離を測定
        float _distance = DistanceCheck();

        // 距離が_cheackDisより短くなった場合
        if (_distance <= _cheackDis && !_isCrouch)
        {
            // SEを再生する
            _audioSystem.PlaySESound(SEData.SE.Squwat);

            // しゃがんだ判定をオンにする
            _isCrouch = true;
        }
        // 距離が_cheackDisより長くなった場合
        else
        {
            // しゃがんだ判定をオフにする
            _isCrouch = false;
        }
    }

    /// <summary>
    ///  腰と床の距離を調べる関数
    /// </summary>
    /// <returns> 腰と床の距離を調べた結果 </returns>
    private float DistanceCheck()
    {
        return _hipObj.transform.position.y - _stageFloor.transform.position.y;
    }

    #endregion ---Methods---
}
