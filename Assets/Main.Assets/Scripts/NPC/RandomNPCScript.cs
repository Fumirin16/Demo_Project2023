//�쐬�Ғn����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNPCScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    private GameObject createPrefab;

    [SerializeField]
    [Tooltip("��������͈�A")]
    private Transform rangeA;

    [SerializeField]
    [Tooltip("��������͈�B")]
    private Transform rangeB;

    [SerializeField]
    [Tooltip("���������")]
    private int pieces;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        while(0 < pieces)
        {
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(x, y, z);
            Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

            if(!Physics.CheckBox(pos,halfExtents))
            {
                // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
                Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);
            }
            else
            {
                continue;
            }
            pieces--;
        }
    }
}
