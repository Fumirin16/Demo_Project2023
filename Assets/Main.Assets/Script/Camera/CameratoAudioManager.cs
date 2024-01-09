using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 作成者：山﨑晶
// カメラに関するスクリプト

public class CameratoAudioManager : MonoBehaviour
{
    #region ---Fields---

    [Header("=== Object ===")]
    /// <summary>
    /// カメラの回転する動作の中心点
    /// </summary>
    [SerializeField]
    private Transform _playerObj;

    /// <summary>
    /// 対象オブジェクトの_layer
    /// </summary>
    [SerializeField]
    private LayerMask _layer;

    /// <summary>
    /// Ray
    /// </summary>
    private Ray _ray;

    /// <summary>
    /// Rayに当たったオブジェクトを格納するリスト
    /// </summary>
    private List<GameObject> _hitObj=new List<GameObject> ();

    /// <summary>
    /// 前回の保存したオブジェクトを格納するリスト
    /// </summary>
    private GameObject[] _saveObj;

    /// <summary>
    /// Rawの範囲
    /// </summary>
    private float _rawRadio;

    /// <summary>
    /// カメラとプレイヤーの距離
    /// </summary>
    private Vector3 _offset;

    [Header("=== Camera ===")]
    /// <summary>
    /// メインカメラのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _mainCameraObj;

    /// <summary>
    /// 左肩カメラのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _leftCameraObj;

    /// <summary>
    /// 右肩カメラのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _rightCameraObj;

    /// <summary>
    /// メインカメラのCameraコンポーネント
    /// </summary>
    private Camera _mainCamera;

    /// <summary>
    /// 左肩カメラのCameraコンポーネント
    /// </summary>
    private Camera _leftCamera;

    /// <summary>
    /// 右肩カメラのCameraコンポーネント
    /// </summary>
    private Camera _rightCamera;

    [Header("=== Camera Function ===")]
    /// <summary>
    /// じらじょちゃんの真後ろから追跡する機能
    /// </summary>
    [SerializeField]
    public bool _nomal = true;

    /// <summary>
    /// じらじょちゃんの肩らへんから追跡する機能
    /// </summary>
    [SerializeField]
    public bool _nomalDiffPos = false;

    /// <summary>
    /// スイッチで視点の場所が切り替わる機能
    /// </summary>
    [SerializeField]
    public bool _switchButton = false;

    /// <summary>
    /// スティック移動で視点移動できる機能
    /// </summary>
    [SerializeField]
    public bool _stickButton = false;

    [Header("=== Canvas ===")]
    /// <summary>
    /// リアクションUIのCanvas
    /// </summary>
    [SerializeField]
    private Canvas _riactionCanvas;

    /// <summary>
    /// 終了UIのCanvas
    /// </summary>
    [SerializeField]
    private Canvas _finishCanvas;

    /// <summary>
    /// プレイヤーUIのCanvas
    /// </summary>
    [SerializeField]
    private Canvas _situationCanvas;

    [Header("=== Object Table ===")]
    /// <summary>
    /// ValueSettingTable
    /// </summary>
    [SerializeField]
    private ValueSettingManager _settingSystem;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // Rawの範囲の値を参照して保存する
        _rawRadio = _settingSystem.cameraHitRadio;

        // カメラオブジェクトからCameraコンポーネントを取得
        _mainCamera = _mainCameraObj.GetComponent<Camera>();
        _leftCamera = _leftCameraObj.GetComponent<Camera>();
        _rightCamera = _rightCameraObj.GetComponent<Camera>();

        // カメラ機能がNomalだった場合
        if (_nomal)
        {
            // メインカメラのカメラコンポーネントをオンにして、他はオフにする
            _mainCamera.enabled = true;
            _leftCamera.enabled = false;
            _rightCamera.enabled = false;

            // メインカメラをアクティブにして、他はオフにする
            _mainCameraObj.SetActive(true);
            _leftCameraObj.SetActive(false);
            _rightCameraObj.SetActive(false);

            // メインカメラとプレイヤーの距離を計算する
            _offset = _mainCamera.transform.position - _playerObj.position;
        }

        // カメラ機能がNomalDiffPos / StickButtonだった場合
        if (_nomalDiffPos || _stickButton)
        {
            // 左肩カメラのカメラコンポーネントをオンにして、他はオフにする
            _mainCamera.enabled = false;
            _leftCamera.enabled = true;
            _rightCamera.enabled = false;

            // 左肩カメラをアクティブにして、他はオフにする
            _mainCameraObj.SetActive(false);
            _leftCameraObj.SetActive(true);
            _rightCameraObj.SetActive(false);

            // 左肩カメラとプレイヤーの距離を計算する
            _offset = _leftCamera.transform.position - _playerObj.position;
        }

        // カメラ機能がSwitchButtonだった場合
        if (_switchButton)
        {
            // 左肩カメラのカメラコンポーネントをオンにして、他はオフにする
            _mainCamera.enabled = false;
            _leftCamera.enabled = true;
            _rightCamera.enabled = false;

            // 左肩カメラと右肩カメラをアクティブにして、他はオフにする
            _mainCameraObj.SetActive(false);
            _leftCameraObj.SetActive(true);
            _rightCameraObj.SetActive(true);

            // 左肩カメラとプレイヤーの距離を計算する
            _offset = _playerObj.position-_leftCamera.transform.position ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ２点間のベクトルを正規化する
        Vector3 positionVector = _offset.normalized;

        Debug.Log(positionVector);

        // カメラ機能がNomalだった場合
        if (_nomal)
        {
            // _rayをカメラからプレイヤーに飛ばす
            _ray = new Ray(_mainCamera.transform.position, positionVector);
        }

        // カメラ機能がNomalDiffPosだった場合
        if (_nomalDiffPos)
        {
            // canvasのカメラ設定を左肩カメラに設定する
            _riactionCanvas.worldCamera = _leftCamera;
            _finishCanvas.worldCamera = _leftCamera;
            _situationCanvas.worldCamera = _leftCamera;

            // _rayをカメラからプレイヤーに飛ばす
            _ray = new Ray(_leftCamera.transform.position, positionVector);

            Debug.DrawRay(_leftCamera.transform.position, positionVector, UnityEngine.Color.red);
        }

        // 
        if (_switchButton)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0) && !_rightCamera.enabled)
            {
                // 右肩カメラのCameraコンポーネントを有効にして、左肩カメラのCameraコンポーネントを無効にする
                _leftCamera.enabled = false;
                _rightCamera.enabled = true;

                // canvasのカメラ設定を右肩カメラに設定する
                _riactionCanvas.worldCamera = _rightCamera;
                _finishCanvas.worldCamera = _rightCamera;
                _situationCanvas.worldCamera = _rightCamera;

                // 左肩カメラとプレイヤーの距離を計算する
                _offset = _rightCamera.transform.position - _playerObj.position;

                // _rayをカメラからプレイヤーに飛ばす
                _ray = new Ray(_rightCamera.transform.position, positionVector);
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3) && !_leftCamera.enabled)
            {
                // 左肩カメラのCameraコンポーネントを有効にして、右肩カメラのCameraコンポーネントを無効にする
                _leftCamera.enabled = true;
                _rightCamera.enabled = false;

                // canvasのカメラ設定を左肩カメラに設定する
                _riactionCanvas.worldCamera = _leftCamera;
                _finishCanvas.worldCamera = _leftCamera;
                _situationCanvas.worldCamera = _leftCamera;

                // 左肩カメラとプレイヤーの距離を計算する
                _offset = _leftCamera.transform.position - _playerObj.position;

                // _rayをカメラからプレイヤーに飛ばす
                _ray = new Ray(_leftCamera.transform.position, positionVector);
            }
        }

        // 球体のRayを生成する
        RaycastHit[] _hits = Physics.SphereCastAll(_ray, _rawRadio, positionVector.magnitude, _layer);

        // 前回のリストを保存する
        _saveObj = _hitObj.ToArray();

        // リストを初期化する
        _hitObj.Clear();

        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit _hit in _hits)
        {
            // 遮蔽物の Renderer コンポーネントを無効にする
            GameObject _renderer = _hit.collider.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

            // オブジェクトが存在していた場合
            if (_renderer != null)
            {
                // 当たったオブジェクトをリストに追加する
                _hitObj.Add(_renderer);

                //当たったオブジェクトを非表示にする
                _renderer.SetActive(false);
            }
        }

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (GameObject _renderer in _saveObj.Except(_hitObj))
        {
            // オブジェクトが存在していた場合
            if (_renderer != null)
            {
                // オブジェクトを表示する
                _renderer.SetActive(true);
            }
        }
    }

    #endregion ---Methods---
}
