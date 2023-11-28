using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �쐬�ҁF�R����   
//  BGM�ASE�𐧌䂷��\�[�X�R�[�h

public class AudioManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// BGM  AudioSource
    /// </summary>
    [SerializeField]
    public AudioSource bgmAudioSource;

    /// <summary>
    /// SE  AudioSource
    /// </summary>
    [SerializeField]
    public AudioSource seAudioSource;

    [Space(10)]

    /// <summary>
    /// BGM�ASE���܂߂��S�̉��ʂ̕ϐ�
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _masterVolume = 1;

    /// <summary>
    /// BGM�̑S�̉��ʂ̕ϐ�
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _bgmMasterVolume = 1;

    /// <summary>
    /// SE�̑S�̉��ʂ̕ϐ�
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _seMasterVolume = 1;

    [Space(10)]

    /// <summary>
    /// BGM�̉����f�[�^�̃��X�g
    /// </summary>
    [SerializeField]
    private List<BGMSoundData> _bgmSoundDatas;

    /// <summary>
    /// SE�̉����f�[�^�̃��X�g
    /// </summary>
    [SerializeField]
    private List<SESoundData> _seSoundDatas;

    #endregion ---Fields---

    #region ---Properties---

    // AudioManager�̃C���X�^���X���쐬   
    public static AudioManager audioManager { get; private set; }

    #endregion ---Properties---

    #region ---Methods---

    /// <summary>
    /// BGM���Đ�����֐�
    /// </summary>
    /// <param name="bgm">  ?����BGM�̃^�C�g��(�񋓌^) </param>
    public void Play_BGMSound(BGMSoundData.BGM bgm)
    {
        // �����f�[�^����Y������f�[�^��ۑ�����   
        BGMSoundData data = _bgmSoundDatas.Find(data => data.bgm == bgm);

        // AudioClip��AudioSource�ɐݒ肷��
        bgmAudioSource.clip = data.audioClip;

        // BGM�̉��ʂ�ݒ肷��
        bgmAudioSource.volume = data.volume * _bgmMasterVolume * _masterVolume;

        // �Đ�����
        bgmAudioSource.Play();
    }

    /// <summary>
    /// SE���Đ�����֐�
    /// </summary>
    /// <param name="se">  ?����SE�̃^�C�g��(�񋓌^) </param>
    public void Play_SESound(SESoundData.SE se)
    {
        // �����f�[�^����Y������f�[�^��ۑ�����
        SESoundData data = _seSoundDatas.Find(data => data.se == se);

        // SE�̉��ʂ�ݒ肷��
        seAudioSource.volume = data.volume * _seMasterVolume * _masterVolume;

        // SE���Đ�����
        seAudioSource.PlayOneShot(data.audioClip);
    }

    /// <summary>
    /// �����Đ����Ă��邩�𒲂ׂ�֐�
    /// </summary>
    /// <param name="audioSource"> ���ׂ���AudioSource </param>
    /// <returns> �����Đ�����������false / �����~�܂��Ă�����true </returns>
    public bool CheckPlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    ///  ?    ?  ?     ~ ? ? 
    /// </summary>
    /// <param name="audioSource">  ~ ?   AudioSource </param>
    public void Stop_Sound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    /// <summary>
    /// BGM ?  ? ?X    ? 
    /// </summary>
    /// <param name="soundVolume">  ?X           </param>
    public void Change_BGMVolume(float soundVolume)
    {
        bgmAudioSource.volume = soundVolume * _bgmMasterVolume * _masterVolume;
    }

    /// <summary>
    /// SE ?  ? ?X    ? 
    /// </summary>
    /// <param name="soundVolume">  ?X           </param>
    public void Change_SEVolume(float soundVolume)
    {
        seAudioSource.volume = soundVolume * _seMasterVolume * _masterVolume;
    }

    #endregion ---Methods---
}

#region ---Class---

/// <summary>
/// BGM ?    f [ ^ N   X
/// </summary>
[System.Serializable]
public class BGMSoundData
{
    /// <summary>
    /// BGM ?    ^ C g  
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
    ///  ??^ ??
    /// </summary>
    public BGM bgm;

    /// <summary>
    /// BGM  AudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// BGM ?   
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

/// <summary>
/// SE ?    f [ ^ N   X
/// </summary>
[System.Serializable]
public class SESoundData
{
    /// <summary>
    /// SE ?    ^ C g  
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
    ///  ??^ ??
    /// </summary>
    public SE se;

    /// <summary>
    /// SE  AudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// SE ?   
    /// </summary>
    [Range(0f, 2f)]
    public float volume = 1;
}

#endregion ---Class---