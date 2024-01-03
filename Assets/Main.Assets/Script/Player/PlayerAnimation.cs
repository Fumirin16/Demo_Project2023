using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] ValueSettingManager _settingManager;

    [SerializeField] Animator _guardsmanAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoGameOver()
    {
        _settingManager.gameOver = true;
        _guardsmanAnim.enabled = false;
    }
}