using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObj : MonoBehaviour
{
    [SerializeField, Tooltip("頭上のアイコン")]
    protected GameObject _icon = default;
    public GameObject Icon
    {
        get { return _icon; }
    }

    // 各種イベントフラグ(true = 可能,false = 不可)
    protected bool _eventFlag = false;
    public bool EventFlag
    {
        get { return _eventFlag; }
        set { _eventFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // アイコンを常にカメラの角度と同じようにする
    void LateUpdate()
    {
        if (_icon != null)
        {
            // アイコンが表示されているとき
            if (_icon.activeSelf)
            {
                _icon.gameObject.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }

    // イベントの更新
    public virtual bool EventUpData()
    {
        return true;
    }

    // イベントデータの初期化
    public virtual void ResetData()
    {

    }
}

