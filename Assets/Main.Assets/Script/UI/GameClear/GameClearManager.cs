using System.Collections;
using TMPro;
using UnityEngine;

// 作成者：山﨑晶
// ゲームクリア画面のUI演出のソースコード

public class GameClearManager : MonoBehaviour
{
    #region ---Fields---

    [SerializeField]
    private float _transTIme=600f;

    [SerializeField]
    private TranstionScenes _transSystem;

    private float time;
    #endregion ---Fields---

    #region ---Methods---

    private void Update()
    {
        time ++;
        if (time >= _transTIme)
        {
            _transSystem.Trans_Scene(0);
        }

        if (Input.GetKey(KeyCode.JoystickButton14))
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                _transSystem.Trans_Scene(0);
            }
        }
    }

    #endregion ---Methods---
}