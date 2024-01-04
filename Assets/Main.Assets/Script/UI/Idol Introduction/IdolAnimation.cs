using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _maoObj;

    [SerializeField]
    private GameObject _ranObj;

    private GameObject _buttonObj;

    private Vector3 _fowerdPosition = new Vector3(0, 3.7f, 9f);

    private Vector3 _backPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ¶@‚P@@‰E[‚P
        float inputHorizontal = Input.GetAxis("Horizontal");

        Debug.Log("inputHorizontal : " + inputHorizontal);
        // ¶‚É“|‚µ‚½‚Æ‚«
        if (inputHorizontal > 0.1)
        {

        }

        if (inputHorizontal < -0.1)
        {

        }
    }
}
