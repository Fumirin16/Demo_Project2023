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
//        // �Q�_�Ԃ̃x�N�g���𐳋K������
//        Vector3 positionVector = (lookAtObj.transform.position - this.transform.position).normalized;

//        // Ray���J��������v���C���[�ɔ�΂�
//        Ray ray = new Ray(this.transform.position, positionVector);

//        // Ray����������f�o�b�N
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
//                Debug.Log("���������I�u�W�F�N�g : " + num);
//            }
//        }

//        if (count > 600)
//        {
//            count = 0;
//        }

//        count++;
//    }
//}
