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
        // �Q�_�Ԃ̃x�N�g���𐳋K������
        Vector3 positionVector = (lookAtObj.transform.position - this.transform.position).normalized;

        // Ray���J��������v���C���[�ɔ�΂�
        Ray ray = new Ray(this.transform.position, positionVector);

        // Ray����������f�o�b�N
        Debug.DrawRay(this.transform.position, positionVector, UnityEngine.Color.red);

        RaycastHit[] _hits = Physics.SphereCastAll(ray, radian, positionVector.magnitude, layer);

        saveHit = rendereHit.ToArray();
        rendereHit.Clear();
        // �Օ����͈ꎞ�I�ɂ��ׂĕ`��@�\�𖳌��ɂ���B
        foreach (RaycastHit _hit in _hits)
        {
            // �Օ�������ʑ̂̏ꍇ�͗�O�Ƃ���
            if (_hit.collider.gameObject == player)
            {
                continue;
            }

            // �Օ����� Renderer �R���|�[�l���g�𖳌��ɂ���
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


