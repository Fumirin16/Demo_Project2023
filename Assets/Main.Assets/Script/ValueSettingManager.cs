using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者；山﨑晶
// 値を管理するアセット

[CreateAssetMenu]
public class ValueSettingManager : ScriptableObject
{
    #region ---Fields---

    // === Player ===
    [Header("=== PLAYER MOCOPI ===")]
    [Range(0f,1000f),Tooltip("足踏みしたときのプレイヤーの速さ")]
    public float MOCOPI_PlayerMoveSpeed = 1f;

    [Range(0f,5f),Tooltip("足踏みしたときの床と足の距離が離れたときの判定をする値")]
    public float FootDistanceFoor = 0.1f;

    [Header("=== PLAYER JOYSTIC ===")]
    [Range(0f, 10f),Tooltip("コントローラーで操作したときにプレイヤーが回転する速さ")]
    public float PlayerRotateSpeed = 0.5f;

    [Range(0f, 10f),Tooltip("コントローラーで操作したときにプレイヤーが動く速さ")]
    public float JOYSTIC_PlayerMoveSpeed = 0.5f;

    // === NPC ===
    [Header("=== NPC ===")]
    /// <summary>
    /// 何秒で到達したかの変数
    /// </summary>
    [Range(0f,10f),Tooltip("何秒で到達したか")]
    public float smoothTime = 1.0f;

    // === GuardMan ===
    [Header("=== GUARDMAN ===")]
    [Range(0f,100f),Tooltip("警備員の動きの速さの値（NavMeshAgent->Speed）")]
    public float guardMoveSpeed = 2f;

    [Range(0f, 1000f),Tooltip("警備員の回転の速さの値（NavMeshAgent->AngularSpeed）")]
    public float guardAngularSpeed = 120f;

    [Range(0f, 100f),Tooltip("警備員の最高加速度の値（NavMeshAgent->Acceleration）")]
    public float guardAcceleration = 8f;

    // === Audio ===
    [Header("=== AUDIO ===")]
    /// <summary>
    /// BGM、SEを含めた全体音量の変数
    /// </summary>
    [Range(0f, 1f),Tooltip("BGM/SEを含めた全体の音量を調節する値")]
    public float masterVolume = 1;

    /// <summary>
    /// BGMの全体音量の変数
    /// </summary>
    [ Range(0f, 1f),Tooltip("BGMの全体の音量を調節する値")]
    public float bgmMasterVolume = 1;

    /// <summary>
    /// SEの全体音量の変数
    /// </summary>
    [Range(0f, 1f),Tooltip("SEの全体の音量を調節する値")]
    public float seMasterVolume = 1;

    /// <summary>
    /// BGMの音声データのリスト
    /// </summary>
    [Tooltip("BGMの音声データ")]
    public List<BGMData> bgmSoundDatas;

    /// <summary>
    /// SEの音声データのリスト
    /// </summary>
    [Tooltip("SEの音声データ")]
    public List<SEData> seSoundDatas;

    // === MainGameUI ===
    [Header("=== UI TIMER ===")]
    [Range(0f, 180f),Tooltip("メインゲームの制限時間")]
    public float GameLimitTime = 90f;

    // === InGame ===
    /// <summary>
    /// ゲームオーバーの判定を保存する変数
    /// </summary>
    [HideInInspector]
    public bool gameOver;

    /// <summary>
    /// ゲームクリアの判定を保存する変数
    /// </summary>
    [HideInInspector]
    public bool gameClear;

    #endregion ---Fields---
}

/// <summary>
/// BGMの音声データ
/// </summary>
[Serializable]
public class BGMData
{
    /// <summary>
    /// BGMのラベル
    /// </summary>
    public enum BGM
    {
        Title,
        Tutorial,
        Main,
        ClearEnd,
        OverEnd,
    }

    /// <summary>
    /// 列挙型の宣言
    /// </summary>
    public BGM bgm;

    /// <summary>
    /// BGMのAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// BGMの音量
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

/// <summary>
/// SEの音声データ
/// </summary>
[Serializable]
public class SEData
{
    /// <summary>
    /// SEのラベル
    /// </summary>
    public enum SE
    {
        Audience,
        Shutters,
        Buzzer,
        ClickButton,
        FoundSecurity,
        Correct,
        Squwat,
        Walk,
        voice,
    }

    /// <summary>
    /// 列挙型の宣言
    /// </summary>
    public SE se;

    /// <summary>
    /// SEのAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// SEの音量
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

