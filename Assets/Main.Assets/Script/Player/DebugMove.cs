using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{
    public float positionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.JoystickButton1))
        {
            transform.position += transform.forward * positionSpeed;
        }
        if(Input.GetKey(KeyCode.JoystickButton2))
        {
            transform.position -= transform.forward * positionSpeed;
        }
        if(Input.GetKey(KeyCode.JoystickButton0))
        {
            transform.position += transform.right * positionSpeed;
        }
        if(Input.GetKey(KeyCode.JoystickButton3))
        {
            transform.position -= transform.right * positionSpeed;
        }

    }
}
