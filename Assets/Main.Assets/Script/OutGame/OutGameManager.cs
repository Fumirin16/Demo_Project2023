using Cinemachine;
using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//  ゲームがゲームオーバー・クリアになった時の処理
//  作成者：山﨑晶

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    [SerializeField]
    private ValueSettingManager _settingManager;

    [SerializeField]
    private AudioManager _audioSystem;

    [SerializeField]
    private TranstionScenes _transSystem;

    [SerializeField]
    private GameObject _noActiveArea;

    [SerializeField]
    private GameObject _TargetObj;

    [SerializeField]
    private GameObject _mainCamera;

    [SerializeField]
    private GameObject _leftCamera;
    [SerializeField]
    private GameObject _rightCamera;

    [SerializeField]
    private GameObject _clearText;

    [SerializeField]
    private Vector3 _cameraMove;

    [SerializeField]
    private float _cameraSpeed;

    [SerializeField]
    private GameObject _playerObj;

    [SerializeField]
    private GameObject _guardObj;

    [SerializeField]
    private GameObject _guardAnimatorObj;

    private Vector3 _endPos;

    private bool _isMemory = false;

    [SerializeField]
    private CameratoAudioManager _cameraSystem;

    #endregion ---Fields---

    #region ---Methods---

    private void Awake()
    {
        _settingManager.gameOver = false;
        _settingManager.gameClear = false;
    }

    private void Start()
    {
        _noActiveArea.SetActive(false);
    }

    private void Update()
    {
        // ゲームオーバー時の処理  
        if (_settingManager.gameOver)
        {
            _audioSystem.StopSound(_audioSystem.bgmAudioSource);

            // プレイヤーと警備員の動きを止める関数の呼び出し
            DontMove_AntherScript();

            // シーンを遷移する
            _transSystem.Trans_Scene(6);

            //StartCoroutine(Direction_UI(_overText, 6,_settingManager.gameOver));
        }

        // ゲームクリア時の処理
        if (_settingManager.gameClear)
        {
            if (!_isMemory)
            {
                _endPos = _playerObj.transform.position;
                _isMemory = true;
            }
            _playerObj.transform.position = _endPos;

            _audioSystem.StopSound(_audioSystem.seAudioSource);

            // プレイヤーと警備員の動きを止める関数の呼び出し
            DontMove_AntherScript();

            _noActiveArea.SetActive(true);

            if (_cameraSystem._nomal)
            {
                _mainCamera.transform.rotation = Quaternion.Slerp(_mainCamera.transform.rotation, Quaternion.LookRotation((_TargetObj.transform.position - _mainCamera.transform.position).normalized), _cameraSpeed * Time.deltaTime);
            }
            if (_cameraSystem._nomalDiffPos || _cameraSystem._stickButton)
            {
                _leftCamera.transform.rotation = Quaternion.Slerp(_leftCamera.transform.rotation, Quaternion.LookRotation((_TargetObj.transform.position - _leftCamera.transform.position).normalized), _cameraSpeed * Time.deltaTime);
            }
            if (_cameraSystem._switchButton)
            {
                if (_leftCamera.GetComponent<Camera>().enabled)
                {
                    _leftCamera.transform.rotation = Quaternion.Slerp(_leftCamera.transform.rotation, Quaternion.LookRotation((_TargetObj.transform.position - _leftCamera.transform.position).normalized), _cameraSpeed * Time.deltaTime);
                }
                else if (_rightCamera.GetComponent<Camera>().enabled)
                {
                    _rightCamera.transform.rotation = Quaternion.Slerp(_rightCamera.transform.rotation, Quaternion.LookRotation((_TargetObj.transform.position - _rightCamera.transform.position).normalized), _cameraSpeed * Time.deltaTime);
                }

            }


            StartCoroutine(GameClear());

        }
    }


    IEnumerator GameClear()
    {
        // 待ち時間
        yield return new WaitForSeconds(10);

        _clearText.SetActive(true);

        // 待ち時間
        yield return new WaitForSeconds(5);

        _audioSystem.StopSound(_audioSystem.bgmAudioSource);

        // シーンを遷移する
        _transSystem.Trans_Scene(5);
    }

    /// <summary>
    ///  オブジェクトの動きを止める関数
    /// </summary>
    private void DontMove_AntherScript()
    {
        //  プレイヤーのMocopiを止める
        _playerObj.GetComponent<MocopiAvatar>().enabled = false;

        // プレイヤーのジョイコンを止める
        _playerObj.GetComponent<PlayerController>().enabled = false;

        // プレイヤーの足踏みを止める
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        // カメラ移動を止める
        //_cameraObj.GetComponent<CinemachineBrain>().enabled = false;

        // 警備員の移動を止める 
        _guardObj.GetComponent<AroundGuardsmanController>().enabled = false;

        // 警備員の動作を止める
        _guardObj.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // 警備員のアニメーションを止める
        _guardAnimatorObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}