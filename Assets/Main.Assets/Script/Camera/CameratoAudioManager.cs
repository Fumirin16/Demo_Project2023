using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameratoAudioManager : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtObj;

    [SerializeField]
    private LayerMask layer;

    private List<GameObject> hitObj=new List<GameObject> ();

    private GameObject[] saveObj;

    [SerializeField]
    private float radio=1.0f;

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

        RaycastHit[] _hits = Physics.SphereCastAll(ray, radio, positionVector.magnitude, layer);


        saveObj = hitObj.ToArray();
        hitObj.Clear();
        // �Օ����͈ꎞ�I�ɂ��ׂĕ`��@�\�𖳌��ɂ���B
        foreach (RaycastHit _hit in _hits)
        {
            // �Օ����� Renderer �R���|�[�l���g�𖳌��ɂ���
            GameObject _renderer = _hit.collider.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
            if (_renderer != null)
            {
                hitObj.Add(_renderer);
                _renderer.SetActive(false);
            }
        }

        // �O��܂őΏۂŁA����ΏۂłȂ��Ȃ������̂́A�\�������ɖ߂��B
        foreach (GameObject _renderer in saveObj.Except(hitObj))
        {
            // �Օ����łȂ��Ȃ��� Renderer �R���|�[�l���g��L���ɂ���
            if (_renderer != null)
            {
                _renderer.SetActive(true);
            }
        }

    }
}
