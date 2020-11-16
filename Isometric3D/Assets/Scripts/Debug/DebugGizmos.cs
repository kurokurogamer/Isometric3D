using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DebugGizmos : MonoBehaviour
{
    private enum TYPE
	{
        CUBE,
        SPHERE,
        MAX
	}

    [SerializeField, Tooltip("")]
    private TYPE _type = TYPE.CUBE;
    [SerializeField, Tooltip("ギズモの色指定")]
    private Color _color = Color.white;
    [SerializeField]
    public bool _editMode = true;
    public static bool Mode = true;
    private MeshFilter _meshFilter;
    private MeshRenderer _renderer;


    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        //_renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
      }

    // Update is called once per frame
    void Update()
    {
        if(_meshFilter)
		{
            if(Mode)
			{
                _renderer.enabled = true;
			}
            else
			{
                _renderer.enabled = false;
			}
		}
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = _color;
        Matrix4x4 matrix = transform.localToWorldMatrix;
        Gizmos.matrix = matrix;
		switch (_type)
		{
			case TYPE.CUBE:
				Gizmos.DrawCube(Vector3.zero, Vector3.one);
				break;
			case TYPE.SPHERE:
                Gizmos.DrawSphere(Vector3.zero, 0.5f);
                break;
			case TYPE.MAX:
			default:
				break;
		}
	}
}