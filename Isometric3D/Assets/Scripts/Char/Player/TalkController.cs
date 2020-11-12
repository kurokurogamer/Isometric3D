using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkController : EventController
{
    // イベント用スクリプト
    private EventCollider _eventCollider = default;

    // 会話の状態
    public enum TalkState
    {
        ON,     // 会話中
        OFF,    // 会話終了
        MAX
    }
    private TalkState _state;
    public TalkState State
    {
        get { return _state; }
    }

   　// 表示する吹き出し画像
    private Image _speechBalloon = default;

    // 表示するテキストui
    private Text _talkText = default;

    // オブジェクトの上のアイコン
    private GameObject _icon = default;

    // 表示する会話文
    private List<string> _textMessageList = default;

    // 文字の表示スピード
    private float _defTextSpeed = 0.05f;
    
    private float _textSpeed = default;
    // 現在表示中の文字数
    private int _textCount = 0;
    // 現在表示中の会話配列
    private int _textMessageIndex = 0;
    // 1回分の会話を表示したかどうか
    private bool _isOneMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        _eventCollider = gameObject.GetComponent<EventCollider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 会話データのセット
    public override void SetData(EventObj obj)
    {
        //TalkObj talk = obj.GetComponent<TalkObj>();

        //// 吹き出し
        //_speechBalloon = talk.SpeechBalloon;
        //// 会話UIText
        //_talkText = talk.TalkText;
        //// 頭上のアイコン
        //_icon = talk.Icon;
        //// 表示する会話文
        //_textMessageList = talk.TextMessageList;
        //// 文字の表示スピード
        //_defTextSpeed = talk.DefTextSpeed;
        //// 会話可能フラグ
        //_eventFlag = true;
    }

    public override bool EventUpData()
    {
        // アイコンを非表示に
        if (_icon != null)
        {
            _icon.gameObject.SetActive(false);
        }
        // 吹き出しと文字の表示
        _speechBalloon.gameObject.SetActive(true);
        _talkText.gameObject.SetActive(true);

        _state = TalkState.ON;
        // 文字をすべて表示していない場合
        if (!_isOneMessage)
        {
            // 経過時間で１文字ずつ出していく
            _textSpeed -= Time.deltaTime;

            // 文字をすべて表示していない場合
            if (_textMessageList[_textMessageIndex].Length > _textCount)
            {
                if (_textSpeed <= 0)
                {
                    // 一文字追加
                    _talkText.text += _textMessageList[_textMessageIndex][_textCount];
                    // 現在の文字数を増やす
                    _textCount++;
                    // 時間の初期化
                    _textSpeed = _defTextSpeed;

                    // 全部の表示が終わったら
                    if (_textCount >= _textMessageList[_textMessageIndex].Length)
                    {
                        // キー入力で一回の会話を
                        if (Input.GetButtonDown("Jump"))
                        {
                            // 次の会話の表示フラグをtrueにする
                            _isOneMessage = true;
                        }
                    }
                }
            }
            // キー入力があった場合、文字を全部表示させる
            if (Input.GetButtonDown("Jump"))
            {
                _talkText.text += _textMessageList[_textMessageIndex].Substring(_textCount);
                _isOneMessage = true;
            }
        }
        else
        {
            // 次の会話に移る
            _textMessageIndex++;
            // 吹き出し内の文字を消す
            _talkText.text = "";
            _textCount = 0;
            _textSpeed = _defTextSpeed;

            // すべての会話が終わった
            if (_textMessageIndex >= _textMessageList.Count)
            {
                ResetData();
                if (_icon != null)
                {
                    _icon.gameObject.SetActive(true);
                }

                // 会話終了にtrueを返す
                return true;
            }
            else
            {
                _isOneMessage = false;
            }
        }

        // 会話が終了しなければfalseを返す
        return false;
    }
    // 初期化
    public void ResetData()
    {
        _state = TalkState.OFF;
        _talkText.text = "";
        _textCount = 0;
        _textMessageIndex = 0;
        _speechBalloon.gameObject.SetActive(false);
        _talkText.gameObject.SetActive(false);
        _isOneMessage = false;
        _textSpeed = _defTextSpeed;
    }
}
