//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using Unity.VisualScripting;
//using UnityEditor.PackageManager.UI;
//using UnityEngine;

//public class CameratoAudioManager : MonoBehaviour
//{
//    [SerializeField]
//    private Transform lookAtObj;

//    private bool isRay = false;

//    private List<GameObject> hitObj = new List<GameObject>();

//    private int count = 0;

//    //private List<SpriteRenderer> hitObj = new List<SpriteRenderer>();

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // ２点間のベクトルを正規化する
//        Vector3 positionVector = (lookAtObj.transform.position - this.transform.position).normalized;

//        // Rayをカメラからプレイヤーに飛ばす
//        Ray ray = new Ray(this.transform.position, positionVector);

//        // Rayを可視化するデバック
//        Debug.DrawRay(this.transform.position, positionVector,UnityEngine.Color.red);

//        RaycastHit hit;
//        if (Physics.SphereCast(ray,1f,out hit))
//        {
//            if (hit.collider.CompareTag("Audience"))
//            {
//                hitObj.Add(hit.collider.gameObject);
//            }
//        }

//        if (count > 100)
//        {
//            foreach (var num in hitObj)
//            {
//                Debug.Log("当たったオブジェクト : " + num);
//            }
//        }

//        if (count > 600)
//        {
//            count = 0;
//        }

//        count++;
//    }
//}
