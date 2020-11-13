using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMap : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask = 0;
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
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Vector3 point = transform.position + new Vector3(x * 2-9, 0, y * 2-9);
                if (Physics.Raycast(point, Vector3.down, out RaycastHit hit, 100, _layerMask, QueryTriggerInteraction.Ignore))
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(hit.point, 0.1f);
                }
            }
        }
    }
}
