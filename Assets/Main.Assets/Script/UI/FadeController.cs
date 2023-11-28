using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// 作成者：地引翼
// メイン画面のフェードイン

public class FadeController : MonoBehaviour
{
    // 不要なので消しました。by山�ｱ晶
    //AudioSource audioSource;
    //[Tooltip("ここにブザー音を入れる")]
    //[SerializeField] AudioClip buzzerClip;
    [Tooltip("いじらない")]
    [SerializeField] UITimer _timer;
    [Tooltip("いじらない")]
    [SerializeField] AroundGuardsmanController _controller;

    // フェードインにかかる時間（秒）★変更可
    [Tooltip("フェードインにかかる時間")]
    [SerializeField] const float _fadeTime = 1.0f;

    // ループ回数（0はエラー）★変更可
    [Tooltip("ループ回数、数が多いと滑らかになる")]
    [SerializeField] const int _loopCount = 60;

    [SerializeField] TextMeshProUGUI _countdownText;
    [SerializeField] Image _countdownImage;
    [SerializeField] Image _fadePanel;

    public NavMeshAgent guardsman;

    float _countdown = 4f;
    int _count;

    // Start is called before the first frame update
    void Start()
    {
        guardsman = guardsman.GetComponent<NavMeshAgent>();

        //UITimer,AroundGuardsmanControllerを一時停止する
        _timer.enabled = false;
        _controller.enabled = false;
        _fadePanel.enabled = true;
        guardsman.enabled = false;


        // 不要なので消しました。by山�ｱ晶
        //audioSource = GetComponent<AudioSource>();

        //フェードインコルーチンスタート
        StartCoroutine("Color_FadeIn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Color_FadeIn()
    {
        //音楽を鳴らす
        // SEのブザー音を再生します。by山�ｱ晶
        //audioSource.PlayOneShot(buzzerClip);
        AudioManager.audioManager.Play_SESound(SESoundData.SE.Buzzer);

        //終了まで待機
        // 曲が流れているかチェックする関数を呼び、曲が流れ終わったらこの関数は「false」の値を持つのでこの書き方にしています。by 山�ｱ晶
        yield return new WaitWhile(() => (!AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource)));

        // 画面をフェードインさせるコールチン

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード元の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // ウェイト時間算出
        float wait_time = _fadeTime / _loopCount;

        // 色の間隔を算出
        float alpha_interval = 255.0f / _loopCount;

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }

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
                //UITimer,AroundGuardsmanControllerを再生する
                _timer.enabled = true;
                _controller.enabled = true;
                guardsman.enabled = true;

                _countdownText.gameObject.SetActive(false);
                _countdownImage.gameObject.SetActive(false);

                // BGMを再生する by山�ｱ晶
                AudioManager.audioManager.Play_BGMSound(BGMSoundData.BGM.Main);

                yield break;
            }
            yield return null;
        }

    }
}
