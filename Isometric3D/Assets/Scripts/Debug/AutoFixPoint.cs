using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFixPoint : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask = 1;
    [SerializeField, Tooltip("補正する基準の高さ")]
    private float _height = 0.0f;

    // 実行時にオブジェクトの座標を地面の高さに合わせて修正する
    void Start()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f,_layerMask,QueryTriggerInteraction.Ignore))
		{
            transform.position = hit.point + new Vector3(0, _height, 0);
		}
        Destroy(this);
    }
}
