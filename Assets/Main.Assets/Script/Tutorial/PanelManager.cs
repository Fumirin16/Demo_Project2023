using UnityEngine;

// B�{�^������������p�l�������

public class PanelManager : MonoBehaviour
{
    /// <summary>
    /// �G���A�ɓ�������
    /// </summary>
    bool _flag = true;

    void OnEnable()
    {
        _flag = true;
    }

    void Update()
    {
        if(_flag && (Input.GetKeyDown (KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.B)))
        {
            if(_flag)
            {
                gameObject.SetActive(false);
                _flag = false;
            }
        }
    }
}