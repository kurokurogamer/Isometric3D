using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugEditUI : MonoBehaviour
{
    private bool _debugMode = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if(_debugMode)
		{
            gameObject.SetActive(true);
		}
        else
		{
            gameObject.SetActive(false);
		}
    }
}
