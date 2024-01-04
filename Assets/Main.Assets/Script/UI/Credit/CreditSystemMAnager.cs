using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditSystemMAnager : MonoBehaviour
{
    [SerializeField]
    private AudioManager _audioSystem;

    [SerializeField]
    private TranstionScenes _transSystem;

    // Start is called before the first frame update
    void Start()
    {
        _audioSystem.PlayBGMSound(BGMData.BGM.Title);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            _audioSystem.PlaySESound(SEData.SE.ClickButton);

            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            _transSystem.Trans_Scene(0);
        }
    }
}
