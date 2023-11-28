using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �쐬�ҁF�R����
// �v���C���[�𑫓��݂ňړ����鏈���̃\�[�X�R�[�h

public class PlayerWalkManager : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField,Range(0,100)]
    private float _moveSpeed;

    [SerializeField]
    private GameObject _playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraForward = Vector3.Scale(_playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Vector3 move = cameraForward * StandStill.powerSource;

        _rb.velocity = transform.forward * 1 * _moveSpeed;

        Debug.Log("PlayerWalkManager._rb.velocity : "+_rb.velocity);

        //transform.position += transform.forward * StandStill.powerSource * _moveSpeed;

        //Debug.Log("transform.position : " + transform.position);
    }
}
