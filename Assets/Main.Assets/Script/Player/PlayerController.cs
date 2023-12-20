using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：地引翼
// プレイヤーの動き

public class PlayerController : MonoBehaviour
{
    // 左右回転の数値取得する変数
    float _rot = 0.0f;
    // 回転スピードを取得する変数
    [Tooltip("回転スピード数字が大きいほど速くなる")]
    //public float rotateSpeed = 0.5f;
    private float rotateSpeed;
    // 前後移動スピードを取得する変数
    [Tooltip("前後スピード数字が大きいほど速くなる")]
    //public float positionSpeed = 0.5f;
    private float positionSpeed;
    // 値を参照する変数
    public ValueSettingManager settingManager;
    public AudioManager audioManager;
    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        // 値を管理するアセットから値を参照して変数に保存する
        rotateSpeed = settingManager.PlayerRotateSpeed;
        positionSpeed = settingManager.JOYSTIC_PlayerMoveSpeed;
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce(new Vector3(0, 0, 0));
        _rb.velocity = Vector3.zero;
        // 左右回転の数値取得
        _rot = Input.GetAxis("Horizontal");
        //Debug.Log(_rot);

        // 回転
        transform.Rotate(new Vector3(0, _rot * -rotateSpeed, 0));

        // 前後移動
        // 前
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        {
            //_rb.AddForce(transform.forward * positionSpeed);
            transform.position += transform.forward * positionSpeed;

            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.Walk);
            }
        }
        // 後ろ
        if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        {
            //_rb.AddForce(-transform.forward * positionSpeed);
            transform.position -= transform.forward * positionSpeed;

            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.Walk);
            }
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button11))
        {

        }

        if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyUp(KeyCode.JoystickButton2))
        {
           if (!audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.StopSound(audioManager.seAudioSource);
            }
        }
    }
}
