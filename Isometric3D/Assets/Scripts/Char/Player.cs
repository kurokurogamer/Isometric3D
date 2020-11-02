using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // キャラクターの状態
    public enum CharState
    {
        NOMAL,  // 
        TALK,   // 会話中
        MAX
    }
    private CharState _state;
   public CharState state
    {
        get { return _state; }
        set { _state = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _state = CharState.NOMAL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
