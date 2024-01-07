using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 作成者：山崎晶
// アイドル紹介画面のボタンを押してタイトル画面に戻るソース

public class IdolSystemManager : MonoBehaviour
{
    /// <summary>
    /// AudioSystemのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    // Update is called once per frame
    void Update()
    {
        // Aボタンを押した場合
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {
            // クリック音を再生
            _audioSystem.PlaySESound(SEData.SE.ClickButton);

            // BGMを止める
            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            // タイトル画面に遷移する
            SceneManager.LoadScene(0);
        }
    }
}
