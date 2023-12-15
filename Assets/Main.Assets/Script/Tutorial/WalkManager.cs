using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class WalkManager : MonoBehaviour
{
    // �J�E���g�̃e�L�X�g
    public TextMeshProUGUI _Text;
    // ������I�������
    private bool isAudioEnd;

    bool SEflag = true;
    // audio�t����
    [SerializeField] AudioManager _audioManager;
    // �p�l�����\���ɂ���
    [SerializeField] GameObject _walkPanel;

    public StandStill _standStill;

    public TutorialManager _tutorialManager;

    public GameObject obj;
    public GameObject _stobj;

    void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.WalkVoice);
        Debug.Log("walk");
    }

    // Start is called before the first frame update
    void Start()
    {
        _standStill = _stobj.GetComponent<StandStill>();
        _tutorialManager = obj.GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Text.text = _standStill.WalkCount.ToString();

        if(_standStill.WalkCount > 3)
        {
            _Text.text = "OK";
        }
        if (SEflag && _standStill.WalkCount > 3)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd || Input.GetKeyDown(KeyCode.Space))
        {
            _walkPanel.SetActive(false);
            _tutorialManager._phaseCount++;
        }

    }
}
