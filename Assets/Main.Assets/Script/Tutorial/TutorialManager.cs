using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] _panel;
    public int _phaseCount;

    public GameObject _bodyDownobj; 
    public GameObject _kakiwakeobj; 
    public GameObject _kakiwakeobj_2; 
    public GameObject _mobobj;

    bool _pushFlag = false;

    [SerializeField] Animator _fadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _phaseCount = Mathf.Clamp(_phaseCount, 0, 3);
        _panel[_phaseCount].SetActive(true);
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (_pushFlag == false)
            {
                _panel[_phaseCount].SetActive(false);
                _phaseCount++;
                _pushFlag = true;
            }
        }
        else
        {
            _pushFlag = false;
        }
        if (_phaseCount == 1)
        {
            _bodyDownobj.SetActive(true);
        }
        if(_phaseCount == 2)
        {
            _kakiwakeobj.SetActive(true);
            _kakiwakeobj_2.SetActive(true);
            _mobobj.SetActive(true);
        }
        if(_phaseCount == 3)
        {
            _fadeAnimator.Play("FadeOut");
        }
    }
}
