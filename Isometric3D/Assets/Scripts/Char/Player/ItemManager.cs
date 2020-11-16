using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // シングルトン
    private static ItemManager _instans = null;
    public static ItemManager Instans
    {
        get { return _instans; }
    }

    // アイテム用リスト
    private Dictionary<ItemBase,int>_itemTable  = new Dictionary<ItemBase, int>();
    public Dictionary<ItemBase, int> ItemTable
    {
        get { return _itemTable; }
        set { _itemTable = value; }
    }
    private List<ItemBase> _itemlist = new List<ItemBase>();

    // 入手アイテムデータ保存用
    // キー
    private ItemBase _itemGetData = default;
    // 個数
    private int _itemGetNumData;

    [SerializeField, Tooltip("入手時に表示するテキスト")]
    private GameObject _itemText = default;
    [SerializeField, Tooltip("入手時に表示するキャンバス")]
    private Canvas _canvas = default;

    // テキスト保存用リスト
    private List<GameObject> _text = new List<GameObject>();

    [SerializeField, Tooltip("持っているアイテムを画面上に表示する")]
    // 使用するアイテムの表示
    private SpriteRenderer _useItemSprite = default;

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

    // アイテムの使用
    public void UseItem()
    {
        _useItemSprite.sprite = _itemGetData.Icon;
    }

    // アイテムの選択
    public void SelectItem()
    {
        _useItemSprite.sprite = _itemGetData.Icon;
    }
    // アイテム追加
    public void SetItemData(EventObj obj)
    {
        ItemObj item = obj.GetComponent<ItemObj>();
        // アイテムデータ
        _itemGetData = item.ItemData;
        // 個数
        _itemGetNumData = item.ItemNum;
        
        // アイテムの要素がなかったら追加
        if (!_itemTable.ContainsKey(_itemGetData))
        {
            // リストに追加
            _itemlist.Add(_itemGetData);
            // ID順で並び替え
            _itemlist.Sort((a, b) => a.ItemId - b.ItemId);
            
            _itemTable.Add(_itemGetData, _itemGetNumData);
        }
        else
        {
            // 個数だけ追加
            _itemTable[_itemGetData] += _itemGetNumData;
        } 
    }

    private void ItemListSort()
    {

    }

    // 入手時のテキストデータを表示
    public void ItemGetText()
    {
        // テキストをインスタンスする
        GameObject textObj = Instantiate(_itemText);
        // 表示するキャンバスの子要素にする
        textObj.transform.SetParent(_canvas.transform, false);
        // 表示したい文
        textObj.GetComponent<Text>().text = _itemGetData.ItemName + "  を  " + _itemGetNumData.ToString() + "個  " + "入手";
        // テキストリストに追加
        _text.Add(textObj);
    }
}
