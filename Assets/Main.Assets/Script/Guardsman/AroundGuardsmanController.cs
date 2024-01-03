using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Navigation��p��������^�x�����̓���
// �쐬�ҁF�n����

public class AroundGuardsmanController : MonoBehaviour
{
    // �Ȃ񂩂����
    int _destPoint = 0;
    // ���E�����Ă邩�����ĂȂ������肷��ϐ�
    bool _targetFlag = false;

    bool _SEflag = true;
    // Guardsman��NavMeshAgent�擾
    NavMeshAgent _agent;

    // �x�����̒��p�|�C���g
    [SerializeField] Transform[] _points;

    [Tooltip("Player�̃I�u�W�F�N�g�����")]
    [SerializeField] GameObject _target;

    [Tooltip("������������UI")]
    [SerializeField] Image _haken;

    [SerializeField] ValueSettingManager settingManager;

    [SerializeField] AudioManager audioManager; 

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent�擾
        _agent = GetComponent<NavMeshAgent>();
        //NavMeshAgent�̒l���Q�Ƃ��ĕۑ�
        _agent.speed = settingManager.guardMoveSpeed;
        _agent.angularSpeed = settingManager.guardAngularSpeed;
        _agent.acceleration = settingManager.guardAcceleration;

        //autoBraking �𖳌��ɂ���ƖڕW�n�_�̊Ԃ��p���I�Ɉړ�
        //�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ�
        _agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă����玟�̖ڕW�n�_��I��
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if (_targetFlag == true)
        {
            _agent.destination = _target.transform.position;
        }
        if (_targetFlag == false)
        {
            _agent.destination = _points[_destPoint].position;
        }
    }

    void GotoNextPoint()
    {
        //�n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ�
        if (_points.Length == 0)
        {
            return;
        }

        //�G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ�
        _agent.destination = _points[_destPoint].position;
        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�K�v�Ȃ�Ώo���n�_�ɂ��ǂ�
        _destPoint = (_destPoint + 1) % _points.Length;

        if (_destPoint == 4)
        {
            _destPoint = 0;
        }
    }

    //���E�ɓ�������ǂ������Ă���
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_SEflag)
            {
                audioManager.PlaySESound(SEData.SE.FoundSecurity);
                //Debug.Log("���E������");
                _targetFlag = true;
                _haken.gameObject.SetActive(true);
                _SEflag = false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("���E�ł�");
            _targetFlag = false;
            _SEflag = true;
            _haken.gameObject.SetActive(false);
        }
    }
}
