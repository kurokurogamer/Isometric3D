﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemIcon : MonoBehaviour
{
    [SerializeField, Tooltip("アイテムの画像")]
    private SpriteRenderer _itemSprite = default;

    [SerializeField, Tooltip("アイテムの名前を表示するテキスト")]
    private TextMeshProUGUI _itemName = default;

    [SerializeField, Tooltip("個数を表示するテキスト")]
    private TextMeshProUGUI _itemNumText = default;
    public TextMeshProUGUI ItemNumText
    {
        get { return _itemNumText; }
    }

    private ItemBase _item = default;

    private ItemManager _itemManager = default;
    // アイコン用ID
    private int _itemId = 0;
    public int ItemId
    {
        get { return _itemId; }
    }

    public static ItemIcon Instantiate(ItemIcon prefab, ItemBase item, int num)
    {
        ItemIcon obj = Instantiate(prefab) as ItemIcon;
        obj._item = item;
        obj._itemSprite.sprite = item.Icon;
        obj._itemName.text = item.ItemName;
        obj._itemNumText.text = num.ToString();
        obj._itemId = item.ItemId;

        return obj;
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // 親子関係
        GameObject itemCircle = GameObject.FindGameObjectWithTag("ItemCircle");
        transform.SetParent(itemCircle.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        // アイコンのセット
        itemCircle.GetComponent<ItemCircle>().SetItemIcon(gameObject);

        _itemManager = ItemManager.Instans.GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
