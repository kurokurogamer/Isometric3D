using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemContlloer : MonoBehaviour
{
    // アイテム用リスト
    private List<ItemBase>  _itemList = new List<ItemBase>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UseItem()
    {

    }

    // アイテム追加
    public void SetItem(ItemBase list, int num)
    {
        // アイテムのIDを取得
        int id = list.ItemId;
        
        // ListにIDがなければ要素追加
        if (!_itemList.Find(x => x.ItemId == id))
        {
            _itemList.Add(list);
        }

        // アイテムの個数を増やす
        _itemList[id].ItemNum += num;
    }
}
