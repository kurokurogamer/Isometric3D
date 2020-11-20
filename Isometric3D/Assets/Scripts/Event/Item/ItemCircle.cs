using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemCircle :MonoBehaviour
{
	[SerializeField, Tooltip("リングコマンドの半径")]
	private float _radius = 100;
	[SerializeField, Tooltip("リングコマンド選択される位置")]
	private float _offsetAngle = 45;

	[SerializeField, Tooltip("リングコマンドの中心座標")]
	private Vector2 _centerPos = default;

	[SerializeField, Tooltip("何角形で表現するか")]
	private int _polygon = 8;

	[SerializeField, Tooltip("アイテムアイコンの表示数 = 角度")]
	private int[] _angle = default;

	// ItemIconを入れるList
	private List<GameObject> _itemIconList = new List<GameObject>();
	public List<GameObject> ItemIconList
	{
		get { return _itemIconList; }
        set { _itemIconList = value; }
	}

	protected void OnValidate()
	{
		Arrange();

	}
	void Start()
    {
		// アイテムを表示するアングルを作成
        for(int k = 0; k< _angle.Length; k++)
        {
			_angle[k] = (-360 / _polygon) * k;
		}
    }

	public void Arrange()
	{
		float splitAngle = -360 / _polygon;
		//var rect = transform as RectTransform;

		// 子の要素ぶん回す
		for (int elementId = 0; elementId < _itemIconList.Count; elementId++)
		{
			var child = _itemIconList[elementId].transform as RectTransform;
			float currentAngle = splitAngle * elementId + _offsetAngle;
			child.anchoredPosition = new Vector2(
				Mathf.Cos(currentAngle * Mathf.Deg2Rad) + _centerPos.x,
				Mathf.Sin(currentAngle * Mathf.Deg2Rad) + _centerPos.y) * _radius;
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
		foreach(GameObject item in _itemIconList)
        {
			// リストに入っているIDを見る
			int id = item.GetComponent<ItemIcon>().ItemId;
			if (itemId < id)
            {
				_itemIconList.Insert(listId, obj);
				Arrange();
				return;
			}
			// 一つ先に進む
			listId++;
		}

		_itemIconList.Add(obj);
		Arrange();
	}

	// 
	public IEnumerator SelectItem(int rightFlag)
    {


		// 逆時計回り
		if (rightFlag == -1)
        {

			_offsetAngle -= 45;
			Arrange();

		}
		else
        {
			_offsetAngle += 45;
			Arrange();
		}

		yield return null;
	}
}