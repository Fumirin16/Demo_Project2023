using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _maoObj;

    [SerializeField]
    private GameObject _ranObj;

    [SerializeField]
    private Animator _selectAnimator;

    private GameObject _buttonObj;

    private Vector3 _fowerdPosition = new Vector3(0, 3.7f, 9f);

    private Vector3 _backPosition;

    private bool _isMao = false;

    private bool _isRan = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ¶@‚P@@‰E[‚P
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        // ¶‚É“|‚µ‚½‚Æ‚«
        if (inputHorizontal > 0.1)
        {
            if (!_isRan)
            {
                _isRan = true;

                _selectAnimator.SetBool("isRan", true);
            }

            if (_isMao)
            {
                _isMao = false;

                _selectAnimator.SetBool("isMao", false);
            }
        }

        if (inputHorizontal < -0.1)
        {
            if (!_isMao)
            {
                _isMao = true;

                _selectAnimator.SetBool("isMao", true);
            }

            if (_isRan)
            {
                _isRan = false ;

                _selectAnimator.SetBool("isRan", false);
            }
        }

        if (inputVertical < -0.1)
        {
            if (_isRan)
            {
                _isRan = false;

                _selectAnimator.SetBool("isRan", false);
            }

            if (_isMao)
            {
                _isMao = false;

                _selectAnimator.SetBool("isMao", false);
            }
        }
    }
}
