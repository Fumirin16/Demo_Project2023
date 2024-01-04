using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameratoAudioManager : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtObj;

    [SerializeField]
    private LayerMask layer;

    private List<GameObject> hitObj=new List<GameObject> ();

    private GameObject[] saveObj;

    [SerializeField]
    private float radio=1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ２点間のベクトルを正規化する
        Vector3 positionVector = (lookAtObj.transform.position - this.transform.position).normalized;

        // Rayをカメラからプレイヤーに飛ばす
        Ray ray = new Ray(this.transform.position, positionVector);

        // Rayを可視化するデバック
        Debug.DrawRay(this.transform.position, positionVector, UnityEngine.Color.red);

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
}
