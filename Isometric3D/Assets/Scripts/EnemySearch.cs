using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

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
    [SerializeField]
    private GameObject _icon = null;
    [SerializeField]
    private AudioClip _clip = null;
    private Animator _anim;

    private int _listCount;


    [SerializeField, Tooltip("巡回待機時間")]
    private float _secondTime = 2.0f;
    // 現在の経過時間
    private float _nowSecondTime;
    // 巡回感覚時間
    private float _waitTime;
    [SerializeField]
    private float _speed = 1.0f;
    private GameObject _target;
    private NavMeshAgent _agent;
    // Gizmos表示に使用するradius取得用変数
    private SphereCollider _sphere;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(_icon, _icon.transform.position, _icon.transform.rotation);
        _icon = obj;
        _anim = _icon.GetComponent<Animator>();
        _icon.transform.SetParent(transform);
        _icon.transform.localPosition = Vector3.zero;
        _target = null;
        // 敵の追従用
        _agent = GetComponent<NavMeshAgent>();
        _listCount = 0;
        _nowSecondTime = 0.0f;
        _waitTime = 0.0f;
    }

    // 追従処理
    private void Follow()
	{
        if (_target != null)
        {
            Debug.Log("当たり判定に入っている");
            var playerDirection = _target.transform.position - transform.position; 

            var angle = Vector3.Angle(transform.forward, playerDirection);
            if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle")
            {
                _anim.Play("Fade");
                // 発見時の効果音
                if (_clip != null)
                {
                    AudioManager.instans.PlayOneSE(_clip);
                }
            }
            _icon.SetActive(true);
            _icon.transform.rotation = Quaternion.Euler(30, 45, 0);
            _icon.transform.position = transform.position;
            if (angle <= _searchAngle)
            {
                if (Physics.Linecast(transform.position, _target.transform.position, out RaycastHit hit, _layerMask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.tag == "Player")
                    {
                        _agent.isStopped = false;
                        _agent.SetDestination(_target.transform.position);
                        Debug.Log("壁が間にない");
                    }
                    else
                    {
                        Debug.Log("間に壁があります");
                        _agent.isStopped = true;
                    }
                }
            }
            else
			{
                Patrol();
			}
		}
	}

    // 巡回処理
    private void Patrol()
    {
        _agent.SetDestination(_pointList[_listCount].transform.position);
		transform.position = Vector3.MoveTowards(transform.position, _pointList[_listCount].transform.position, Time.deltaTime * _agent.speed);
		Debug.Log(Vector3.Distance(transform.position, _pointList[_listCount].transform.position));
		if (Vector3.Distance(transform.position,_pointList[_listCount].transform.position) <= 0.1f)
		{
            _waitTime += Time.deltaTime;
            if (_waitTime >= 3.0f)
            {
                _waitTime = 0f;
                _listCount++;
                _nowSecondTime = 0;
                if (_listCount >= _pointList.Count)
                {
                    _listCount = 0;
                }
            }
		}
	}

    // Update is called once per frame
    void Update()
    {
        _nowSecondTime += Time.deltaTime;
		if (_nowSecondTime <= _secondTime)
		{
			Quaternion targetRot = Quaternion.LookRotation(_pointList[_listCount].transform.position - transform.position);
			// プレイヤーの回転
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _speed);
		}

		if (_nowSecondTime > _secondTime)
		{
            Follow();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            Debug.Log("範囲外に移動");
            _target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("範囲外に移動");
			_agent.isStopped = true;
			_target = null;
		}
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
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
