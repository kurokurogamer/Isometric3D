using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 抽象クラス
public abstract class ButtonAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // それぞれのボタンごとの処理を記述する
    public abstract void Action();

    // Update is called once per frame
    void Update()
    {
        
    }
}
