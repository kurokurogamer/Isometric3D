using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[ExecuteInEditMode]
public class EnemySearch : MonoBehaviour
{
    [SerializeField]
    private Color _color = Color.white;
    [SerializeField]
    private float _distans = 0.5f;
    [SerializeField]
    private float _searchAngle = 60f;
    private SphereCollider _sphere;
    private NavMeshAgent _agent;
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _sphere = GetComponent<SphereCollider>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    }

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
            Debug.Log("当たり判定に入っている");
            var playerDirection = other.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);
            if(angle <= _searchAngle)
			{
                Debug.Log("プレイヤーの発見");
                _agent.isStopped = false;
                Debug.Log(_agent.isStopped);
                _agent.SetDestination(other.transform.position);
			}
            else
			{
                Debug.Log("範囲外に移動");
                _agent.isStopped = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("範囲外に移動");
            _agent.isStopped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = _color;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_searchAngle, 0f) * transform.forward, _searchAngle * 2f, _sphere.radius);
    }
}
