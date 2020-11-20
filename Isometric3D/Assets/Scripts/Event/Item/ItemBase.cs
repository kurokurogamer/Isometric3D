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

    [SerializeField, Tooltip("アイテムの情報")]
    private string _information = default;
    public string Information
    {
        get { return _information; }
    }

    [SerializeField, Tooltip("アイテムサークル使うプレハブ")]
    private ItemIcon _itemIcon = default;
    public ItemIcon ItemIcon
    {
        get { return _itemIcon; }
        set { _itemIcon = value; }
    }
}
