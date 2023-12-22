using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：地引翼
// プレイヤーの動き

public class PlayerController : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 左右回転の数値を取得する変数
    /// </summary>
    float _rot;

    /// <summary>
    /// 回転スピードを取得する変数数字が大きいほど速くなる
    /// </summary>
    float _rotateSpeed;

    /// <summary>
    /// 前後移動スピードを取得する変数数字が大きいほど速くなる
    /// </summary>
    float _positionSpeed;

    /// <summary>
    /// カメラが切り替え判定
    /// </summary>
    bool _cameraActive;

    /// <summary>
    /// カメラオブジェクト参照する変数
    /// </summary>
    [SerializeField] GameObject _mainCamera;
    [SerializeField] GameObject _subCamera;

    /// <summary>
    /// ValueSettingManager参照する変数
    /// </summary>
    public ValueSettingManager settingManager;

    /// <summary>
    /// AudioManager参照する変数
    /// </summary>
    public AudioManager audioManager;

    #endregion ---Fields---

    #region ---Methods---

    void Start()
    {
        // 値を参照したものを保存する
        _rotateSpeed = settingManager.PlayerRotateSpeed;
        _positionSpeed = settingManager.JOYSTIC_PlayerMoveSpeed;

        //サブカメラを非アクティブにする
        _subCamera.SetActive(false);
    }

    void Update()
    {
        // 左右回転の数値取得
        _rot = Input.GetAxis("Horizontal");

        // 回転
        transform.Rotate(new Vector3(0, _rot * -_rotateSpeed, 0));

        // 前後移動
        // 前
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * _positionSpeed;

            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.Walk);
            }
        }
        // 後ろ
        if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * _positionSpeed;

            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.Walk);
            }
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            if (!audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.StopSound(audioManager.seAudioSource);
            }
        }

        // ジョイコンの右スティックを押すとメインカメラとサブカメラを切り替える
        if (Input.GetKeyDown(KeyCode.JoystickButton11) || Input.GetKeyDown(KeyCode.Space))
        {
            if(_cameraActive)
            {
                _mainCamera.SetActive(false);
                _subCamera.SetActive(true);
                _cameraActive = false;
            }
            else
            {
                _mainCamera.SetActive(true);
                _subCamera.SetActive(false);
                _cameraActive = true;
            }
        }
    }
    #endregion ---Methods---
}