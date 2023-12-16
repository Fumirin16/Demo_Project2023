using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁG�R����
// �l���Ǘ�����A�Z�b�g

[CreateAssetMenu]
public class ValueSettingManager : ScriptableObject
{
    #region ---Fields---

    // === Player ===
    [Header("=== PLAYER MOCOPI ===")]
    [Range(0f,1000f),Tooltip("�����݂����Ƃ��̃v���C���[�̑���")]
    public float MOCOPI_PlayerMoveSpeed = 1f;

    [Range(0f,5f),Tooltip("�����݂����Ƃ��̏��Ƒ��̋��������ꂽ�Ƃ��̔��������l")]
    public float FootDistanceFoor = 0.1f;

    [Header("=== PLAYER JOYSTIC ===")]
    [Range(0f, 10f),Tooltip("�R���g���[���[�ő��삵���Ƃ��Ƀv���C���[����]���鑬��")]
    public float PlayerRotateSpeed = 0.5f;

    [Range(0f, 10f),Tooltip("�R���g���[���[�ő��삵���Ƃ��Ƀv���C���[����������")]
    public float JOYSTIC_PlayerMoveSpeed = 0.5f;

    // === NPC ===
    [Header("=== NPC ===")]
    /// <summary>
    /// ���b�œ��B�������̕ϐ�
    /// </summary>
    [Range(0f,10f),Tooltip("���b�œ��B������")]
    public float smoothTime = 1.0f;

    // === GuardMan ===
    [Header("=== GUARDMAN ===")]
    [Range(0f,100f),Tooltip("�x�����̓����̑����̒l�iNavMeshAgent->Speed�j")]
    public float guardMoveSpeed = 2f;

    [Range(0f, 1000f),Tooltip("�x�����̉�]�̑����̒l�iNavMeshAgent->AngularSpeed�j")]
    public float guardAngularSpeed = 120f;

    [Range(0f, 100f),Tooltip("�x�����̍ō������x�̒l�iNavMeshAgent->Acceleration�j")]
    public float guardAcceleration = 8f;

    // === GuardMan ===
    [Header("=== TUTORIAL ===")]
    [Tooltip("")]
    public int ClearCount;

    [Tooltip("")]
    public float ClearTime;

    [Tooltip("")]
    public int ClearHuman;

    // === Audio ===
    [Header("=== AUDIO ===")]
    /// <summary>
    /// BGM�ASE���܂߂��S�̉��ʂ̕ϐ�
    /// </summary>
    [Range(0f, 1f),Tooltip("BGM/SE���܂߂��S�̂̉��ʂ𒲐߂���l")]
    public float masterVolume = 1;

    /// <summary>
    /// BGM�̑S�̉��ʂ̕ϐ�
    /// </summary>
    [ Range(0f, 1f),Tooltip("BGM�̑S�̂̉��ʂ𒲐߂���l")]
    public float bgmMasterVolume = 1;

    /// <summary>
    /// SE�̑S�̉��ʂ̕ϐ�
    /// </summary>
    [Range(0f, 1f),Tooltip("SE�̑S�̂̉��ʂ𒲐߂���l")]
    public float seMasterVolume = 1;

    /// <summary>
    /// BGM�̉����f�[�^�̃��X�g
    /// </summary>
    [Tooltip("BGM�̉����f�[�^")]
    public List<BGMData> bgmSoundDatas;

    /// <summary>
    /// SE�̉����f�[�^�̃��X�g
    /// </summary>
    [Tooltip("SE�̉����f�[�^")]
    public List<SEData> seSoundDatas;

    // === MainGameUI ===
    [Header("=== UI TIMER ===")]
    [Range(0f, 180f),Tooltip("���C���Q�[���̐�������")]
    public float GameLimitTime = 90f;

    // === InGame ===
    /// <summary>
    /// �Q�[���I�[�o�[�̔����ۑ�����ϐ�
    /// </summary>
    [HideInInspector]
    public bool gameOver;

    /// <summary>
    /// �Q�[���N���A�̔����ۑ�����ϐ�
    /// </summary>
    [HideInInspector]
    public bool gameClear;

    #endregion ---Fields---
}

/// <summary>
/// BGM�̉����f�[�^
/// </summary>
[Serializable]
public class BGMData
{
    /// <summary>
    /// BGM�̃��x��
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
    /// �񋓌^�̐錾
    /// </summary>
    public BGM bgm;

    /// <summary>
    /// BGM��AudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// BGM�̉���
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

/// <summary>
/// SE�̉����f�[�^
/// </summary>
[Serializable]
public class SEData
{
    /// <summary>
    /// SE�̃��x��
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
        WalkVoice,
        BodyDownVoice,
        KakiwakeVoice,
    }

    /// <summary>
    /// �񋓌^�̐錾
    /// </summary>
    public SE se;

    /// <summary>
    /// SE��AudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// SE�̉���
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

