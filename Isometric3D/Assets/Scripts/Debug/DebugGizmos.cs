using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{
    [SerializeField, Tooltip("ギズモの色指定")]
    private Color _color = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = _color;
        Matrix4x4 matrix = transform.localToWorldMatrix;
        Gizmos.matrix = matrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
	}
}
