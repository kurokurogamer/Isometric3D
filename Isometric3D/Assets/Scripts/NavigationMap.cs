using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavigationMap : MonoBehaviour
{
    [SerializeField]
    private LayerMask _CheckLayer = 0;
    [SerializeField]
    private LayerMask _NoLayer = 0;
    [SerializeField]
    private float areaSize = 50.0f;
    [SerializeField]
    private int _oneSize = 1;
    // ルート
    private List<Vector3> _rootList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        _rootList = new List<Vector3>();
        for (int y = 0; y < areaSize; y += _oneSize)
        {
            for (int x = 0; x < areaSize; x += _oneSize)
            {
				Vector3 point = transform.position + new Vector3(x, 1, y);
				if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, 1, _CheckLayer, QueryTriggerInteraction.Ignore))
                {
                    _rootList.Add(point);
                }
            }
        }
    }

    private void Map()
	{
        foreach(var vec in _rootList)
		{
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    // デバック表示:ビルド時は必要のないメソッドなので処理をスキップするようにする
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
	{
        List<Vector3> _upPoint = new List<Vector3>();
        for (int i = 0; i < _rootList.Count; i++)
        {
            if (_upPoint.Count > areaSize / _oneSize)
            {
                _upPoint.RemoveAt(0);
            }
            // 球体の描画(ノード位置)
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_rootList[i], 0.5f);

            Gizmos.color = Color.blue;


            if (i % (areaSize / _oneSize)  != 0)
            {
                if (Physics.Linecast(_rootList[i], _rootList[i - 1]))
                {
                    Gizmos.color = Color.blue;
                }
                else
				{
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(_rootList[i], _rootList[i - 1]);
            }

            if((i - _upPoint.Count) > 0)
			{
                if (Physics.Linecast(_rootList[i], _upPoint[0]))
                {
                    Gizmos.color = Color.blue;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(_rootList[i], _upPoint[0]);
			}


			_upPoint.Add(_rootList[i]);
            if (_upPoint.Count > areaSize / _oneSize)
            {
                _upPoint.RemoveAt(0);
            }
		}
    }
}
