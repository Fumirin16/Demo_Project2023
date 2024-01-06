using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// プレイヤーのMocopiを使用した移動のソースコード

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---
    /// <summary>
    /// Rigidbodyを保存する変数
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// プレイヤーが動くスピードを保存する変数
    /// </summary>
    private float _moveSpeed;

    private Vector3 _startPos;

    /// <summary>
    /// プレイヤーのカメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    [SerializeField]
    private GameObject obj;

    /// <summary>
    /// 値を参照する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager setttingManager;

    [SerializeField]
    private StandStill _power;

    public bool _isActive=false;

    #endregion ---Fields---

    #region ---Methods---

    private void Awake()
    {
        //this.transform.position=new Vector3(obj.transform.localPosition.x,this.transform.position.y,obj.transform.localPosition.z);
        _startPos = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 値を管理するアセットから値を参照して保存する
        _moveSpeed = setttingManager.MOCOPI_PlayerMoveSpeed;

        // Rigidbody   Q ?   
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {

        if (!_isActive)
        {
            this.transform.position = _startPos;
        }

        float power = _power.moveWalkPower;
        if (_power.moveWalkPower >= 0.9)
        {
            // プレイヤーを移動させる動力を保存 
            float moveSpeed = power * _moveSpeed;
            //Debug.Log("modeSpeed : " + moveSpeed);

            // カメラの向きを取得
            Vector3 cameraForward = Vector3.Scale(_playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            //Debug.Log("cameraForward : " + cameraForward);

            Vector3 moveForward = transform.forward * moveSpeed;
            // プレイヤーを移動させる
            _rb.AddForce(moveForward);
            //Debug.Log(moveForward);
        }
        else
        {
            //_rb.velocity = new Vector3(0, 0, 0);
        }



    }

    #endregion ---Methods---
}