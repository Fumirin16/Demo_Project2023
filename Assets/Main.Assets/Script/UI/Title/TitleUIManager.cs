using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// タイトル画面の演出処理を記述したスクリプト
// 作成者：山�ｱ晶

public class TitleUIManager : MonoBehaviour
{
    // 画像を表示する間隔の時間を保存
    [SerializeField, Range(0f, 10f)] private float[] _intervalTIme = new float[2];

    [Space(10)]

    // カメラが移動する速さを保存
    [SerializeField, Range(0f, 10f)] private float _cameraMoveSpeed = 1f;
   
    // カメラの初期位置を保存
    private Vector3 _startPosition;
    // カメラの移動先の位置を保存
    [SerializeField] private Vector3 _endPosition;

    [Space(10)]

    // フェードインの速さを保存
    [SerializeField, Range(0f, 10f)] private float _fadeInSpeed = 0.1f;

    // フェードアウトの速さを保存
    [SerializeField, Range(0f, 10f)] private float _fadeOutSpeed = 0.1f;


    //「FadeSystem」のインスタンスを生成
    private FadeManager _fadeSystem = new FadeManager();
    //「TranstionScenes」のインスタンスを生成
    [HideInInspector] public TranstionScenes transSystem;

    // フェードをするImageを取得
    [SerializeField] private Image _fadeImage;
    // タイトルロゴのImageを取得
    [SerializeField] private Image _titleImage;
    // 「ui_startButton」と「ui_endButton」をゲームオブジェクトとして取得
    [SerializeField]private GameObject[] _buttonObj = new GameObject[2];
    // 選択されていないボタンを暗く表示するためのImageを取得
    [SerializeField] private GameObject[] _selectButtonImage = new GameObject[2];
    // カメラを動かすために「MainCamera」をゲームオブジェクトとして保存
    [SerializeField] private GameObject _cameraObj;
    //「TitleCanvas」をゲームオブジェクトとして取得
    [SerializeField] private GameObject _titleCanvas;

    // 「_distance」は初期位置と移動先の距離を保存
    // 「_positionValue」は２点間の移動する位置の値を保存
    // 「&& _isInputButton」は現在のゲーム時間を保存
    private float _distance, _positionValue, _time;

    // スタートボタンが押されたかの判定を保存
    private bool _isClickButton = false;

    // ボタンの入力を受け付ける判定を保存
    private bool _isInputButton = false;

    private bool _isStepScene = false;

    private GameObject _saveButton;

    // Start is called before the first frame update
    void Start()
    {
        // タイトル画面のUIの初期化
        Initi_TitleUI();

        // スタートボタンが押された処理関係の初期化 
        Initi_TransFunction();

        FadeVariables.Initi_Fade();
    }

    // Update is called once per frame
    void Update()
    {
        // 選択中のボタンの情報を保存する
        _saveButton = EventSystem.current.currentSelectedGameObject;

        if (_saveButton == _buttonObj[0])
        {
            _selectButtonImage[0].SetActive(false);
            _selectButtonImage[1].SetActive(true);
        }
        if( _saveButton == _buttonObj[1])
        {
            _selectButtonImage[1].SetActive(false);
            _selectButtonImage[0].SetActive(true);
        }

        // タイトル画面のUIの演出をするコルーチンを呼び出す
        StartCoroutine("Fade_UI");

        if (_isClickButton&& _isInputButton)
        {
            Move_CameraObj(1);
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton3) )
        {
            AudioManager.Instance.Play_SESound(SESoundData.SE.ClickButton);

            _isStepScene = true;

            // 現在のゲーム内の時間を変数に保存する
            _time = Time.time;

            // タイトル画面のUI表示を非表示にする
            _titleCanvas.SetActive(false);
        }

        if (_isStepScene && _isInputButton)
        {
            Move_CameraObj(2);
        }
    }


    // タイトル画面のUIの演出をするコルーチン
    private IEnumerator Fade_UI()
    {
        // 一番目に表示させる演出処理
        if (!FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // フェードインをする関数を呼び出す
            _fadeSystem.FadeIn(_fadeImage, _fadeImage.color.a, _fadeInSpeed);
        }

        // 二番目に表示させる演出処理
        if (FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_intervalTIme[0]);

            if (AudioManager.Instance.CheckPlaySound(AudioManager.Instance.bgmAudioSource))
            {
                AudioManager.Instance.Play_BGMSound(BGMSoundData.BGM.Title);
            }

            // フェードアウトをする関数を呼び出す
            _fadeSystem.FadeOut(_titleImage, _titleImage.color.a, _fadeOutSpeed);
        }

        // 三番目に表示させる演出処理
        if (FadeVariables.FadeIn && FadeVariables.FadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_intervalTIme[1]);

            // ボタンを表示させる処理
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
            }

            _isInputButton = true;
        }
    }
    void Initi_TransFunction()
    {
        // カメラの初期位置を変数に保存する
        _startPosition = _cameraObj.transform.position;

        // 初期位置と移動先の位置同士の距離の長さを変数に保存する
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // スタートボタンが押された判定を無効にする
        _isClickButton = false;
    }

    void Initi_TitleUI()
    {
        // ボタンの表示を無効にする
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);

            _selectButtonImage[i].SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);
    }

    public void OnClick_StartButton()
    {
        AudioManager.Instance.Play_SESound(SESoundData.SE.ClickButton);

        // タイトル画面のUI表示を非表示にする
        _titleCanvas.SetActive(false);

        // スタートボタンが押された判定を有効にする
        _isClickButton = true;

        // 現在のゲーム内の時間を変数に保存する
        _time = Time.time;
    }

    public void OnClick_EndButton()
    {
        AudioManager.Instance.Play_SESound(SESoundData.SE.ClickButton);

        if (AudioManager.Instance.CheckPlaySound(AudioManager.Instance.seAudioSource))
        {
            transSystem.Trans_EndGame();
        }
    }

    private void Move_CameraObj(int _seceneNumber)
    {
        AudioManager.Instance.Change_BGMVolume(0.01f);

        if (AudioManager.Instance.CheckPlaySound(AudioManager.Instance.seAudioSource))
        {
            //AudioManager.Instance.Play_SESound(SESoundData.SE.Audience);
            AudioManager.Instance.Play_SESound(SESoundData.SE.Walk);
        }

        // 初期位置と移動先の距離の割合を計算する処理
        // 「(&& _isInputButton.&& _isInputButton - && _isInputButton) / _distance」は距離の長さを100として見て時間経過で距離の長さを割ることで２点の移動距離を指定する値を求める。
        _positionValue = ((Time.time-_time) / _distance) * _cameraMoveSpeed;

        // カメラの位置を動かす処理
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // カメラの位置が指定した位置に来た場合
        if (_cameraObj.transform.position == _endPosition)
        {
            // スタートボタンが押された判定を無効にする
            _isClickButton = false;

            AudioManager.Instance.Stop_Sound(AudioManager.Instance.seAudioSource);
            AudioManager.Instance.Stop_Sound(AudioManager.Instance.bgmAudioSource);

            // チュートリアルのシーンに遷移する
            transSystem.Trans_Scene(_seceneNumber);
        }
    }
}
