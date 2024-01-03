using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Navigationを用いた巡回型警備員の動き
// 作成者：地引翼

public class AroundGuardsmanController : MonoBehaviour
{
    // なんかいれる
    int _destPoint = 0;
    // 視界入ってるか入ってないか判定する変数
    bool _targetFlag = false;

    bool _SEflag = true;
    // GuardsmanのNavMeshAgent取得
    NavMeshAgent _agent;

    // 警備員の中継ポイント
    [SerializeField] Transform[] _points;

    [Tooltip("Playerのオブジェクト入れる")]
    [SerializeField] GameObject _target;

    [Tooltip("見つかった時のUI")]
    [SerializeField] Image _haken;

    [SerializeField] ValueSettingManager settingManager;

    [SerializeField] AudioManager audioManager; 

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent取得
        _agent = GetComponent<NavMeshAgent>();
        //NavMeshAgentの値を参照して保存
        _agent.speed = settingManager.guardMoveSpeed;
        _agent.angularSpeed = settingManager.guardAngularSpeed;
        _agent.acceleration = settingManager.guardAcceleration;

        //autoBraking を無効にすると目標地点の間を継続的に移動
        //つまり、エージェントは目標地点に近づいても速度を落とさない
        _agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //エージェントが現目標地点に近づいてきたら次の目標地点を選択
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
        //地点がなにも設定されていないときに返す
        if (_points.Length == 0)
        {
            return;
        }

        //エージェントが現在設定された目標地点に行くように設定
        _agent.destination = _points[_destPoint].position;
        // 配列内の次の位置を目標地点に設定し必要ならば出発地点にもどる
        _destPoint = (_destPoint + 1) % _points.Length;

        if (_destPoint == 4)
        {
            _destPoint = 0;
        }
    }

    //視界に入ったら追いかけてくる
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_SEflag)
            {
                audioManager.PlaySESound(SEData.SE.FoundSecurity);
                //Debug.Log("視界入った");
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
            //Debug.Log("視界でた");
            _targetFlag = false;
            _SEflag = true;
            _haken.gameObject.SetActive(false);
        }
    }
}
