using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollider : MonoBehaviour
{
    // イベントのオブジェクト保存用List
    private List<EventObj> _eventObjList = new List<EventObj>();
    private EventObj _event = default;
    public EventObj Event
    {
        get { return _event; }
        set { _event = value; }
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
        if (SortEventList())
        {
            // イベントを登録
            _event = _eventObjList[0];
            // イベント可能
            return true;
        }
        return false;
    }

    // ソートと不要なデータがあれば削除
    private bool SortEventList()
    {
        // 要素があればtrueになる
        if (_eventObjList == null)
        {
            return false;
        }

        // 不要な要素削除
        // リストをコピー
        List<EventObj> list = new List<EventObj>(_eventObjList);
        foreach (EventObj obj in list)
        {
            // nullは削除
            if(obj == null)
            {
                _eventObjList.Remove(obj);
            }
        }
        // 要素がなくなったらfalse
        if (_eventObjList.Count == 0)
        {
            return false;
        }
        
        return true;
    }

    public void SetEventObjIcon()
    {
        foreach (EventObj obj in _eventObjList)
        {
            // 頭上のアイコンが設定されていれば表示にする
            if (obj.Icon != null)
            {
                obj.Icon.SetActive(true);
            }
        }
    }
}
