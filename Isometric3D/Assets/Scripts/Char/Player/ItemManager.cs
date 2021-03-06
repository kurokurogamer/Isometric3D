﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

// アイテムリストの整列規則クラス
public sealed class SortRule : IComparer<ItemBase> // <>の中身はKeyの型を指定する
{
    // ID昇順に並べ替え
    public int Compare(ItemBase x, ItemBase y)
    {
        return x.ItemId - y.ItemId;
    }
}

public class ItemManager : MonoBehaviour
{
    // シングルトン
    private static ItemManager _instans = null;
    public static ItemManager Instans
    {
        get { return _instans; }
    }

    // アイテム用リスト
    private SortedDictionary<ItemBase, int>_itemTable  = new SortedDictionary<ItemBase, int>(new SortRule());
    public SortedDictionary<ItemBase, int> ItemTable
    {
        get { return _itemTable; }
        set { _itemTable = value; }
    }

    // 入手アイテムデータ保存用
    // キー
    private ItemBase _itemGetData = default;
    // 個数
    private int _itemGetNumData;

    // UI
    [SerializeField, Tooltip("入手時に表示するテキスト")]
    private GameObject _itemText = default;
    [SerializeField, Tooltip("入手時に表示するキャンバス")]
    private Canvas _canvas = default;

    // テキスト保存用リスト
    private List<GameObject> _text = new List<GameObject>();

    // 使用するアイテムの番号
    private int _useItemNum = 0;

    [SerializeField, Tooltip("アイテムアイコンを表示するサークル")]
    private ItemCircle _itemCircle = default;

    [SerializeField, Tooltip("アイテムアイコン用プレハブ")]
    private ItemIcon _itemIcon = default;

   

    private void Awake()
    {
        if (_instans == null)
        {
            _instans = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // テキストアニメーションの更新
        ItemGetTextupData();
    }

    // アイテムの使用
    public void UseItem()
    {
        // return条件
        // アイテム未取得の場合
        if (_itemTable.Count == 0)
        {
            return;
        }
        // アイテム数が0の場合
        else if(_itemTable.ElementAt(_useItemNum).Value != 0)
        {
            return;
        }
       

    }

    // アイテムの選択
    public void SelectItem()
    {
        int num = _itemCircle.UseItemListNum + (int)Input.GetAxis("ItemSelectKey");
        Input.ResetInputAxes();
        // 使用されるアイテムの番号がリストの要素数以上にならないようにする
        if (num < 0 || _itemTable.Count <= num)
        {
            return;
        }
        // 表示スプライトの更新
        _itemCircle.SelectItem(num - _itemCircle.UseItemListNum);
 
    }

    // アイテム追加
    public void SetItemData(EventObj obj)
    {
        ItemObj item = obj.GetComponent<ItemObj>();
        // アイテムデータ
        _itemGetData = item.ItemData;
        // 個数
        _itemGetNumData = item.ItemNum;
        
        if (_itemCircle != null)
        {
            if (_itemCircle.gameObject.activeSelf == false)
            {
                _itemCircle.gameObject.SetActive(true);
            }
        }

        // アイテムの要素がなかったら追加
        if (!_itemTable.ContainsKey(_itemGetData))
        {      
            // アイテムリストに追加
            _itemTable.Add(_itemGetData, _itemGetNumData);
            // アイテムサークルにアイコンを追加
            _itemGetData.ItemIcon = ItemIcon.Instantiate(_itemIcon, _itemGetData, _itemGetNumData);  
        }
        else
        {
            // 個数だけ追加
            _itemTable[_itemGetData] += _itemGetNumData;
            // 所持数テキストの更新
            _itemGetData.ItemIcon.ItemNumText.text = _itemTable[_itemGetData].ToString();
        } 
    }

    // 入手時のテキストデータを表示
    public void ItemGetText()
    {
        // テキストをインスタンスする
        GameObject textObj = Instantiate(_itemText);
        // 表示するキャンバスの子要素にする
        textObj.transform.SetParent(_canvas.transform, false);
        // 表示したい文
        textObj.GetComponent<TextMeshProUGUI>().text = _itemGetData.ItemName + "  を  " + _itemGetNumData.ToString() + "個  " + "入手";
        // テキストリストに追加
        _text.Add(textObj);
    }

    // 入手時に出るテキストのアニメーション
    private void ItemGetTextupData()
    {
        // アイテム入手テキストのポップアップ
        // リストに要素があった場合
        if (_text.Count != 0)
        {
            // テキストのアニメーションが終わると自動的に破棄→nullになる
            if (_text[0] == null)
            {
                // 先頭の要素削除
                _text.RemoveAt(0);
            }
            else
            {
                // 表示する
                _text[0].SetActive(true);
            }
        }
    }
}
