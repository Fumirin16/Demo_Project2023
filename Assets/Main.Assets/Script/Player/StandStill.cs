using UnityEngine;

// 作成者：山﨑晶
// 足踏みしているかを判定するソースコード
public class StandStill : MonoBehaviour
{
    #region --- Fields ---

    /// <summary>
    /// 右足のオブジェクトを取得する変数 
    /// </summary>
    [SerializeField]
    private GameObject _footObj;

    /// <summary>
    /// 右足の構造体を宣言と初期化
    /// </summary>
    private FootPosition _foot;

    /// <summary>
    /// 歩く判定をする距離の差の値
    /// </summary>
    private float _reactionValume = 0.1f;

    /// <summary>
    /// 歩かせる強さを保存する変数
    /// </summary>
    private float powerSource = 0f;

    [HideInInspector]
    public float moveWalkPower = 0f;

    /// <summary>
    /// 足が動いているかの判定をする変数
    /// </summary>
    private bool _moveFoot = false;

    /// <summary>
    /// 値を管理するアセットから値を参照する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager settingManager;

    [SerializeField]
    private AudioManager audioManager;

    bool asibumisitaka;

    [HideInInspector]
    public int WalkCount;

    #endregion --- Fields ---

    #region --- Methods ---

    // Start is called before the first frame update
    void Start()
    {
        // 値を参照して保存する
        _reactionValume = settingManager.FootDistanceFoor;

        // 右足の初期位置を保存する
        _foot = new FootPosition(_footObj.transform.position, _footObj.transform.position, 0);
        //Debug.Log("StandStill  initiFoot : " + _foot);

        powerSource = settingManager.movePower;
    }

    // Update is called once per frame
    void Update()
    {
        // 二点の距離を保存する
        _foot.distance = FuncDistance(_footObj.transform.position, _foot.initiPosition);
        //Debug.Log("StandStill  distancce : " + _foot.distance);

        WalkPower();
    }

    /// <summary>
    /// 初期位置を保存する関数
    /// </summary>
    /// <param name="initiPosition"> 初期位置を保存するための変数 </param>
    /// <param name="obj"> オブジェクトの位置 </param>
    void InitialPosition(out Vector3 initiPosition, GameObject obj)
    {
        initiPosition = obj.transform.position;
    }

    /// <summary>
    /// 二点の距離の差を求める
    /// </summary>
    /// <param name="obj"> 現在のオブジェクトの位置 </param>
    /// <param name="initi"> 初期のオブジェクトの位置 </param>
    /// <returns> 二点の距離の返す </returns>
    float FuncDistance(Vector3 obj, Vector3 initi)
    {
        return Vector3.Distance(obj, initi);
    }

    /// <summary>
    /// 歩くための動力を保存する関数
    /// </summary>
    void WalkPower()
    {
        if (_reactionValume < _foot.distance && _moveFoot&& asibumisitaka)
        {
            if (audioManager.CheckPlaySound(audioManager.seAudioSource))
            {
                audioManager.PlaySESound(SEData.SE.WalkMini);
            }
            //Debug.Log("aaaaaaaaaaaaaaaaaaaaaa");

            //if(asibumisitaka)
            //{
            //    WalkCount++;
            //    asibumisitaka = false;
            //}
            WalkCount++;

            moveWalkPower = powerSource;
        }
    }

    /// <summary>
    /// 地面についているときの判定用関数
    /// </summary>
    /// <param name="collision"> 当たっているオブジェクト </param>
    private void OnCollisionStay(Collision collision)
    {
        // 当たったオブジェクトのタグがGroundだった場合
        if (collision.gameObject.tag == "Ground")
        {
            // 動いていない判定にする
            _moveFoot = false;

            // 動力を０にする
            moveWalkPower = 0f;

            //Debug.Log("StandStill  入っている。");
        }
    }

    /// <summary>
    /// 地面から離れたときの判定用関数
    /// </summary>
    /// <param name="collision"> 当たっているオブジェクト </param>
    private void OnCollisionExit(Collision collision)
    {
        // 当たっていない
        if (collision.gameObject.tag == "Ground")
        {
            // 動いている判定にする
            _moveFoot = true;

            asibumisitaka = true;

            //Debug.Log("StandStill  抜けた。");
        }
    }

    #endregion --- Methods ---

    #region --- Structs ---

    /// <summary>
    /// 足の座標を保存する構造体
    /// </summary>
    private struct FootPosition
    {
        // 初期位置を保存する変数
        public Vector3 initiPosition;

        // 現在位置を保存する変数
        public Vector3 nowPosition;

        // 初期位置と現在の位置の距離を保存する変数
        public float distance;

        /// <summary>
        /// 構造体の初期化
        /// </summary>
        /// <param name="initi"> 初期位置の変数 </param>
        /// <param name="now"> 現在位置の変数 </param>
        /// <param name="length"> 2点間の距離の変数 </param>
        public FootPosition(Vector3 initi, Vector3 now, float length)
        {
            initiPosition = initi;
            nowPosition = now;
            distance = length;
        }
    }

    #endregion --- Structs ---
}
