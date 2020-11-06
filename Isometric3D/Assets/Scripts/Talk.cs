using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    [SerializeField, MultilineAttribute(3), Tooltip("会話文")]
    private List<string> _textMessageList = default;

    [SerializeField, Tooltip("表示スピード")]
    private float _defTextSpeed = 0.05f;

    [SerializeField, Tooltip("表示する吹き出し画像")]
    private Image _speechBalloon = default;

    [SerializeField, Tooltip("表示するテキストui")]
    private Text _talkText = default;

    [SerializeField, Tooltip("頭上のアイコン")]
    private GameObject _icon = default;


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
        // アイコンが表示されているとき
        if (_icon.activeSelf)
        {
            _icon.gameObject.transform.rotation = Camera.main.transform.rotation;
        }
    }

    // 当たり判定
    // 入った瞬間
    private void OnTriggerEnter(Collider other)
    {
        // キャラクターがコライダーに入ったら
        if (other.tag == "Player")
        {
            SetTalkData(other.gameObject);
           
            // アイコンの表示
            _icon.gameObject.SetActive(true);
        }
    }
    // 出たとき
    private void OnTriggerExit(Collider other)
    {
        // キャラクターがコライダーから出たら
        if (other.tag == "Player")
        {
            TalkController talk = other.gameObject.GetComponent<TalkController>();
            // 会話の初期化
            talk.TalkEnd();
            talk.TalkFlag = false;
            _icon.gameObject.SetActive(false);
        }
    }
    // TalkControllerにデータを送る
    private void SetTalkData(GameObject player)
    {
        TalkController talk = player.GetComponent<TalkController>();

        // 吹き出し
        talk.SpeechBalloon = _speechBalloon;
        // 会話UIText
        talk.TalkText = _talkText;
        // 頭上のアイコン
        talk.Icon = _icon;
        // 表示する会話文
        talk.TextMessageList = _textMessageList;
        // 文字の表示スピード
        talk.DefTextSpeed = _defTextSpeed;
        // 会話可能フラグ
        talk.TalkFlag = true;

    }
}
