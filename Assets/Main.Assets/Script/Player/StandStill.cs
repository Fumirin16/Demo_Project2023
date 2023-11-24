using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

// �쐬�ҁF�R����
// �����݂��Ă��邩�𔻒肷��\�[�X�R�[�h
public class StandStill : MonoBehaviour
{
    #region --- Fields ---

    /// <summary>
    /// �E���̃I�u�W�F�N�g���擾����ϐ� 
    /// </summary>
    [SerializeField]
    private GameObject _footObj;

    /// <summary>
    /// �E���̍\���̂�錾�Ə�����
    /// </summary>
    private FootPosition _foot;

    /// <summary>
    /// ������������鋗���̍��̒l
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _reactionValume = 0.1f;

    /// <summary>
    /// �������鋭����ۑ�����ϐ�
    /// </summary>
    [NonSerialized]
    public static float powerSource = 0f;

    /// <summary>
    /// ���������Ă��邩�̔��������ϐ�
    /// </summary>
    private bool _moveFoot = false;

    #endregion --- Fields ---

    #region --- Methods ---

    // Start is called before the first frame update
    void Start()
    {
        // �E���̏����ʒu��ۑ�����
        _foot = new FootPosition(_footObj.transform.position, _footObj.transform.position, 0);
        //Debug.Log("StandStill  initiFoot : " + _foot);
    }

    // Update is called once per frame
    void Update()
    {
        // ��_�̋�����ۑ�����
        _foot.distance = FuncDistance(_footObj.transform.position, _foot.initiPosition);
        //Debug.Log("StandStill  distancce : " + _foot.distance);

        WalkPower();
    }

    /// <summary>
    /// �����ʒu��ۑ�����֐�
    /// </summary>
    /// <param name="initiPosition"> �����ʒu��ۑ����邽�߂̕ϐ� </param>
    /// <param name="obj"> �I�u�W�F�N�g�̈ʒu </param>
    void InitialPosition(out Vector3 initiPosition, GameObject obj)
    {
        initiPosition = obj.transform.position;
    }

    /// <summary>
    /// ��_�̋����̍������߂�
    /// </summary>
    /// <param name="obj"> ���݂̃I�u�W�F�N�g�̈ʒu </param>
    /// <param name="initi"> �����̃I�u�W�F�N�g�̈ʒu </param>
    /// <returns> ��_�̋����̕Ԃ� </returns>
    float FuncDistance(Vector3 obj, Vector3 initi)
    {
        return Vector3.Distance(obj, initi);
    }

    /// <summary>
    /// �������߂̓��͂�ۑ�����֐�
    /// </summary>
    void WalkPower()
    {
        if (_reactionValume < _foot.distance&&_moveFoot)
        {
            powerSource = 1;
        }
    }

    /// <summary>
    /// �n�ʂɂ��Ă���Ƃ��̔���p�֐�
    /// </summary>
    /// <param name="collision"> �������Ă���I�u�W�F�N�g </param>
    private void OnCollisionStay(Collision collision)
    {
        // ���������I�u�W�F�N�g�̃^�O��Ground�������ꍇ
        if (collision.gameObject.tag == "Ground")
        {
            // �����Ă��Ȃ�����ɂ���
            _moveFoot = false;

            // ���͂��O�ɂ���
            powerSource = 0;

            //Debug.Log("StandStill  �����Ă���B");
        }
    }

    /// <summary>
    /// �n�ʂ��痣�ꂽ�Ƃ��̔���p�֐�
    /// </summary>
    /// <param name="collision"> �������Ă���I�u�W�F�N�g </param>
    private void OnCollisionExit(Collision collision)
    {
        // �������Ă��Ȃ�
        if (collision.gameObject.tag == "Ground")
        {
            // �����Ă��锻��ɂ���
            _moveFoot = true;

            //Debug.Log("StandStill  �������B");
        }
    }

    #endregion --- Methods ---

    #region --- Structs ---

    /// <summary>
    /// ���̍��W��ۑ�����\����
    /// </summary>
    private struct FootPosition
    {
        // �����ʒu��ۑ�����ϐ�
        public Vector3 initiPosition;

        // ���݈ʒu��ۑ�����ϐ�
        public Vector3 nowPosition;

        // �����ʒu�ƌ��݂̈ʒu�̋�����ۑ�����ϐ�
        public float distance;

        /// <summary>
        /// �\���̂̏�����
        /// </summary>
        /// <param name="initi"> �����ʒu�̕ϐ� </param>
        /// <param name="now"> ���݈ʒu�̕ϐ� </param>
        /// <param name="length"> 2�_�Ԃ̋����̕ϐ� </param>
        public FootPosition(Vector3 initi, Vector3 now, float length)
        {
            initiPosition = initi;
            nowPosition = now;
            distance = length;
        }
    }

    #endregion --- Structs ---
}
