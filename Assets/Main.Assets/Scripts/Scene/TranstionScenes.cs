//SÒFRú±»
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// V[ÌJÚ
public class TranstionScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// wTitlexV[ÉJÚ·é
    //public void Trans_Title()
    //{
    //    SceneManager.LoadScene(0);
    //}

    //// wWayOfPlayingxV[ÉJÚ·é
    //public void Trans_WayPlay()
    //{
    //    SceneManager.LoadScene(1);
    //}

    //// wMainxV[ÉJÚ·é
    //public void Trans_Main()
    //{
    //    SceneManager.LoadScene(2);
    //}

    //public void Trans_GameClear()
    //{
    //    SceneManager.LoadScene(3);
    //}

    //public void Trans_GameOver()
    //{
    //    SceneManager.LoadScene(4);
    //}

    public void Trans_Scene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // »ÝÌV[ðÄÇÝÝ·é
    public void Trans_Retry()
    {
        SceneManager.LoadScene(2);
    }

    // Q[ðI¹³¹é
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
