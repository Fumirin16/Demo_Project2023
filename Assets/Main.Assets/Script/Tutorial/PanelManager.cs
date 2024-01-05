using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.B))
        {
            if(flag)
            {
                gameObject.SetActive(false);
                flag = false;
            }
        }
    }
}
