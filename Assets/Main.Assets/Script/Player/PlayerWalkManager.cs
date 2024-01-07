using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// プレイヤーのMocopiを使用した移動のソースコード

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---
    private const int _space = 4;

    [Header("=== Camera ===")]
    /// <summary>
    /// プレイヤーのカメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    [Space(_space), Header("=== Script ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingManager;

    /// <summary>
    /// Col.RightToeBaseのスクリプト
    /// </summary>
    [SerializeField]
    private StandStill _movePower;

    /// <summary>
    /// Rigidbodyを保存する変数
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// プレイヤーが動くスピードを保存する変数
    /// </summary>
    private float _moveSpeed = 0f;

    /// <summary>
    /// プレイヤーのスタート位置
    /// </summary>
    private Vector3 _startPos = new Vector3(0, 0, 0);

    /// <summary>
    /// プレイヤーが初期位置から移動していいかの判定
    /// </summary>
    [HideInInspector]
    public bool _isActive = false;

    #endregion ---Fields---

    #region ---Methods---

    private void Awake()
    {
        // プレイヤーの現在の位置を保存する
        _startPos = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 値を管理するアセットから値を参照して保存する
        _moveSpeed = _settingManager.MOCOPI_PlayerMoveSpeed;

        // Rigidbodyを取得する
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // プレイヤーが動いてはダメな判定だった場合
        if (!_isActive)
        {
            // プレイヤーを初期位置で固定する
            this.transform.position = _startPos;
        }

        // 値を参照する
        float power = _settingManager.movePower;

        // Powerが1以上だった場合 
        if (_movePower.moveWalkPower >= 1)
        {
            // プレイヤーを移動させる動力を保存 
            float moveSpeed = power * _moveSpeed;

            // プレイヤーの正面の方向を取得する
            Vector3 moveForward = transform.forward * moveSpeed;

            // プレイヤーを移動させる
            _rb.AddForce(moveForward, ForceMode.Impulse);
        }
    }

    #endregion ---Methods---
}