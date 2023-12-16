using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// タイトル画面のUI演出をするソースコード
//  作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  待ち時間を保存する変数
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float[] _intervalTIme = new float[2];

    [Space(10)]

    /// <summary>
    /// カメラの移動する速さ
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float _cameraMoveSpeed = 1f;

    /// <summary>
    ///  カメラの初期位置を保存する変数
    /// </summary>
    private Vector3 _startPosition;

    /// <summary>
    ///  カメラの移動先を保存する変数
    /// </summary>
    [SerializeField]
    private Vector3 _endPosition;

    [Space(10)]

    /// <summary>
    ///  FadeManagerを参照する変数
    /// </summary>
    [SerializeField]
    private FadeManager _fadeSystem;

    /// <summary>
    ///  背景画像のフェードを設定する
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _blackFadeIn;

    /// <summary>
    ///  タイトルロゴのフェードを設定する
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _logoFadeOut;

    /// <summary>
    ///  TranstionScenesを参照する変数
    /// </summary>
    [SerializeField]
    private TranstionScenes transSystem;

    /// <summary>
    ///  ボタンを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject[] _buttonObj = new GameObject[2];

    [SerializeField]
    private Image[] _buttonImage= new Image[2];

    /// <summary>
    /// 選択状態のボタンの画像を取得数変数
    /// </summary>
    [SerializeField]
    private Image[] _buttonSelect = new Image[2];

    /// <summary>
    /// カメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    /// canvasを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _titleCanvas;

    /// <summary>
    /// カメラの距離を保存する変数
    /// </summary>
    private float _distance;

    /// <summary>
    ///  カメラの位置を保存する変数
    /// </summary>
    private float _positionValue;

    /// <summary>
    /// 時間を保存する変数
    /// </summary>
    private float _time;

    /// <summary>
    /// ボタンが押されたかを判定する変数
    /// </summary>
    private bool _isClickButton = false;

    /// <summary>
    /// ボタンが押せる状況化を判定する変数
    /// </summary>
    private bool _isInputButton = false;

    /// <summary>
    /// シーン遷移ができる状況化を判定する変数
    /// </summary>
    private bool _isStepScene = false;

    /// <summary>
    /// 選択状態のボタンを保存する変数? 
    /// </summary>
    private GameObject _saveButton;

    /// <summary>
    /// AudioManagerを取得する
    /// </summary>
    [SerializeField]
    private AudioManager audioManager;

    /// <summary>
    /// 値を参照するために取得する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager settingManager;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // ボタンを非表示にする
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);
            _buttonSelect[i].enabled = false;
            _buttonImage[i].enabled = false;
        }

        // 初期に選択状態にするオブジェクトを設定する
        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);

        // 初期地点を保存する
        _startPosition = _cameraObj.transform.position;

        //  カメラの初期位置と終了位置の距離を保存する
        _distance = Vector3.Distance(_startPosition, _endPosition);

        //  X ^ [ g { ^         ?    ??  ?   
        _isClickButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ボタンが押せるようになった場合
        if (_isInputButton)
        {
            InputButton();
        }

        // UIの演出をする
        StartCoroutine("Fade_UI");

        // ｙボタンが押された場合
        if (Input.GetKeyDown(KeyCode.JoystickButton3)&&!_isStepScene)
        {
            // ボタンが押されたときのSEを鳴らす
            audioManager.PlaySESound(SEData.SE.ClickButton);

            // 現在の時間を保存する
            _time = Time.time;

            // canvasを表示する
            _titleCanvas.SetActive(false);

            // yボタンが押された判定をする
            _isStepScene = true;
        }
    }

    /// <summary>
    /// ボタンが押せるようになった時の演出処理の関数
    /// </summary>
    void InputButton()
    {
        // 現在の選択しているボタンを保存する
        _saveButton = EventSystem.current.currentSelectedGameObject;

        if (_saveButton == _buttonObj[0])
        {
            _buttonSelect[0].enabled = true;
            _buttonSelect[1].enabled = false;
        }
        if (_saveButton == _buttonObj[1])
        {
            _buttonSelect[0].enabled = false;
            _buttonSelect[1].enabled = true;
        }

        // ボタンが押された場合
        if (_isClickButton)
        {
            // カメラを動かす
            MoveCameraObj(1);
        }

        //  yボタンが押された場合
        if (_isStepScene)
        {
            // カメラを動かす
            MoveCameraObj(2);
        }
    }

    /// <summary>
    /// UIの演出関数
    /// </summary>
    /// <returns> 待ち時間  </returns>
    private IEnumerator Fade_UI()
    {
        // 一番目の演出
        if (!FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            // 背景画面をフェードインさせる
            _fadeSystem.FadeIn(_blackFadeIn);
        }

        // ２番目の演出
        if (FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            // 待ち時間
            yield return new WaitForSeconds(_intervalTIme[0]);

            // 音が鳴っていなかった場合
            if (audioManager.CheckPlaySound(audioManager.bgmAudioSource))
            {
                // BGMを鳴らす
                audioManager.PlayBGMSound(BGMData.BGM.Title);
            }

            // タイトルロゴをフェードアウトする
            _fadeSystem.FadeOut(_logoFadeOut);
        }

        // 三番目の演出
        if (FadeManager.fadeIn && FadeManager.fadeOut)
        {
            // 待ち時間
            yield return new WaitForSeconds(_intervalTIme[1]);

            // ボタンを表示する
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
                _buttonImage[i].enabled = true;
            }

            // ボタンを押せるようにする
            _isInputButton = true;
        }
    }

    /// <summary>
    /// スタートボタンが押されたときの関数
    /// </summary>
    public void OnClickStartButton()
    {
        // SEを鳴らす
        audioManager.PlaySESound(SEData.SE.ClickButton);

        // canvasを非表示にする
        _titleCanvas.SetActive(false);

        // ボタンを押せるようにする
        _isClickButton = true;

        // 現在の時間を保存する
        _time = Time.time;
    }

    /// <summary>
    /// 終了ボタンが押されたときの関数
    /// </summary>
    public void OnClickEndButton()
    {
        audioManager.PlaySESound(SEData.SE.ClickButton);

        // 音が鳴り終わった場合
        if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        {
            // ゲームを終了させる
            transSystem.Trans_EndGame();
        }
    }

    /// <summary>
    /// カメラを動かす演出の関数
    /// </summary>
    /// <param name="_seceneNumber">  遷移させたいシーンの番号  </param>
    private void MoveCameraObj(int _seceneNumber)
    {
        audioManager.ChangeBGMVolume(0.01f);

        if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        {
            //AudioManager.audioManager.Play_SESound(SESoundData.SE.Audience);
            audioManager.PlaySESound(SEData.SE.Walk);
        }

        // カメラを移動する位置を設定する
        _positionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

        // カメラを移動させる
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        if (_endPosition == _cameraObj.transform.position)
        {
            // 音を止める
            audioManager.StopSound(audioManager.seAudioSource);
            audioManager.StopSound(audioManager.bgmAudioSource);

            // 指定のシーンに遷移する
            transSystem.Trans_Scene(_seceneNumber);
        }
    }

    #endregion ---Methods---
}