using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOff : MonoBehaviour
{
    private bool _isMouse = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9)||Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isMouse)
            {
                Cursor.visible = true;

                // �J�[�\�������R�ɓ�������
                Cursor.lockState = CursorLockMode.None;
                // �J�[�\������ʓ��œ�������
                Cursor.lockState = CursorLockMode.Confined;

                _isMouse = true;
            }

            if (_isMouse)
            {
                Cursor.visible = false;
                _isMouse = false;
            }
        }
    }
}
