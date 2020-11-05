using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiUITest : MonoBehaviour
{
    [SerializeField]
    private GuidReference _targetUI = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_targetUI.gameObject.name);
    }
}
