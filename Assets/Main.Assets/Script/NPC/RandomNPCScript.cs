using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ϋq�������_������
// �쐬�ҁF�n����

public class RandomNPCScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    GameObject createPrefab;

    [SerializeField]
    [Tooltip("��������͈�A")]
    Transform rangeA;

    [SerializeField]
    [Tooltip("��������͈�B")]
    Transform rangeB;

    [SerializeField]
    [Tooltip("���������")]
    int pieces = 0;

    // Start is called before the first frame update
    void Start()
    {
        RundomNPC();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RundomNPC()
    {
        while (0 < pieces)
        {
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // ��������I�u�W�F�N�g�̍��W��ۑ�����
            Vector3 pos = new Vector3(x, y, z);
            // �e���ɂ��Ẵ{�b�N�X�T�C�Y�̔�����ۑ�
            Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

            // �w�肵���{�b�N�X�����̃R���C�_�[�Əd�Ȃ��Ă��邩�m�F
            if (!Physics.CheckBox(pos, halfExtents))
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
