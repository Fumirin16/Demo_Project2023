using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class CameratoAudioManager : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtObj;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private GameObject player;

    private List<GameObject> rendereHit = new List<GameObject>();

    private GameObject[] saveHit;

    [SerializeField]
    private float radian=1.0f;

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

        RaycastHit[] _hits = Physics.SphereCastAll(ray, radian, positionVector.magnitude, layer);

        saveHit = rendereHit.ToArray();
        rendereHit.Clear();
        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit _hit in _hits)
        {
            // 遮蔽物が被写体の場合は例外とする
            if (_hit.collider.gameObject == player)
            {
                continue;
            }

            // 遮蔽物の Renderer コンポーネントを無効にする
            GameObject _renderer = _hit.collider.gameObject.transform.GetChild(12).gameObject;
            if (_renderer != null)
            {
                rendereHit.Add(_renderer);
                _renderer.SetActive(false);
            }
        }

        foreach (GameObject num in saveHit.Except(rendereHit))
        {
            if (num != null)
            {
                num.SetActive(true);
            }
        }
    }
}


