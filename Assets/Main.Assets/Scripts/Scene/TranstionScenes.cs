// SÒFRú±»
// V[ÌJÚ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class TranstionScenes : MonoBehaviour
{
    // wTitlexV[ÉJÚ·é
    public void Trans_Title()
    {
        SceneManager.LoadScene(0);
    }

    // wWayOfPlayingxV[ÉJÚ·é
    public void Trans_WayPlay()
    {
        SceneManager.LoadScene(1);
    }

    // wMainxV[ÉJÚ·é
    public void Trans_Main()
    {
        SceneManager.LoadScene(2);
    }

    // »ÝÌV[ðÄÇÝÝ·é
    public void Trans_Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Q[ðI¹³¹é
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
