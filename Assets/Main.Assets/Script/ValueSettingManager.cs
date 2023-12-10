using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ì¬ÒGRú±»
// lðÇ·éAZbg

[CreateAssetMenu]
public class ValueSettingManager : ScriptableObject
{
    #region ---Fields---

    // === Player ===
    [Header("=== PLAYER MOCOPI ===")]
    [Range(0f,100f),Tooltip("«¥Ýµ½Æ«ÌvC[Ì¬³")]
    public float MOCOPI_PlayerMoveSpeed = 1f;

    [Range(0f,5f),Tooltip("«¥Ýµ½Æ«Ì°Æ«Ì£ª£ê½Æ«Ì»èð·él")]
    public float FootDistanceFoor = 0.1f;

    [Header("=== PLAYER JOYSTIC ===")]
    [Range(0f, 10f),Tooltip("Rg[[Åìµ½Æ«ÉvC[ªñ]·é¬³")]
    public float PlayerRotateSpeed = 0.5f;

    [Range(0f, 10f),Tooltip("Rg[[Åìµ½Æ«ÉvC[ª®­¬³")]
    public float JOYSTIC_PlayerMoveSpeed = 0.5f;

    // === NPC ===
    [Header("=== NPC ===")]
    /// <summary>
    /// ½bÅBµ½©ÌÏ
    /// </summary>
    [Range(0f,10f),Tooltip("½bÅBµ½©")]
    public float smoothTime = 1.0f;

    // === GuardMan ===
    [Header("=== GUARDMAN ===")]
    [Range(0f,100f),Tooltip("xõõÌ®«Ì¬³ÌliNavMeshAgent->Speedj")]
    public float guardMoveSpeed = 2f;

    [Range(0f, 1000f),Tooltip("xõõÌñ]Ì¬³ÌliNavMeshAgent->AngularSpeedj")]
    public float guardAngularSpeed = 120f;

    [Range(0f, 100f),Tooltip("xõõÌÅÁ¬xÌliNavMeshAgent->Accelerationj")]
    public float guardAcceleration = 8f;

    // === Audio ===
    [Header("=== AUDIO ===")]
    /// <summary>
    /// BGMASEðÜß½SÌ¹ÊÌÏ
    /// </summary>
    [Range(0f, 1f),Tooltip("BGM/SEðÜß½SÌÌ¹Êð²ß·él")]
    public float masterVolume = 1;

    /// <summary>
    /// BGMÌSÌ¹ÊÌÏ
    /// </summary>
    [ Range(0f, 1f),Tooltip("BGMÌSÌÌ¹Êð²ß·él")]
    public float bgmMasterVolume = 1;

    /// <summary>
    /// SEÌSÌ¹ÊÌÏ
    /// </summary>
    [Range(0f, 1f),Tooltip("SEÌSÌÌ¹Êð²ß·él")]
    public float seMasterVolume = 1;

    /// <summary>
    /// BGMÌ¹ºf[^ÌXg
    /// </summary>
    [Tooltip("BGMÌ¹ºf[^")]
    public List<BGMData> bgmSoundDatas;

    /// <summary>
    /// SEÌ¹ºf[^ÌXg
    /// </summary>
    [Tooltip("SEÌ¹ºf[^")]
    public List<SEData> seSoundDatas;

    // === MainGameUI ===
    [Header("=== UI TIMER ===")]
    [Range(0f, 180f),Tooltip("CQ[Ì§ÀÔ")]
    public float GameLimitTime = 90f;

    // === InGame ===
    /// <summary>
    /// Q[I[o[Ì»èðÛ¶·éÏ
    /// </summary>
    [HideInInspector]
    public bool gameOver;

    /// <summary>
    /// Q[NAÌ»èðÛ¶·éÏ
    /// </summary>
    [HideInInspector]
    public bool gameClear;

    #endregion ---Fields---
}

/// <summary>
/// BGMÌ¹ºf[^
/// </summary>
[Serializable]
public class BGMData
{
    /// <summary>
    /// BGMÌx
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
    /// ñ^Ìé¾
    /// </summary>
    public BGM bgm;

    /// <summary>
    /// BGMÌAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// BGMÌ¹Ê
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

/// <summary>
/// SEÌ¹ºf[^
/// </summary>
[Serializable]
public class SEData
{
    /// <summary>
    /// SEÌx
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
    }

    /// <summary>
    /// ñ^Ìé¾
    /// </summary>
    public SE se;

    /// <summary>
    /// SEÌAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// SEÌ¹Ê
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

