using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ϋq�̋���
// �쐬�ҁF�n����

public class NPCManager : MonoBehaviour
{
    // ���̃I�u�W�F�N�g�̍��W���擾
    Vector3 _pos;
    // Vector3(0, 0, 0) ���擾
    Vector3 velocity = Vector3.zero;
    // _pos�֓��B����܂ł̂����悻�̎��Ԃ̕ϐ�
    public float smoothTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        // SmoothDamp(���݈ʒu, �ړI�n, ���݂̑��x, target �֓��B����܂ł̂����悻�̎��ԁB�l���������قǁAtarget �ɑ������B)
        transform.position = Vector3.SmoothDamp(current, _pos, ref velocity, smoothTime);
    }
}
