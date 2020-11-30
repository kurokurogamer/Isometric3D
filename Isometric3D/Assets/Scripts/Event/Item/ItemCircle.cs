using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemCircle : MonoBehaviour
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
	private Tweener[] _tweener = default;
	private RectTransform _rectTransform = default;
	[SerializeField, Tooltip("選択時の回転の度合")]
	private AnimationCurve _curve = default;
	// 矢印のアニメーション用
	[SerializeField, Tooltip("左矢印")]
	private GameObject _leftArrow = default;
	private RectTransform _leftArrowDefPos = default;
	private Tweener _leftArrowtweener = default;
	[SerializeField, Tooltip("右矢印")]
	private GameObject _rightArrow = default;
	private RectTransform _rightArrowDefPos = default;
	private Tweener _rightArrowtweener = default;

	// 非表示時の座標
	private Vector2 _inactivePos = default;
	private void Awake()
	{
		_tweener = new Tweener[3];
		_rectTransform = GetComponent<RectTransform>();
		_inactivePos = _rectTransform.anchoredPosition;

		_leftArrowDefPos = _leftArrow.GetComponent<RectTransform>();
		_rightArrowDefPos = _rightArrow.GetComponent<RectTransform>();

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

			// 座標の設定
			Vector2 pos = new Vector2(
				Mathf.Cos(currentAngle * Mathf.Deg2Rad) + _centerPos.x,
				Mathf.Sin(currentAngle * Mathf.Deg2Rad) + _centerPos.y) * _radius;
			// 回転アニメーション
			child.DOAnchorPos(pos, 0.2f).SetEase(_curve);

		}
		// 戻るアニメーション
		_tweener[0] = _rectTransform.DOAnchorPos(new Vector2(-400, -225), 0.5f).SetDelay(1);
		_tweener[1] = _leftArrow.GetComponent<RectTransform>().DOAnchorPos(new Vector2(20, 50), 0.5f).SetDelay(1);
		_tweener[2] = _rightArrow.GetComponent<RectTransform>().DOAnchorPos(new Vector2(50, 20), 0.5f).SetDelay(1);
	}

	// アイテム選択
	public void SelectItem(int rightFlag)
	{
		// アニメーション
		ItemChangeAnim();
		// 選択中アイテム基準の回転角
		_offsetAngle = _offsetAngle + (_angle * rightFlag);
		// 選択中のアイテム
		_useItemListNum = _useItemListNum + rightFlag;
		// 回転
		Arrange();
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
		for (int k = 0; k < _tweener.Length; k++)
		{
			if (_tweener[k] != null)
			{
				// 削除
				_tweener[k].Kill();
			}
		}
		// アニメーションの登録・開始
		_rectTransform.DOAnchorPos(new Vector2(-350, -175), 0.5f);
		_leftArrow.GetComponent<RectTransform>().DOAnchorPos(new Vector2(5, 55), 0.5f).SetEase(_curve);
		_rightArrow.GetComponent<RectTransform>().DOAnchorPos(new Vector2(55, 5), 0.5f).SetEase(_curve);

	}

	private void OnDisable()
	{
		InactiveAnim();

	}

	// アイテムサークルの非アクティブ時のアニメーション
	public IEnumerator InactiveAnim()
	{
		// アニメーションの登録・開始
		_rectTransform.DOAnchorPos(_inactivePos, 0.5f);

		yield return new WaitForSeconds(0.5f);

		gameObject.SetActive(false);
	}

}