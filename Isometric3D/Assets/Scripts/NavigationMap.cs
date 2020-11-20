using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMap : MonoBehaviour
{
    [SerializeField]
    private LayerMask _CheckLayer = 0;
    [SerializeField]
    private LayerMask _NoLayer = 0;
    [SerializeField]
    private int _areaSize = 20;
    [SerializeField]
    private int _oneSize = 1;
    [SerializeField]
    private float _size = 0.5f;

    public class PointNode
	{
        public Vector3 point = Vector3.zero;
        public bool up       = false;
        public bool right    = false;
        public bool upRight  = false;
        public bool downRight= false;
	}

    public Dictionary<int, PointNode> _node;

    // Start is called before the first frame update
    void Start()
    {
        _node = new Dictionary<int, PointNode>();
        for (int y = 0; y < _areaSize; y += _oneSize)
        {
            for (int x = 0; x < _areaSize; x += _oneSize)
            {
				Vector3 point = transform.position + new Vector3(x, 1, y);
				if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, 1, _CheckLayer, QueryTriggerInteraction.Ignore))
                {
                    _node.Add(_node.Count, new PointNode());
                    _node[_node.Count - 1].point = point;
                }
            }
        }

        WallCheck();
    }

    private void WallCheck()
    {
        List<Vector3> _upPoint = new List<Vector3>();

        for (int i = 0; i < _node.Count; i++)
        {
            if (i % (_areaSize / _oneSize) != 0)
            {
                if (Physics.Linecast(_node[0].point, _node[i - 1].point))
                {        
                    Debug.Log("壁に衝突している");
                    _node[i].right = true;
                }
                else
                {
                    Debug.Log("衝突していない");
                    _node[i].right = false;
                }
            }

            Debug.Log(i + (_areaSize / _oneSize));
            if (i + (_areaSize / _oneSize) < _node.Count)
            {
				if (Physics.Linecast(_node[i].point, _node[i + (_areaSize / _oneSize)].point))
				{
					_node[i].up = true;
					Debug.Log("壁に衝突している");
				}
				else
				{
					_node[i].up = false;
					Debug.Log("衝突していない");
				}
			}
        }
    }

    private void Map()
	{

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    //// デバック表示:ビルド時は必要のないメソッドなので処理をスキップするようにする
    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
	{
        if(_node == null)
		{
            return;
		}
        List<Vector3> _upPoint = new List<Vector3>();
        for (int i = 0; i < _node.Count; i++)
        {
            //Debug.Log(i + "ノード番号" + _node[i].up);
            // 球体の描画(ノード位置)
            Gizmos.color = Color.red;
			Gizmos.DrawSphere(_node[i].point, _size);

			if (i % (_areaSize / _oneSize) != 0)
			{
				if (_node[i].right)
				{
					Gizmos.color = Color.blue;
				}
				else
				{
					Gizmos.color = Color.red;
				}
				Gizmos.DrawLine(_node[i].point, _node[i - 1].point);
			}

            if (i + (_areaSize / _oneSize) < _node.Count)
            {
                if (_node[i].up)
				{
					Gizmos.color = Color.blue;
				}
				else
				{
					Gizmos.color = Color.red;
				}
				Gizmos.DrawLine(_node[i].point, _node[i + (_areaSize / _oneSize)].point);
			}
		}
    }
}
