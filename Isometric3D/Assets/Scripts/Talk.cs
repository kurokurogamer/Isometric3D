using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    // 会話の状態
    public enum TalkState
    {
        ON,     // 会話中
        OFF,    // 会話していない
        MAX
    }
    private TalkState _state;

    // 会話可能フラグ(true = 会話可能,false = 会話不可)
    private bool _talkFlag = false;

    [SerializeField, MultilineAttribute(3), Tooltip("会話文")]
    private string[] _textMessage = default;
    //現在表示中の文字数
    private int _textCount = 0;
    // 現在表示中の会話配列
    private int _textMessageIndex = 0;

    [SerializeField, Tooltip("表示スピード")]
    private float _defTextSpeed = 0.05f;
    private float _textSpeed = default;

    [SerializeField, Tooltip("表示する吹き出し画像")]
    private Image _speechBalloon = default;

    [SerializeField, Tooltip("表示するテキストui")]
    private Text _talkText = default;

    [SerializeField, Tooltip("頭上のアイコン")]
    private GameObject _icon = default;

    // 1回分のメッセージを表示したかどうか
    private bool _isOneMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        _state = TalkState.OFF;
        _textSpeed = _defTextSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // 会話可能フラグがture
        if (_talkFlag)
        {
            // スペースキーで会話開始
           // if (Input.GetButtonDown("Jump"))
            {
                _state = TalkState.ON;
            }
        }
        // 会話中
        if (_state == TalkState.ON)
        {
            Talking();
        }
    }

    // アイコンを常にカメラの角度と同じようにする
    void LateUpdate()
    {
        _icon.gameObject.transform.rotation = Camera.main.transform.rotation;
    }

    // 当たり判定
    // 入った時
    private void OnTriggerEnter(Collider other)
    {
        // キャラクターがコライダーに入ったら
        if (_state == TalkState.OFF
        &&  other.tag == "Player")
        {
            // 会話中でなければ
            if (other.gameObject.GetComponent<Player>().state <= Player.CharState.TALK)
            {
                _icon.gameObject.SetActive(true);
                _talkFlag = true;
            }
        }
    }
    // 出たとき
    private void OnTriggerExit(Collider other)
    {
        // キャラクターがコライダーから出たら
        if (other.tag == "Player")
        {
            TalkEnd();
        }
    }

    void Talking()
    {
        // アイコンを非表示に
        _icon.gameObject.SetActive(false);
        // 吹き出しと文字の表示
        _speechBalloon.gameObject.SetActive(true);
        _talkText.gameObject.SetActive(true);

        // 文字をすべて表示していない場合
        if (!_isOneMessage)
        {
            // 経過時間で１文字ずつ出していく
            _textSpeed -= Time.deltaTime;

            // 文字をすべて表示していない場合
            if (_textMessage[_textMessageIndex].Length > _textCount)
            {
                if (_textSpeed <= 0)
                {
                    // 一文字追加
                    _talkText.text += _textMessage[_textMessageIndex][_textCount];
                    // 現在の文字数を増やす
                    _textCount++;
                    // 時間の初期化
                    _textSpeed = _defTextSpeed;

                    // 全部の表示が終わったら
                    if (_textCount >= _textMessage[_textMessageIndex].Length)
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            _isOneMessage = true;
                        }
                    }
                }
            }
            // キー入力があった場合、文字を全部表示させる
            if (Input.GetButtonDown("Jump"))
            {
                _talkText.text += _textMessage[_textMessageIndex].Substring(_textCount);
                _isOneMessage = true;
            }
        }
        else
        {
            // 次の文章に移る
            _textMessageIndex++;
            _talkText.text = "";
            _textCount = 0;
            _textSpeed = _defTextSpeed;

            // すべての文章が終わった
            if (_textMessageIndex >= _textMessage.Length)
            {
                TalkEnd();
            }
            else
            {
                _isOneMessage = false;
            }
        }
    }
    // 初期化
    void TalkEnd()
    {
        _state = TalkState.OFF;
        _talkText.text = "";
        _textCount = 0;
        _textMessageIndex = 0;
        _icon.gameObject.SetActive(false);
        _speechBalloon.gameObject.SetActive(false);
        _talkText.gameObject.SetActive(false);
        _talkFlag = false;
        _isOneMessage = false;
        _textSpeed = _defTextSpeed;
    }
}
