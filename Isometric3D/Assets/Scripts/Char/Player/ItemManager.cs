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
        // リストに要素があった場合
        if (_text.Count != 0)
        {
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
    public void UseItem()
    {

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
            _itemTable.Add(_itemGetData, _itemGetNumData);
        }
        else
        {
            // 個数だけ追加
            _itemTable[_itemGetData] += _itemGetNumData;
        } 
    }

    // 入手時のテキストデータを表示
    public void ItemGetText(EventObj obj)
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
