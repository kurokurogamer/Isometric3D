using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollider : MonoBehaviour
{
    // イベントのオブジェクト保存用List
    private List<EventObj> _eventObjList = new List<EventObj>();
    public List<EventObj> EventObjList
    {
        get { return _eventObjList; }
        set { _eventObjList = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // イベント用リスト作成
        _eventObjList = new List<EventObj>();
        // プレイヤーのアイテム、会話スクリプトを取得しておく
        GameObject player = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    // 当たり判定
    // 入った瞬間
    private void OnTriggerEnter(Collider other)
    {
        // ItemかTalkのオブジェクトがコライダーに入ったら
        if (other.tag == "Item"|| other.tag == "Talk")
        {
            EventObj eventObj = other.gameObject.GetComponent<EventObj>();
            // イベント用スクリプトのListに同じ要素が入っていなければ追加
            if (!_eventObjList.Contains(eventObj))
            {
                _eventObjList.Add(eventObj);
            }
            // 頭上のアイコンが設定されていれば表示する
            if(eventObj.Icon != null)
            {
                eventObj.Icon.SetActive(true);
            }
        }
    }
    // 出たとき
    private void OnTriggerExit(Collider other)
    {
        // ItemかTalkのオブジェクトがコライダーに入ったら
        if (other.tag == "Item" || other.tag == "Talk")
        {
            EventObj eventObj = other.gameObject.GetComponent<EventObj>();
            // Listに同じ要素が入っていれば削除
            if (_eventObjList.Contains(eventObj))
            {
                _eventObjList.Remove(eventObj);
            }
            // 頭上のアイコンが設定されていれば非表示にする
            if (eventObj.Icon != null)
            {
                eventObj.Icon.SetActive(false);
            }
        }
    }
    // イベントがあるか確認する
    public bool CheckEvent()
    {
        // 要素があればtrueになる
        if (_eventObjList == null)
        {
            return false;
        }
        else if(_eventObjList.Count == 0)
        {
            return false;
        }
        else
        {
            // イベント可能
            return true;
        }
    }
    // イベントの内容をセット
    private void SetEventData()
    {
        _eventObjList[0].EventUpData();
    }
}
