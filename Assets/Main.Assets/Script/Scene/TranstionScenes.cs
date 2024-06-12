using UnityEngine;
using UnityEngine.SceneManagement;

// 作成者：山﨑晶 
// シーン遷移のソースコード

public class TranstionScenes : MonoBehaviour
{
    private const int _toMain = 4;

    // シーン遷移
    public void Trans_Scene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // リトライ
    public void Trans_Retry()
    {
        SceneManager.LoadScene(_toMain);
    }

    // ゲーム終了   
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
