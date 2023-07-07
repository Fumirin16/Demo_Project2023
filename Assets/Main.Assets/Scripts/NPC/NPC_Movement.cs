using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPC_Movement : MonoBehaviour
{
    [SerializeField] public GameObject cheakPoint;  //�`�F�b�N�|�C���g���擾
    public bool moveLR;
    public bool comePoint;

    public float limitTime;
    private float npcSpeed = 1.0f;

    float length = 0.1f;
    float speed = 0.00001f;
    private Vector3 startPos;
    private Rigidbody rb;

    float saveTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //�`�F�b�N�|�C���g�ƌ��݂̈ʒu�̊p�x�����߂�
        Quaternion lookRotation = Quaternion.LookRotation(cheakPoint.transform.position - this.transform.position);

        //���݂̊p�x����`�F�b�N�|�C���g�܂ł̊p�x�ɉ�]����
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);

        //�i�݂��������ɉ������
        Vector3 _npcFoward = new Vector3(0f, 0f, (npcSpeed * Time.deltaTime));

        //NPC��O�i������
        this.transform.Translate(_npcFoward);

        //saveTime += Time.deltaTime;

        //if (saveTime < limitTime)
        //{
        //    //�`�F�b�N�|�C���g�ƌ��݂̈ʒu�̊p�x�����߂�
        //    Quaternion lookRotation = Quaternion.LookRotation(cheakPoint.transform.position - this.transform.position);

        //    //���݂̊p�x����`�F�b�N�|�C���g�܂ł̊p�x�ɉ�]����
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);

        //    //�i�݂��������ɉ������
        //    Vector3 _npcFoward = new Vector3(0f, 0f, (npcSpeed * Time.deltaTime));

        //    //NPC��O�i������
        //    this.transform.Translate(_npcFoward);
        //}
        //else
        //{
        //    npcSpeed *= -1.0f;
        //    saveTime = 0f;
        //}
    }
}
