using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ϋq�������_������
// �쐬�ҁF�n����

public class RandomNPCScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������GameObject")]
    GameObject _createPrefab;

    [SerializeField]
    [Tooltip("��������͈�A")]
    Transform _rangeA;

    [SerializeField]
    [Tooltip("��������͈�B")]
    Transform _rangeB;

    [SerializeField]
    [Tooltip("���������")]
    int _pieces = 0;

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
        while (0 < _pieces)
        {
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float _x = Random.Range(_rangeA.position.x, _rangeB.position.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float _y = Random.Range(_rangeA.position.y, _rangeB.position.y);
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float _z = Random.Range(_rangeA.position.z, _rangeB.position.z);

            // ��������I�u�W�F�N�g�̍��W��ۑ�����
            Vector3 _pos = new Vector3(_x, _y, _z);
            // �e���ɂ��Ẵ{�b�N�X�T�C�Y�̔�����ۑ�
            Vector3 _halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

            // �w�肵���{�b�N�X�����̃R���C�_�[�Əd�Ȃ��Ă��邩�m�F
            if (!Physics.CheckBox(_pos, _halfExtents))
            {
                // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
                Instantiate(_createPrefab, new Vector3(_x, _y, _z), _createPrefab.transform.rotation);
            }
            else
            {
                continue;
            }
            _pieces--;
        }
    }
}
