using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemCircle :MonoBehaviour
{
	[SerializeField, Tooltip("リングコマンドの半径")]
	private float _radius = 30;
	[SerializeField, Tooltip("リングコマンド選択される位置")]
	private float _offsetAngle = 45;
	[SerializeField, Tooltip("リングコマンドの中心座標")]
	private Vector2 _centerPos = default;

	[SerializeField, Tooltip("何角形で表現するか")]
	private int _polygon = 8;

	// アイテムアイコンの各間隔の角度
	private int _angle = default;

	// 使用するアイテムのID
	private int _useItemListNum = 0;
	public int UseItemListNum
    {
        get { return _useItemListNum; }
    }

	// ItemIconを入れるList
	private List<GameObject> _itemIconList = new List<GameObject>();
	public List<GameObject> ItemIconList
	{
		get { return _itemIconList; }
        set { _itemIconList = value; }
	}

	// アニメーション用
	private Tweener _tweener = default;
	private RectTransform _rectTransform = default;

	// 非表示時の座標
	private Vector2 _inactivePos = default;
	private void Awake()
    {
		_rectTransform = GetComponent<RectTransform>();
		_inactivePos = _rectTransform.anchoredPosition;
	}

    void Start()
    {
		_angle = 360 / _polygon;
	}

    private void OnEnable()
    {
		// アニメーションの登録・開始
		_rectTransform.DOAnchorPos(new Vector2(-400, -225), 0.5f);
	}

    // 位置の設定
    private void Arrange()
	{
		// 子の要素の数だけ回す
		for (int elementId = 0; elementId < _itemIconList.Count; elementId++)
		{
			var child = _itemIconList[elementId].transform as RectTransform;
			// 角度
			float currentAngle = (-_angle) * elementId + _offsetAngle;

			// 現在選択されているアイテムIDを登録
			if (elementId == _useItemListNum)
			{
				// アイテムの名前表示
				_itemIconList[elementId].transform.GetChild(1).gameObject.SetActive(true);
				// 画像を前に持ってくる
				_itemIconList[elementId].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;
			}
			else
			{
				// アイテムの名前非表示
				_itemIconList[elementId].transform.GetChild(1).gameObject.SetActive(false);
				// 画像を後ろに下げる
				_itemIconList[elementId].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
			}
			// 位置の設定
			child.anchoredPosition = new Vector2(
			Mathf.Cos(currentAngle * Mathf.Deg2Rad) + _centerPos.x,
			Mathf.Sin(currentAngle * Mathf.Deg2Rad) + _centerPos.y) * _radius;
		}
		// 戻るアニメーション
		_tweener = _rectTransform.DOAnchorPos(new Vector2(-400, -225), 0.5f).SetDelay(1);
	}

	// アイテム選択
	public void SelectItem(int rightFlag)
    {
		// 逆時計回り
		if (rightFlag == -1)
        {
			// アニメーション
			ItemChangeAnim();
			// 選択中アイテム基準の回転角
			_offsetAngle -= _angle;
			// 選択中のアイテム
			_useItemListNum--;
			// 回転
			Arrange();
		}
		else
        {
			// アニメーション
			ItemChangeAnim();
			// 選択中アイテム基準の回転角
			_offsetAngle += _angle;
			// 選択中のアイテム
			_useItemListNum++;
			// 回転
			Arrange();
		}
	}

	// アイテムアイコンをListに追加
	public void SetItemIcon(GameObject obj)
	{
		// ID順で並べる
		// 追加するアイテムのIDを取得
		int itemId = obj.GetComponent<ItemIcon>().ItemId;
		// リストの要素追加用
		int listId = 0;

		// リストの途中に要素を追加する
		foreach (GameObject item in _itemIconList)
		{
			// リストに入っているIDを見る
			int id = item.GetComponent<ItemIcon>().ItemId;
			if (itemId < id)
			{		
				_itemIconList.Insert(listId, obj);

				// 選択されているアイテムよりも追加アイテムのIDが前だった場合
				if (itemId < _itemIconList[_useItemListNum].GetComponent<ItemIcon>().ItemId)
				{
					_useItemListNum++;
					// 角度を補正する
					_offsetAngle += _angle;		
				}
				// 位置の設定
				Arrange();
				return;
			}
			// 一つ先に進む
			listId++;
		}

		// リストの末尾に追加
		_itemIconList.Add(obj);
		// 位置の設定
		Arrange();
	}
	// サークルが右下から出てくるアニメーション
	private void ItemChangeAnim()
    {
		// 戻るアニメーションが登録されていたら
		if (_tweener != null)
		{
			// 削除
			_tweener.Kill();
		}
		// アニメーションの登録・開始
		_rectTransform.DOAnchorPos(new Vector2(-350, -175), 0.5f);
	}

    private void Update()
    {
		if(Input.GetKeyDown(KeyCode.M))
        {
			gameObject.SetActive(false);
        }



            

	}

    private void OnDisable()
    {
		InactiveAnim();

	}

    // アイテムサークルの非アクティブ時のアニメーション
    public void InactiveAnim()
    {
		// アニメーションの登録・開始
		_rectTransform.DOAnchorPos(_inactivePos, 0.5f);
	}

}