using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �t���[�����[�g�̕\��

public class FPSDisplay : MonoBehaviour
{
    // �t���[�����[�g
    float _fps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _fps = 1f / Time.deltaTime;
        //Debug.Log(_fps.ToString("F2"));
    }
}
