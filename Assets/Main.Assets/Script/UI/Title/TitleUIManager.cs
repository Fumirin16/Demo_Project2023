using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

// タイトル画面のUI演出をするソースコード
//  作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// インスペクターの間隔
    /// </summary>
    private const int _inspectorSpace = 4;

    [Header("=== Title Logo ===")]
    /// <summary>
    /// タイトルスタートを再生するオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _titleStartobj;

    /// <summary>
    /// タイトルスタートのVideoPlayer
    /// </summary>
    private VideoPlayer _titleStartVideo;

    /// <summary>
    /// タイトルロゴを再生するオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _titleLogoObj;

    /// <summary>
    /// タイトルロゴのVideoPlayer
    /// </summary>
    private VideoPlayer _titleLogoVideo;

    [Space(_inspectorSpace)]

    [Header("=== Button UI ===")]
    /// <summary>
    /// スタートボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _startButtonObj;

    /// <summary>
    /// スタートボタンの選択中の画像
    /// </summary>
    [SerializeField]
    private Image[] _startButtonImage=new Image[2];

    /// <summary>
    /// アイドル紹介ボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _idolButtonObj;

    /// <summary>
    /// アイドル紹介ボタンの選択中の画像
    /// </summary>
    [SerializeField]
    private Image _idolButtonImage;

    /// <summary>
    /// クレジットボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _creditButtonObj;

    /// <summary>
    /// クレジットボタン画像のRect Transform
    /// </summary>
    [SerializeField]
    private RectTransform _creditButtonImage;

    /// <summary>
    /// クレジットボタン画像のRect Transformの初期Scale
    /// </summary>
    private Vector3 _creditScale = new Vector3(1, 1, 1);

    /// <summary>
    /// 退場ボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _endButtonObj;

    /// <summary>
    /// 退場ボタン画像のRect Transform
    /// </summary>
    [SerializeField]
    private RectTransform _endButtonImage;

    /// <summary>
    /// 退場ボタン画像のRect Transformの初期Scale
    /// </summary>
    private Vector3 _endScale = new Vector3(1, 1, 1);

    /// <summary>
    /// クレジットボタンと退場ボタンのScaleを変更する値
    /// </summary>
    [SerializeField]
    private float _buttonScaleChange = 1.4f;

    [Space(_inspectorSpace)]

    [Header("=== Camera Move ===")]
    /// <summary>
    /// メインカメラのオブジェクト
    /// </summary>
    [SerializeField]
    private Camera _mainCamera;

    /// <summary>
    /// メインカメラが動く速さ
    /// </summary>
    [SerializeField]
    private float _cameraMoveSpeed = 1f;

    /// <summary>
    /// メインカメラの初期位置
    /// </summary>
    private Vector3 _startPostion;

    /// <summary>
    /// メインカメラの移動先
    /// </summary>
    [SerializeField]
    private Vector3 _endPosition;

    /// <summary>
    /// メインカメラの初期位置と移動先の距離
    /// </summary>
    private float _distance;

    /// <summary>
    /// 現在のゲーム内時間
    /// </summary>
    private float _time;

    [Space(_inspectorSpace)]

    [Header("=== Use Script ===")]
    /// <summary>
    /// TranstionSceneのクラス
    /// </summary>
    [SerializeField]
    private TranstionScenes _transScene;

    /// <summary>
    /// AudioManagerのクラス
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    /// <summary>
    /// UI演出の現在の順番
    /// </summary>
    private int _uiCounter = 10;

    /// <summary>
    /// 選択されてるオブジェクト
    /// </summary>
    private GameObject _buttonObj;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {
        // ui_TitleStartVideoからVideoPlayerコンポーネントを取得
        _titleStartVideo = _titleStartobj.GetComponent<VideoPlayer>();

        // ui_TitleLogoVideoからVideoPlayerコンポーネントを取得
        _titleLogoVideo = _titleLogoObj.GetComponent<VideoPlayer>();

        // ui_TitleStartVideoのループ設定をオフにする
        _titleStartVideo.isLooping = false;

        // ui_TitleLogoVideoのループ設定をオンにする
        _titleLogoVideo.isLooping = true;

        // タイトルスタートを表示
        _titleStartobj.SetActive(true);

        // タイトルロゴを非表示
        _titleLogoObj.SetActive(false);

        // ui_TitleStartVideoを再生する
        _titleStartVideo.Play();

        // ui_TitleLogoVideoを止めておく
        _titleLogoVideo.Stop();

        // スタートボタンの選択中画像を表示しておく
        _startButtonImage[0].enabled = true;
        _startButtonImage[1].enabled = true;

        // アイドル紹介ボタンの選択中画像を非表示にしておく
        _idolButtonImage.enabled = false;

        // クレジットボタン画像のScaleを初期Scaleに設定する
        _creditButtonImage.transform.localScale = _creditScale;

        // 退場ボタン画像のScaleを初期Scaleに設定する
        _endButtonImage.transform.localScale = _endScale;

        // スタートボタンを非アクティブにする
        _startButtonObj.SetActive(false);

        // アイドル紹介ボタンを非アクティブにする
        _idolButtonObj.SetActive(false);

        // クレジットボタンを非アクティブにする
        _creditButtonObj.SetActive(false);

        // 退場ボタンを非アクティブにする
        _endButtonObj.SetActive(false);

        // カメラの初期位置を保存する
        _startPostion = _mainCamera.transform.position;

        // メインカメラの初期位置と移動先位置の距離を計算する
        _distance = Vector3.Distance(_startPostion, _endPosition);

        // 初期に選択状態にしておくボタンを設定する
        EventSystem.current.SetSelectedGameObject(_startButtonObj);
    }
    /*
     * 選択中画像を非表示・表示を設定しておく理由：Unity側で間違って非表示・表示の設定をしても正常に動くようにするため
     */

    private void Update()
    {
        // 再生してからUI演出の処理を稼働させるための処理
        if (_titleStartVideo.isPlaying)
        {
            _uiCounter = 0;
        }

        // _uiCounterの値によって処理を変える
        switch (_uiCounter)
        {
            // 再生されてるタイトルスタートが終わった後の演出
            case (int)UIdirecton.StartLogo:
                // タイトルスタートが流れ終わった場合
                if (!_titleStartVideo.isPlaying)
                {
                    // タイトルロゴを表示
                    _titleLogoObj.SetActive(true);

                    // タイトルスタートを非表示
                    _titleStartobj.SetActive(false);

                    // タイトルロゴを再生する
                    _titleLogoVideo.Play();

                    _uiCounter = 1;
                }

                break;

            // ボタンを表示する演出
            case (int)UIdirecton.Button:
                // スタートボタンをアクティブにする
                _startButtonObj.SetActive(true);

                // アイドル紹介ボタンをアクティブにする
                _idolButtonObj.SetActive(true);

                // クレジットボタンをアクティブにする
                _creditButtonObj.SetActive(true);

                // 退場ボタンをアクティブにする
                _endButtonObj.SetActive(true);

                _uiCounter = 2;
                break;

            // 選択されているボタンの演出
            case (int)UIdirecton.Select:
                // 現在、選択されているボタンの情報を保存する
                _buttonObj = EventSystem.current.currentSelectedGameObject;

                // 選択されているボタンがスタートボタンな場合
                if (_buttonObj == _startButtonObj)
                {
                    // スタートボタンの選択中の画像を表示
                    _startButtonImage[0].enabled = true;
                    _startButtonImage[1].enabled = true;

                    // アイドル紹介ボタンの選択中の画像を非表示
                    _idolButtonImage.enabled = false;

                    if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Z))
                    {
                        // タイトルロゴを非表示
                        _titleLogoObj.SetActive(false);

                        // スタートボタンをアクティブにする
                        _startButtonObj.SetActive(false);

                        // アイドル紹介ボタンをアクティブにする
                        _idolButtonObj.SetActive(false);

                        // クレジットボタンをアクティブにする
                        _creditButtonObj.SetActive(false);

                        // 退場ボタンをアクティブにする
                        _endButtonObj.SetActive(false);

                        _time = Time.time;

                        _uiCounter = 3;
                    }

                    if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Y))
                    {
                        // タイトルロゴを非表示
                        _titleLogoObj.SetActive(false);

                        // スタートボタンをアクティブにする
                        _startButtonObj.SetActive(false);

                        // アイドル紹介ボタンをアクティブにする
                        _idolButtonObj.SetActive(false);

                        // クレジットボタンをアクティブにする
                        _creditButtonObj.SetActive(false);

                        // 退場ボタンをアクティブにする
                        _endButtonObj.SetActive(false);

                        _time = Time.time;

                        _uiCounter = 4;
                    }
                }
                // 選択されているボタンがアイドル紹介ボタンな場合
                if (_buttonObj == _idolButtonObj)
                {
                    // スタートボタンの選択中の画像を非表示
                    _startButtonImage[0].enabled = false;
                    _startButtonImage[1].enabled = false;

                    // アイドル紹介ボタンの選択中の画像を表示
                    _idolButtonImage.enabled = true;

                    // クレジットボタンのScaleを初期Scaleに固定
                    _creditButtonObj.transform.localScale = _creditScale;
                }
                // 選択されているボタンがクレジットボタンな場合
                if(_buttonObj == _creditButtonObj)
                {
                    // アイドル紹介ボタンの選択中の画像を非表示
                    _idolButtonImage.enabled = false;

                    // クレジットボタンのScaleを変更
                    _creditButtonObj.transform.localScale = new Vector3(_buttonScaleChange, _buttonScaleChange, _buttonScaleChange);

                    // 退場ボタンのScaleを初期Scaleに固定
                    _endButtonImage.transform.localScale = _endScale;
                }
                if (_buttonObj == _endButtonObj)
                {
                    // アイドル紹介ボタンの選択中の画像を非表示
                    _idolButtonImage.enabled = false;

                    // クレジットボタンを初期Scaleに固定
                    _creditButtonObj.transform.localScale = _creditScale;

                    // 退場ボタンのScaleのScaleを変更
                    _endButtonImage.transform.localScale = new Vector3(_buttonScaleChange, _buttonScaleChange, _buttonScaleChange);
                }
                break;

            // スタートボタンのAボタンが押された後にカメラを動かす演出
            case (int)UIdirecton.ClickAButton:
                // カメラを移動する位置を設定する
                float _positionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

                // カメラを移動させる
                _mainCamera.transform.position = Vector3.Lerp(_startPostion, _endPosition, _positionValue);

                if (_endPosition == _mainCamera.transform.position)
                {
                    _transScene.Trans_Scene(3);
                }
                break;

            // スタートボタンのYボタンが押された後にカメラを動かす演出
            case (int)UIdirecton.ClickYButton:
                // カメラを移動する位置を設定する
                float _postionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

                // カメラを移動させる
                _mainCamera.transform.position = Vector3.Lerp(_startPostion, _endPosition, _postionValue);

                if (_endPosition == _mainCamera.transform.position)
                {
                    _transScene.Trans_Scene(4);
                }
                break;
            default:
                break;
        }
    }

    public void OnClikButton(int transSceneNum)
    {
        // タイトルロゴを非表示
        _titleLogoObj.SetActive(false);


        // スタートボタンをアクティブにする
        _startButtonObj.SetActive(false);

        // アイドル紹介ボタンをアクティブにする
        _idolButtonObj.SetActive(false);

        // クレジットボタンをアクティブにする
        _creditButtonObj.SetActive(false);

        // 退場ボタンをアクティブにする
        _endButtonObj.SetActive(false);

        _transScene.Trans_Scene(transSceneNum);
    }

    #endregion ---Methods---

    private enum UIdirecton
    {
        StartLogo,
        Button,
        Select,
        ClickAButton,
        ClickYButton,
    }
}