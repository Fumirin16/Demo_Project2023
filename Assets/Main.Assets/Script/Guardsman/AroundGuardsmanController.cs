using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // ナビゲーション使う際に必要
using UnityEngine.UI;  // UI関連に必要

// Navigationを用いた巡回型警備員の動き
// 作成者：地引翼

public class AroundGuardsmanController : MonoBehaviour
{
    // なんかいれる
    int _destPoint = 0;
    // GuardsmanのNavMeshAgent取得
    NavMeshAgent _agent;

    // 警備員の中継ポイント
    [SerializeField] Transform[] _points;

    [Tooltip("Playerのオブジェクト入れる")]
    [SerializeField] GameObject _target;

    bool _flag = false;

    [Tooltip("見つかった時のUI")]
    [SerializeField] Image _haken;

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent取得
        _agent = GetComponent<NavMeshAgent>();

        //autoBraking を無効にすると目標地点の間を継続的に移動
        //つまり、エージェントは目標地点に近づいても速度を落とさない
        _agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //エージェントが現目標地点に近づいてきたら次の目標地点を選択
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if (_flag == true)
        {
            _agent.destination = _target.transform.position;
        }
        if (_flag == false)
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
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("視界入った");
            _flag = true;
            _haken.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("視界でた");
            _flag = false;
            _haken.gameObject.SetActive(false);
        }
    }
}
