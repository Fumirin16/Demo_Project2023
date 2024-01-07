using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameratoAudioManager : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtObj;

    [SerializeField]
    private Transform playerObj;

    [SerializeField]
    private LayerMask layer;

    private List<GameObject> hitObj=new List<GameObject> ();

    private GameObject[] saveObj;

    [SerializeField]
    private float radio=1.0f;

    private Vector3 _offset;

    float input;

    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private Camera _leftCamera;

    [SerializeField]
    private Camera _rightCamera;

    [SerializeField]
    private GameObject _mainCameraObj;

    [SerializeField]
    private GameObject _leftCameraObj;

    [SerializeField]
    private GameObject _rightCameraObj;

    // じらじょちゃんの真後ろから追跡するカメラ
    [SerializeField]
    public bool _nomal = true;

    // じらじょちゃんの肩らへんから追跡すルカメラ
    [SerializeField]
    public bool _nomalDiffPos = false;

    // スイッチで視点の場所が切り替わるカメら
    [SerializeField]
    public bool _switchButton = false;

    // スティック移動で視点移動できるカメラ
    [SerializeField]
    public bool _stickButton = false;

    [SerializeField]
    private Canvas _riactionCanvas;

    [SerializeField]
    private Canvas _finishCanvas;

    [SerializeField]
    private Canvas _situationCanvas;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        if (_nomal)
        {
            _mainCamera.enabled = true;
            _leftCamera.enabled = false;
            _rightCamera.enabled = false;

            _mainCameraObj.SetActive(true);
            _leftCameraObj.SetActive(false);
            _rightCameraObj.SetActive(false);

            _offset = _mainCamera.transform.position - playerObj.position;
        }

        if (_nomalDiffPos|| _switchButton|| _stickButton)
        {
            _mainCamera.enabled = false;
            _leftCamera.enabled = true;
            _rightCamera.enabled = false;

            _mainCameraObj.SetActive(false);
            _leftCameraObj.SetActive(true);
            _rightCameraObj.SetActive(true);

            _offset = _leftCamera.transform.position - playerObj.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ２点間のベクトルを正規化する
        Vector3 positionVector = _offset.normalized;

        if (_nomal)
        {
            _mainCamera.transform.RotateAround(playerObj.position, Vector3.up, input * 3f);

            // Rayをカメラからプレイヤーに飛ばす
            ray = new Ray(_mainCamera.transform.position, positionVector);

            // Rayを可視化するデバック
            Debug.DrawRay(_mainCamera.transform.position, positionVector, UnityEngine.Color.red);

        }

        if (_nomalDiffPos)
        {
            _leftCamera.transform.RotateAround(playerObj.position, Vector3.up, input * 3f);

            _riactionCanvas.worldCamera = _leftCamera;
            _finishCanvas.worldCamera = _leftCamera;
            _situationCanvas.worldCamera = _leftCamera;

            // Rayをカメラからプレイヤーに飛ばす
            ray = new Ray(_leftCamera.transform.position, positionVector);

            // Rayを可視化するデバック
            Debug.DrawRay(_leftCamera.transform.position, positionVector, UnityEngine.Color.red);
        }
        if (_switchButton)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (!_rightCamera.enabled)
                {
                    _leftCamera.enabled = false;
                    _rightCamera.enabled = true;

                    _riactionCanvas.worldCamera = _rightCamera;
                    _finishCanvas.worldCamera = _rightCamera;
                    _situationCanvas.worldCamera = _rightCamera;

                    _rightCamera.transform.RotateAround(playerObj.position, Vector3.up, input * 3f);

                    // Rayをカメラからプレイヤーに飛ばす
                    ray = new Ray(_rightCamera.transform.position, positionVector);

                    // Rayを可視化するデバック
                    //Debug.DrawRay(_rightCamera.transform.position, positionVector, UnityEngine.Color.red);
                }

            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                if (!_leftCamera.enabled)
                {
                    _leftCamera.enabled = true;
                    _rightCamera.enabled = false;

                    _riactionCanvas.worldCamera = _leftCamera;
                    _finishCanvas.worldCamera = _leftCamera;
                    _situationCanvas.worldCamera = _leftCamera;

                    _leftCamera.transform.RotateAround(playerObj.position, Vector3.forward, input * 3f);

                    // Rayをカメラからプレイヤーに飛ばす
                    ray = new Ray(_leftCamera.transform.position, positionVector);

                    // Rayを可視化するデバック
                    //Debug.DrawRay(_leftCamera.transform.position, positionVector, UnityEngine.Color.red);
                }
            }
        }

        if (_stickButton)
        {
            float input = Input.GetAxis("Horizontal") * -1;

            _leftCamera.transform.RotateAround(playerObj.position, Vector3.up, input * 3f);
            _leftCamera.transform.LookAt(lookAtObj);

            // Rayをカメラからプレイヤーに飛ばす
            ray = new Ray(_leftCamera.transform.position, positionVector);

            // Rayを可視化するデバック
            //Debug.DrawRay(_leftCamera.transform.position, positionVector, UnityEngine.Color.red);
        }

        RaycastHit[] _hits = Physics.SphereCastAll(ray, radio, positionVector.magnitude, layer);

        saveObj = hitObj.ToArray();
        hitObj.Clear();
        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit _hit in _hits)
        {
            // 遮蔽物の Renderer コンポーネントを無効にする
            GameObject _renderer = _hit.collider.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
            if (_renderer != null)
            {
                hitObj.Add(_renderer);
                _renderer.SetActive(false);
            }
        }

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (GameObject _renderer in saveObj.Except(hitObj))
        {
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            if (_renderer != null)
            {
                _renderer.SetActive(true);
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage") && _mainCameraObj.activeSelf)
        {
            _mainCameraObj.GetComponent<SphereCollider>().isTrigger = true;
        }

        if (collision.gameObject.CompareTag("Stage") && _leftCameraObj.activeSelf)
        {
            _leftCameraObj.GetComponent<SphereCollider>().isTrigger = true;
        }

        if (collision.gameObject.CompareTag("Stage") && _rightCameraObj.activeSelf)
        {
            _rightCameraObj.GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stage") && _mainCameraObj.activeSelf)
        {
            _mainCameraObj.GetComponent<SphereCollider>().isTrigger = false;
        }

        if (other.gameObject.CompareTag("Stage") && _leftCameraObj.activeSelf)
        {
            _leftCameraObj.GetComponent<SphereCollider>().isTrigger = false;
        }

        if (other.gameObject.CompareTag("Stage") && _rightCameraObj.activeSelf)
        {
            _rightCameraObj.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}
