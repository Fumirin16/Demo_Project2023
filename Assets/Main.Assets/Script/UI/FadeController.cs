using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class FadeController : MonoBehaviour
{
    [SerializeField] UITimer _timer;
    [SerializeField] AroundGuardsmanController _controller;
    [SerializeField] AroundGuardsmanController _controller2;
    [SerializeField] AudioManager _audio;
    [SerializeField] TextMeshProUGUI _countdownText;
    [SerializeField] Image _countdownImage;
    [SerializeField] Image _fadePanel;
    [SerializeField] Animator _animator;
    [SerializeField] NavMeshAgent guardsman;
    [SerializeField] NavMeshAgent guardsman2;
    [SerializeField] Animator _maoAnim;
    [SerializeField] Animator _ranAnim;
    float _countdown = 4f;
    int _count;

    void Start()
    {
        _timer.enabled = false;
        _fadePanel.enabled = true;
        guardsman.enabled = false;
        guardsman2.enabled = false;
        _controller.enabled = false;
        _controller2.enabled = false;
        _audio.PlaySESound(SEData.SE.Buzzer);
    }

    void Update()
    {
        if (_audio.CheckPlaySound(_audio.seAudioSource))
        {
            _animator.Play("FadeIn");
        }
    }

    public void Fade()
    {
        StartCoroutine("Color_FadeIn");
    }

    IEnumerator Color_FadeIn()
    {
        _countdownText.gameObject.SetActive(true);
        _countdownImage.gameObject.SetActive(true);

        while (_countdown > 0)
        {
            _countdown -= Time.deltaTime;
            _countdownImage.fillAmount = _countdown % 1.0f;
            _count = (int)_countdown;
            _countdownText.text = _count.ToString();

            if (_countdown <= 0)
            {
                _timer.enabled = true;
                guardsman.enabled = true;
                guardsman2.enabled = true;
                _controller.enabled = true;
                _controller2.enabled = true;
                _maoAnim.Play("dance");
                _ranAnim.Play("dance");

                _countdownText.gameObject.SetActive(false);
                _countdownImage.gameObject.SetActive(false);

                _audio.PlayBGMSound(BGMData.BGM.Main);

                yield break;
            }
            yield return null;
        }
    }
}