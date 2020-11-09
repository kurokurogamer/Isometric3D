using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemContlloer : MonoBehaviour
{
    // アイテム用リスト
    private Dictionary<ItemBase,int>_itemTable  = new Dictionary<ItemBase, int>();

    // アイテム取得可能フラグ(true = 可能,false = 不可)
    private bool _itemFlag = false;
    public bool ItemFlag
    {
        get { return _itemFlag; }
        set { _itemFlag = value; }
    }

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
    public void SetItem(ItemBase item, int num)
    {
        // アイテムの要素がなかったら追加
        if (!_itemTable.ContainsKey(item))
        {
            _itemTable.Add(item, num);
        }
        else
        {
            // 個数だけ追加
            _itemTable[item] += num;
        }
    }
}
