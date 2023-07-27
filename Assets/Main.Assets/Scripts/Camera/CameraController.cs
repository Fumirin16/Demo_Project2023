using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

//�J�����R���g���[���[�X�N���v�g
//�쐬�ҁF�~�X䝗D
//�㉺120�����E���x����

public class CameraController : MonoBehaviour
{
    /*
    �ぁ( 0,1.0)
    ����(0,-1.0)
    �E��( 1.0,0)
    ����(-1.0,0)
     */
    private float moveX;
    private float moveY;
    [SerializeField] float viewAngle;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnMove(InputValue moveValue)
    {
        var movementVector = moveValue.Get<Vector2>();

        moveX = movementVector.x;
        moveY = movementVector.y;
      //  Debug.Log(moveX);
    }
    void Update()
    {
        Rotate(-moveY, -moveX, viewAngle);
    }

    void Rotate(float inputX, float inputY, float limit)
    {
        float maxlimit = limit, minLimit = 360 - maxlimit;

        //X����]
        var localAngle = transform.localEulerAngles;
        localAngle.x += inputY;
        if (localAngle.x > maxlimit && localAngle.x < 180)
            localAngle.x = maxlimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y����]
        var angle = transform.eulerAngles;
        angle.y += inputX;
        transform.eulerAngles = angle;

    }
}
