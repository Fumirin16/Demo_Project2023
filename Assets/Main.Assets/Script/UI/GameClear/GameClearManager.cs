using System.Collections;
using TMPro;
using UnityEngine;

// 作成者：山﨑晶
// ゲームクリア画面のUI演出のソースコード

public class GameClearManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  「FadeManager」を参照する変数
    /// </summary>
    [SerializeField]
    private FadeManager _fadeManager;

    /// <summary>
    ///  「TranstionScenes」を参照する変数
    /// </summary>
    [SerializeField]
    private TranstionScenes _transSystem;

    /// <summary>
    /// AudioManager
    /// </summary>
    [SerializeField]
    private AudioManager audioManager;

    /// <summary>
    /// 値を参照するために取得する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager settingManager;

    /// <summary>
    /// 背景画像のフェードの設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeOut;

    /// <summary>
    /// チェキ枠のフェードの設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _chekiFadeOut;

    /// <summary>
    ///  シーン遷移時のフェードの設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    /// アイドル画像を取得する変数
    /// </summary>
    [SerializeField]
    private RectTransform[] _idolImage = new RectTransform[2];

    /// <summary>
    /// プレイヤー画像を取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// スライドインの初期位置を保存する変数
    /// </summary>
    private Vector2[] _startPosition = new Vector2[2];

    /// <summary>
    /// スライドインの最終位置を保存する変数
    /// </summary>
    [SerializeField]
    private Vector2[] _endPosition = new Vector2[2];

    /// <summary>
    /// スライドインの初期位置と最終位置の距離を保存する変数
    /// </summary>
    private float[] _distance = new float[2];

    /// <summary>
    /// 時間を保存する変数
    /// </summary>
    private float _time;

    /// <summary>
    /// スライドインの速さ
    /// </summary>
    [SerializeField, Range(0f, 100f)]
    private float _moveSpeed;

    /// <summary>
    /// カウントダウンの時間を保存する変数
    /// </summary>
    [SerializeField, Range(0, 10f)]
    private float _countDownTime;

    /// <summary>
    /// カウントダウンを表示するテキストを取得する変数
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _countText;

    /// <summary>
    /// 最期に表示するテキストを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _lastText;

    /// <summary>
    /// チェキを撮影してからの待ち時間
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float _changeSpeed;

    /// <summary>
    /// UIの演出処理の順番を保存する変数
    /// </summary>
    private int _uiCount;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        Initi_UI();
    }

    // Update is called once per frame
    void Update()
    {
        direction_UI();
    }

    /// <summary>
    /// UIの初期化する関数
    /// </summary>
    private void Initi_UI()
    {
        // アイドル画像の初期化  
        for (int i = 0; i < _idolImage.Length; i++)
        {
            _startPosition[i] = _idolImage[i].anchoredPosition;
            _distance[i] = Vector2.Distance(_startPosition[i], _endPosition[i]);
        }

        // UI演出順番の初期化    
        _uiCount = 0;
    }

    /// <summary>
    /// UI演出処理の関数
    /// </summary>
    private void direction_UI()
    {
        switch (_uiCount)
        {
            // 背景画像をフェードする 
            case 0:
                _fadeManager.FadeOut(_backGroundFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _uiCount++;
                }
                break;

            // チェキ枠をフェードする
            case 1:
                _fadeManager.FadeOut(_chekiFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _time = Time.time;
                    _uiCount++;
                }
                break;

            // アイドル画像をスライドインする
            case 2:
                Move_IdolImage();
                break;

            // プレイヤー画像を表示する 
            case 3:
                _playerImage.SetActive(true);
                _uiCount++;
                break;

            // カウントダウンを表示する
            case 4:
                CountDown_Text();
                break;

            // チェキを撮る演出をする
            case 5:
                StartCoroutine(ShotPhoto());
                break;

            // シーン遷移のフェードをする
            case 6:
                _fadeManager.FadeOut(_endFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _transSystem.Trans_Scene(0);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// アイドルをスライドインさせる関数
    /// </summary>
    private void Move_IdolImage()
    {
        float _positionValue;

        for (int i = 0; i < _idolImage.Length; i++)
        {
            //  移動する位置を設定する
            _positionValue = ((Time.time - _time) / _distance[i]) * _moveSpeed;

            // ２点の距離のどこに画像を移動させるかを設定する
            _idolImage[i].anchoredPosition = Vector2.Lerp(_startPosition[i], _endPosition[i], _positionValue);

            //  アイドル画像が終了位置に着いた場合
            if ((_idolImage[0].anchoredPosition == _endPosition[0]) && (_idolImage[1].anchoredPosition == _endPosition[1]))
            {
                _lastText.SetActive(false);
                _uiCount++;
            }
        }
    }

    /// <summary>
    /// カウントダウンの関数
    /// </summary>
    private void CountDown_Text()
    {
        // カウントダウンを整数で保存する変数
        int _countDownText;

        // 時間を取得
        _countDownTime -= Time.deltaTime;

        // 時間を整数に変換
        _countDownText = (int)_countDownTime;

        // 変換した時間をテキストとして表示
        _countText.text = (_countDownText + 1).ToString();

        // 時間が０になった場合
        if ((int)_countDownTime < 0)
        {
            _countText.enabled = false;
            _uiCount++;
        }
    }

    /// <summary>
    /// チェキを取る演出の関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotPhoto()
    {
        //　音声を再生する
        if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        {
            audioManager.PlaySESound(SEData.SE.Shutters);
        }

        // 待ち時間  
        yield return new WaitForSeconds(_changeSpeed);

        // 最期のテキストを表示する 
        _lastText.SetActive(true);

        // UI演出の順番を進める
        if (_uiCount == 5)
        {
            _uiCount++;
        }

        // 終了する
        yield return null;
    }

    #endregion ---Methods---
}