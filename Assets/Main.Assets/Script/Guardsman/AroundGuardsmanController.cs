using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // �i�r�Q�[�V�����g���ۂɕK�v
using UnityEngine.UI;  // UI�֘A�ɕK�v

// Navigation��p��������^�x�����̓���
// �쐬�ҁF�n����

public class AroundGuardsmanController : MonoBehaviour
{
    // 
    int destPoint = 0;
    // Guardsman��NavMeshAgent�擾
    NavMeshAgent agent;

    // �x�����̒��p�|�C���g
    [SerializeField] Transform[] points;

    [Tooltip("Player�̃I�u�W�F�N�g�����")]
    [SerializeField] GameObject target;

    bool flag = false;

    [SerializeField] Image haken;

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent�擾
        agent = GetComponent<NavMeshAgent>();

        //autoBraking �𖳌��ɂ���ƖڕW�n�_�̊Ԃ��p���I�Ɉړ�
        //�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ�
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //�G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă����玟�̖ڕW�n�_��I��
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if (flag == true)
        {
            agent.destination = target.transform.position;
        }
        if (flag == false)
        {
            agent.destination = points[destPoint].position;
        }
    }

    void GotoNextPoint()
    {
        //�n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ�
        if (points.Length == 0)
        {
            return;
        }

        //�G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ�
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�K�v�Ȃ�Ώo���n�_�ɂ��ǂ�
        destPoint = (destPoint + 1) % points.Length;

        if (destPoint == 4)
        {
            destPoint = 0;
        }
    }

    //���E�ɓ�������ǂ������Ă���
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("���E������");
            flag = true;
            haken.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("���E�ł�");
            flag = false;
            haken.gameObject.SetActive(false);
        }
    }

    //�v���C���[�ɓ���������Q�[���I�[�o�[
    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        // �Q�[���I�[�o�[�̔����true�ɂ���
    //        VariablesController.gameOverControl = true;

    //        Debug.Log("�Q�[���I�[�o�[");
    //    }
    //}
}
