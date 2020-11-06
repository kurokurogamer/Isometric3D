using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, Tooltip("取得できるアイテム")]
    private ItemBase _item = default;

    [SerializeField, Tooltip("アイテムの個数")]
    private int _itemNum = default;

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
            SetItemData(other.gameObject);

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
            _icon.gameObject.SetActive(false);
        }
    }
    private void SetItemData(GameObject player)
    {
        // アイテム情報追加
        ItemContlloer item = player.GetComponent<ItemContlloer>();
        item.SetItem(_item, _itemNum);
        //// 吹き出しテキスト追加
        //TalkController talk = player.GetComponent<TalkController>();
        //talk.
    }
}
