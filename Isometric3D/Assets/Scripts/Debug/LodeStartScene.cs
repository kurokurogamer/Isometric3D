using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodeStartScene : MonoBehaviour
{
    [SerializeField, Tooltip("シーン名の配列")]
    private string[] _sceneName = new string[0];
    // Start is called before the first frame update
    void Start()
    {
        foreach (string sceneName in _sceneName)
        {
            SceneCtl.instance.AddScene(sceneName);
        }
    }
}
