using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System.Diagnostics;

//[ExecuteInEditMode]
public class EnemySearch : MonoBehaviour
{
    [SerializeField, Tooltip("サーチ範囲視認用カラー")]
    private Color _color = new Color(1, 0, 0, 0.25f);
    [SerializeField, Tooltip("壁判定用のレイヤーマスク")]
    private LayerMask _layerMask = 0;
    [SerializeField, Tooltip("サーチ範囲の角度")]
    private float _searchAngle = 60f;
    [SerializeField, Tooltip("巡回ポイント")]
    private List<GameObject> _pointList = new List<GameObject>();
    private int _listCount;

    private GameObject _target = null;

    [SerializeField, Tooltip("巡回待機時間")]
    private float _secondTime = 2.0f;
    private float _nowSecondTime; 
    private NavMeshAgent _agent;
    // Gizmos表示に使用するradius取得用変数
    private SphereCollider _sphere;

    // Start is called before the first frame update
    void Start()
    {
        // 敵の追従用
        _agent = GetComponent<NavMeshAgent>();
        _listCount = 0;
        _nowSecondTime = 0.0f;
    }

    // 追従処理
    private void Follow()
	{
        if (_target != null)
        {
            UnityEngine.Debug.Log("当たり判定に入っている");
            var playerDirection = _target.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);

            if (angle <= _searchAngle)
            {
                if (Physics.Linecast(transform.position, _target.transform.position, out RaycastHit hit, _layerMask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.tag == "Player")
                    {
                        _agent.isStopped = false;
                        _agent.SetDestination(_target.transform.position);
                    }
                }
                    UnityEngine.Debug.Log("プレイヤーの発見");
            }
            else
            {
                UnityEngine.Debug.Log("範囲外に移動");
				_agent.isStopped = true;
				_target = null;
            }
        }
    }

    // 巡回処理
    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointList[_listCount].transform.position, Time.deltaTime * _agent.speed);
        UnityEngine.Debug.Log(Vector3.Distance(transform.position, _pointList[_listCount].transform.position));
		if (Vector3.Distance(transform.position,_pointList[_listCount].transform.position) <= 0f)
		{
            _listCount++;
            _nowSecondTime = 0;
            if(_listCount >= _pointList.Count)
			{
                _listCount = 0;
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (_nowSecondTime <= _secondTime)
        {
            Quaternion targetRot = Quaternion.LookRotation(_pointList[_listCount].transform.position - transform.position);
            // プレイヤーの回転
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 10);

            _nowSecondTime += Time.deltaTime;
        }

        if(_nowSecondTime > _secondTime)
		{
            Patrol();
		}
		Follow();
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            UnityEngine.Debug.Log("範囲外に移動");
            _target = other.gameObject;
        }
    }

  

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UnityEngine.Debug.Log("範囲外に移動");
			_agent.isStopped = true;
			_target = null;
		}
    }

    [Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
    {
		if (_sphere == null)
		{
			_sphere = GetComponent<SphereCollider>();
		}

        Handles.color = _color;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_searchAngle, 0f) * transform.forward, _searchAngle * 2f, _sphere.radius);

		if (_target != null)
        {
            if (Physics.Linecast(transform.position, _target.transform.position, out RaycastHit hit, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.tag == "Player")
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawLine(transform.position, hit.point);
            }
        }

    }
}
