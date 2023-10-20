using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Inspector�ɕ\������Ԋu�̒���
    private const int spaceValue = 8;

    // BGM��炷AudioSource���擾
    [SerializeField]private  AudioSource _bgmAudioSource;
    // SE��炷AudioSource���擾
    [SerializeField] private  AudioSource _seAudioSource;
    
    [Space(spaceValue)]

    // BGM�ASE���܂߂đS�̂̉��ʂ�ݒ肷��
    [Range(0f, 1f)] public float masterVolume = 1;�@
    // BGM�̑S�̂̉��ʂ�ݒ肷��
    [Range(0f, 1f)] public float bgmMasterVolume = 1;
    // SE�̑S�̂̉��ʂ�ݒ肷��
    [Range(0f, 1f)] public float seMasterVolume = 1;

    [Space(spaceValue)]

    // BGM�̉�����ۑ�����
    [SerializeField] private List<BGMSoundData> _bgmSoundData;
    // SE�̉�����ۑ�����
    [SerializeField] private List<SESoundData> _seSoundData;

    // 
    public static AudioManager audioManager { get; private set; }

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = _bgmSoundData.Find(data => data.bgm == bgm);
        _bgmAudioSource.clip = data.audioClip;
        _bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        _bgmAudioSource.Play();
    }

    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = _seSoundData.Find(data => data.se == se);
        _seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        _seAudioSource.PlayOneShot(data.audioClip);
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        Tutorial,
        Main,
        Clear,
        Over,
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        ClickButton,
        Walk,
        Squat,
        Correct,
        Found,
        Buzzer,
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume = 1;

}

