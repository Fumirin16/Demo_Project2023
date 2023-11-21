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
    Vector3 _velocity = Vector3.zero;
    // _pos�֓��B����܂ł̂����悻�̎��Ԃ̕ϐ�
    [Tooltip("�l���������قǑ����Ȃ�")]
    public float smoothTime = 0.1f;

    Rigidbody _rb;

    [SerializeField] GameObject _head;
    [SerializeField] GameObject _rhand;

    public float _attackRange;

    float _distance;


    // Start is called before the first frame update
    void Start()
    {
        _pos = this.transform.position;
        //Rigidbody���擾
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = _head.transform.position.y - _rhand.transform.position.y;


        Vector3 _current = transform.position;
        // SmoothDamp(���݈ʒu, �ړI�n, ���݂̑��x, _target �֓��B����܂ł̂����悻�̎��ԁB�l���������قǁA_target �ɑ������B)
        transform.position = Vector3.SmoothDamp(_current, _pos, ref _velocity, smoothTime);
        //�ړ�����]�����Ȃ��悤�ɂ���
        _rb.constraints = RigidbodyConstraints.FreezeAll;

        if(Input.GetKey(KeyCode.JoystickButton14))
        {
            Attack();
        }
        if (_distance < _attackRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation
                        | RigidbodyConstraints.FreezePositionY;

        //Debug.Log("�U��");
    }
}
