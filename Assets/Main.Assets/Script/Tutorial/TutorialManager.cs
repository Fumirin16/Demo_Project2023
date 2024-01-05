using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] _phase;
    public int _phaseCount;

    public GameObject _bodyDownobj; 
    public GameObject _kakiwakeobj; 
    public GameObject _kakiwakeobj_2; 
    public GameObject _mobobj;

    bool _pushFlag = false;

    [SerializeField] Animator _fadeAnimator;

    // Update is called once per frame
    void Update()
    {
        _phaseCount = Mathf.Clamp(_phaseCount, 0, 3);
        _phase[_phaseCount].SetActive(true);

        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.A))
        {
            if (_pushFlag == false)
            {
                _phase[_phaseCount].SetActive(false);
                _phaseCount++;
                _pushFlag = true;
            }
        }
        else
        {
            _pushFlag = false;
        }
        switch(_phaseCount)
        {
            case 1:
                _kakiwakeobj.SetActive(true);
                _kakiwakeobj_2.SetActive(true);
                _mobobj.SetActive(true);
                break;
            case 2:
                _kakiwakeobj.SetActive(false);
                _kakiwakeobj_2.SetActive(false);
                _mobobj.SetActive(false);
                _bodyDownobj.SetActive(true);
                break;
            case 3:
                _fadeAnimator.Play("FadeOut");
                break;
                default:
                break;
        }
    }
}