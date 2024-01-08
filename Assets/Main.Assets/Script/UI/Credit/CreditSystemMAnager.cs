using UnityEngine;
using UnityEngine.SceneManagement;

// 作成者：山﨑晶
// クレジット画面のボタンを押したときの処理

public class CreditSystemMAnager : MonoBehaviour
{
    [Header("=== Script ===")]
    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    // Update is called once per frame
    void Update()
    {
        // Aボタンが押された場合
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {
            // クリック音を再生する
            _audioSystem.PlaySESound(SEData.SE.ClickButton);

            // BGMを止める
            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            // タイトル画面に遷移する
            SceneManager.LoadScene(0);
        }
    }
}
