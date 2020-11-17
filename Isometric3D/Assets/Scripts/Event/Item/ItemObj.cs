using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObj : EventObj
{
    [SerializeField, Tooltip("取得できるアイテム")]
    private ItemBase _itemData = default;
    public ItemBase ItemData
    {
        get { return _itemData; }
    }

    [SerializeField, Tooltip("アイテムの個数")]
    private int _itemNum = default;
    public int ItemNum
    {
        get { return _itemNum; }
    }

    [SerializeField, Tooltip("アイテムを入手できる回数")]
    private int _defItemGetNum = 1;
    // アイテムの入手回数カウント用変数
    private int _itemGetNum = 0;

    private void Awake()
    {
        // tagをItemにしておく
        gameObject.tag = "Item";
        if (_icon != null)
        {
            // アイコンの位置をセット
            GameObject obj = Instantiate(_icon);
            _icon = obj;
            _icon.transform.SetParent(gameObject.transform,false);
            // アイコンを非表示に
            _icon.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // イベントの更新
    public override bool EventUpData()
    {
        // アイテムデータ追加
        ItemManager.Instans.SetItemData(this);

        // 入手テキストを表示
        ItemManager.Instans.ItemGetText();

        _itemGetNum++;

        // 入手の限界回数になったら
        if(_itemGetNum >= _defItemGetNum)
        {
            Destroy(this.gameObject);
        }

        // イベント終了
        return true;
    }
}
