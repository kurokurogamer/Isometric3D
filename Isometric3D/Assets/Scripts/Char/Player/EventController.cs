using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
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
    public virtual void SetData(EventObj obj)
    {

    }

    public virtual bool EventUpData()
    {
        return true;
    }
}
