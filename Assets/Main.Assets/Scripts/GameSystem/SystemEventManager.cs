//SÒFRú±
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class SystemEventManager : MonoBehaviour
{
    // Q[I[o[ANAð±ÌÏÅtOÇ
    [HideInInspector]public bool gameOver;
    [HideInInspector]public bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        // tOÌú»
        gameClear = false;
        gameOver = false;

        // tOÌmFpfobN
        Debug.Log("gameOver:" + gameOver);
        Debug.Log("gameClear:" + gameClear);
    }

    // Update is called once per frame
    void Update()
    {
        // tOªutruevÉÈÁ½Ì
        if (gameOver || gameClear)
        {
            // Q[àÌÔð~ßé
            Time.timeScale = 0;

            // Q[àÌÔª~ÜÁÄ¢é©ÌmFpfobN
            Debug.Log("Time : " + Time.timeScale);
        }
    }
}
