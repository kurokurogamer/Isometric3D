using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動用スクリプト
    private CharController _move = default;
    // 会話用スクリプト
    private TalkController _talk = default;
    // アイテム用スクリプト
    private ItemContlloer _item = default;

    // キャラクターの状態
    public enum CharState
    {
        NOMAL,  // 通常
        IVENT,   // 会話中
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
        _move = gameObject.GetComponent<CharController>();
        _talk = gameObject.GetComponent<TalkController>();
        _state = CharState.NOMAL;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case CharState.NOMAL:
                // 移動
                _move.Move();
                // イベント
                CheckIvent();
                break;
            case CharState.IVENT:
                // 会話
                _talk.Talk();
                if(_talk.State == TalkController.TalkState.OFF)
                {
                    _state = CharState.NOMAL;
                }
                break;
            default:
                break;
        }
    }

    // イベントがあるか確認
    void CheckIvent()
    {
        if (_talk.TalkFlag)
        {
            // スペースキーで会話開始
            if (Input.GetButtonDown("Jump"))
            {
                Input.ResetInputAxes();
                _talk.Talk();
                _state = CharState.IVENT;
            }
        }
        else
        {
            _state = CharState.NOMAL;
        }
    }
}
