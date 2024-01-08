﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//  ゲームがゲームオーバー・クリアになった時の処理
//  作成者：山﨑晶

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Character ===")]
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _playerObj;

    /// <summary>
    /// 周囲の観客を消す範囲のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _noActiveArea;

    /// <summary>
    /// プレイヤーの固定位置
    /// </summary>
    private Vector3 _endPos;

    /// <summary>
    /// プレイヤーが固定されたかの判定
    /// </summary>
    private bool _isFixation;

    /// <summary>
    /// 警備員のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _guardObj;

    /// <summary>
    /// 警備員のアニメーションのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _guardAnimatorObj;


    [Header("=== Camera ===")]
    /// <summary>
    /// 注目する注視点のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _TargetObj;

    /// <summary>
    /// メインカメラ
    /// </summary>
    [SerializeField]
    private GameObject _mainCamera;

    /// <summary>
    /// 左肩カメラ
    /// </summary>
    [SerializeField]
    private GameObject _leftCamera;

    /// <summary>
    /// 右肩カメラ
    /// </summary>
    [SerializeField]
    private GameObject _rightCamera;

    /// <summary>
    /// カメラが動く速さ
    /// </summary>
    [SerializeField]
    private float _cameraSpeed;

    [ Header("=== Text ===")]
    /// <summary>
    /// クリアテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _clearText;

    /// <summary>
    /// スキップテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _skipText;

    /// <summary>
    /// 盛り上げテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _moriageText;

    /// <summary>
    /// ボタンテキストのオブジェクト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _buttonText;

    /// <summary>
    /// 盛り上げテキストが表示されたかの判定
    /// </summary>
    private bool _isMoriage;

    [Header("=== Script ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingManager;

    /// <summary>
    /// system_Audioのスクリプト
    /// </summary>
    [SerializeField]
    private AudioManager _audioSystem;

    /// <summary>
    /// Cameraのスクリプト
    /// </summary>
    [SerializeField]
    private CameratoAudioManager _cameraSystem;

    #endregion ---Fields---

    #region ---Methods---

    private void Awake()
    {
        // ゲームオーバー・ゲームクリアの判定をオフにする
        _settingManager.gameOver = false;
        _settingManager.gameClear = false;
    }

    private void Start()
    {
        // 周囲の観客を消すオブジェクトを非アクティブにする
        _noActiveArea.SetActive(false);

        // スキップテキストを表示する
        _skipText.enabled = false;

        // クリアテキストを表示する
        _clearText.enabled = false;

        // ボタンテキストを表示する
        _buttonText.enabled = false;

        // 盛り上げテキストを表示する
        _moriageText.enabled = false;

        // テキストが表示されたかの判定をオフにする
        _isMoriage = false;
    }

    private void Update()
    {
        // ゲームオーバー時の処理  
        if (_settingManager.gameOver)
        {
            // BGMを止める
            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            // プレイヤーと警備員の動きを止める関数の呼び出し
            DontMove_AntherScript();

            // シーンを遷移する
            SceneManager.LoadScene(6);
        }

        // ゲームクリア時の処理
        if (_settingManager.gameClear)
        {
            GameClearFunc();
        }
    }

    /// <summary>
    /// ゲームクリア時の演出関数
    /// </summary>
    private void GameClearFunc()
    {
        if (!_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
        {
            // SEを止める
            _audioSystem.StopSound(_audioSystem.seAudioSource);
        }
        // 位置が固定されてない判定になっていた場合
        if (!_isFixation)
        {
            // 現在のプレイヤーの位置を保存する
            _endPos = _playerObj.transform.position;

            // 位置が固定された判定をオンにする
            _isFixation = true;
        }

        // プレイヤーの位置を最終位置に固定する
        _playerObj.transform.position = _endPos;

        // 周囲の観客を消すオブジェクトをアクティブにする
        _noActiveArea.SetActive(true);

        // プレイヤーと警備員の動きを止める関数の呼び出し
        DontMove_AntherScript();

        // メインカメラ視点を使用している場合
        if (_cameraSystem._nomal)
        {
            // メインカメラをステージの注視点に向けて移動させる
            CameraMode(_mainCamera);
        }

        // 左肩カメラ視点もしくは左肩カメラを使用していた場合
        if (_cameraSystem._nomalDiffPos || _cameraSystem._stickButton)
        {
            // 左肩カメラをステージの注視点に向けて移動させる
            CameraMode(_leftCamera);
        }

        // 左肩カメラと右肩カメラの両視点を使用している場合
        if (_cameraSystem._switchButton)
        {
            // 左肩カメラが有効だった場合
            if (_leftCamera.GetComponent<Camera>().enabled)
            {
                // 左肩カメラをステージの注視点に向けて移動させる
                CameraMode(_leftCamera);
            }
            else if (_rightCamera.GetComponent<Camera>().enabled) // 右肩カメラが有効だった場合
            {
                // 右肩カメラをステージの注視点に向けて移動させる
                CameraMode(_rightCamera);
            }
        }

        // 盛り上げテキストの表示判定がオフだった場合
        if (!_isMoriage)
        {
            StartCoroutine(text());
        }

        // Aボタンが押された場合　デバッグ用にFキー
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.F))
        {
            // SEが鳴っていたら止める
            if (!_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
            {
                _audioSystem.StopSound(_audioSystem.seAudioSource);
            }

            // 歓声を再生する
            _audioSystem.PlaySESound(SEData.SE.Cheer);
        }

        // Bボタンが押された場合　デバッグ用にGキー
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.G))
        {
            // SEが鳴っていたら止める
            if (!_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
            {
                _audioSystem.StopSound(_audioSystem.seAudioSource);
            }

            // 叫びを再生する
            _audioSystem.PlaySESound(SEData.SE.Shout);
        }

        // Xボタンが押された場合　デバッグ用にHキー
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.H))
        {
            // SEが鳴っていたら止める
            if (!_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
            {
                _audioSystem.StopSound(_audioSystem.seAudioSource);
            }

            // ランダムな値を設定する
            int randam = (int)Random.Range(1, 3);

            // 設定した値を元にSEを再生する
            switch (randam)
            {
                case 1:
                    // Mao1を再生する
                    _audioSystem.PlaySESound(SEData.SE.MaoShout1);
                    break;
                case 2:
                    // Mao2を再生する
                    _audioSystem.PlaySESound(SEData.SE.MaoShout2);
                    break;
                case 3:
                    // Mao3を再生する
                    _audioSystem.PlaySESound(SEData.SE.MaoShout3);
                    break;
            }
        }

        // Yボタンが押された場合　デバッグ用にJキー
        if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.J))
        {
            // SEが鳴っていたら止める
            if (!_audioSystem.CheckPlaySound(_audioSystem.seAudioSource))
            {
                _audioSystem.StopSound(_audioSystem.seAudioSource);
            }

            // ランダムな値を設定する
            int randam = (int)Random.Range(1, 3);

            // 設定した値を元にSEを再生する
            switch (randam)
            {
                case 1:
                    // Ran1を再生する
                    _audioSystem.PlaySESound(SEData.SE.RanShout1);
                    break;
                case 2:
                    // Ran2を再生する
                    _audioSystem.PlaySESound(SEData.SE.RanShout2);
                    break;
                case 3:
                    // Ran3を再生する
                    _audioSystem.PlaySESound(SEData.SE.RanShout3);
                    break;
            }
        }

        // Rボタンを押した場合　デバッグ用にKキー
        if (Input.GetKeyDown(KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.K))
        {
            // クリア演出をスキップする
            GameClear();
        }
    }

    /// <summary>
    /// テキストの表示演出関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator text()
    {
        // スキップテキストを表示する
        _skipText.enabled = true;

        // クリアテキストを表示する
        _clearText.enabled = true;

        // ボタンテキストを表示する
        _buttonText.enabled = true;

        // 盛り上げテキストを表示する
        _moriageText.enabled = true;

        // 処理を待つ
        yield return new WaitForSeconds(6);

        // 盛り上げテキストを非表示にする
        _moriageText.enabled = false;

        // 盛り上げテキストの表示判定をオンにする
        _isMoriage = true;
    }

    /// <summary>
    /// ゲームクリア画面に遷移するための関数
    /// </summary>
    public void GameClear()
    {
        // BGMを止める
        _audioSystem.StopSound(_audioSystem.bgmAudioSource);

        // シーンを遷移する
        SceneManager.LoadScene(5);
    }

    /// <summary>
    /// カメラをステージの注視点に向けて移動させる関数
    /// </summary>
    /// <param name="camera"> 動かしたいカメラ </param>
    private void CameraMode(GameObject camera)
    {
        // カメラの回転を移動させる
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.LookRotation((_TargetObj.transform.position - camera.transform.position).normalized), _cameraSpeed * Time.deltaTime);
    }

    /// <summary>
    ///  オブジェクトの動きを止める関数
    /// </summary>
    private void DontMove_AntherScript()
    {
        //  プレイヤーのMocopiを止める
        //_playerObj.GetComponent<MocopiAvatar>().enabled = false;

        // プレイヤーのジョイコンを止める
        _playerObj.GetComponent<PlayerController>().enabled = false;

        // プレイヤーの足踏みを止める
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        // 警備員の移動を止める 
        _guardObj.GetComponent<AroundGuardsmanController>().enabled = false;

        // 警備員の動作を止める
        _guardObj.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // 警備員のアニメーションを止める
        _guardAnimatorObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}