using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//　しゃがんだ時の判定

public class BodyDownManager : MonoBehaviour
{
    // 経過時間
    float _count;
    // しゃがみ出来ているかできていないか
    bool _active = true;
    // カウントのテキスト
    public TextMeshProUGUI _countTimeText;
    // 音が鳴り終わったか
    private bool isAudioEnd;
    // 音が一度だけ再生するフラグ
    bool SEflag = true;
    // パネルを非表示にする
    [SerializeField] GameObject _bodyDownPanel;
    // audio付ける
    [SerializeField] AudioManager _audioManager;

    public TutorialManager _tutorialManager;

    private void OnEnable()
    {
        _audioManager.PlaySESound(SEData.SE.BodyDownVoice);
        Debug.Log("syagami");
    }

    void Update()
    {
        if (_active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }
        _countTimeText.text = _count.ToString("F1");

        if (_count >3)
        {
            _countTimeText.text = "OK";
        }

        if (SEflag && _count > 3)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _bodyDownPanel.SetActive(false);
            gameObject.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _active = false;
            Debug.Log("ataltuteru");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _active = true;
            Debug.Log("nai");
        }
    }
}
