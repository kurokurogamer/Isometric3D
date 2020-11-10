using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    // アイテム用リスト
    private Dictionary<ItemBase,int>_itemTable  = new Dictionary<ItemBase, int>();

    // 入手アイテムデータ保存用
    // キー
    private ItemBase _itemGetData = default;
    public ItemBase ItemGetData
    {
        set { _itemGetData = value; }
    }
    // 個数
    private int _itemGetNumData;
    public int ItemGetNumData
    {
        set { _itemGetNumData = value; }
    }

    [SerializeField, Tooltip("入手時に表示するテキスト")]
    private GameObject _itemGetTextData = default;
    public int ItemGetTextData
    {
        set { _itemGetNumData = value; }
    }
    [SerializeField, Tooltip("入手時に表示するキャンバス")]
    private Canvas _canvas = default;

    // テキスト保存用リスト
    private List<GameObject> _text = new List<GameObject>();

    // アイテム取得可能フラグ(true = 可能,false = 不可)
    private bool _itemGetFlag = false;
    public bool ItemGetFlag
    {
        get { return _itemGetFlag; }
        set { _itemGetFlag = value; }
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
                _text[0].SetActive(true);
            }
        }
    }

    public void UseItem()
    {

    }

    // アイテム追加
    public void SetItemData(ItemBase item, int num)
    {
        _itemGetData = item;
        _itemGetNumData = num;
    }

    public void GetItem()
    {
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

        // テキスト情報追加
        GameObject obj = Instantiate(_itemGetTextData);
        obj.transform.SetParent(_canvas.transform, false);
        obj.GetComponent<Text>().text = _itemGetData.ItemName + "  を  " + _itemGetNumData.ToString() + "個  " + "入手";
        _text.Add(obj);
        _itemGetFlag = false;
        // 入手予定のアイテムの要素を初期化
        ResetItemData();
    }
    // 入手用するアイテムデータの初期化
    public void ResetItemData()
    {
        ItemGetData = null;
        ItemGetNumData = 0;
    }
}
