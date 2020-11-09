using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemBase : ScriptableObject
{
    [SerializeField, Tooltip("表示するアイコン")]
    private Sprite _icon = default;
    public Sprite Icon
    {
        get { return _icon;}
    }

    [SerializeField, Tooltip("アイテムのID")]
    private int _itemID = default;
    public int ItemId
    {
        get { return _itemID; }
    }

    [SerializeField, Tooltip("表示する名前")]
    private string _itemName = default;
    public string ItemName
    {
        get { return _itemName; }
    }
    [SerializeField, Tooltip("アイテムの初期所持数")]
    private int _itemNum = 0;
    public int ItemNum
    {
        get { return _itemNum; }
        set { _itemNum = value; }
    }

    [SerializeField, Tooltip("アイテムの情報")]
    private string _information = default;
    public string Information
    {
        get { return _information; }
    }
}
