using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScene : MonoBehaviour
{
    [SerializeField, Tooltip("追加読み込みするシーンの名前")]
    private List<string> _sceneNameList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        // リスト内に記述されているシーンを追加する
        foreach(string sceneName in _sceneNameList)
        {
            SceneCtl.instance.AddScene(sceneName);
        }
    }
}
