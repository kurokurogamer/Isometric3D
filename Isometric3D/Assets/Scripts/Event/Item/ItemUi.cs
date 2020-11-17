using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUi :MonoBehaviour
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
	//private List<GameObject> _itemIconList = new 

    void Start()
    {
		// アイテムを表示するアングルを作成
        for(int k = 0; k< _angle.Length; k++)
        {
			_angle[k] = (360 / _polygon) * k;
		}
    }

	public void Arrange()
	{
		float splitAngle = 360 / transform.childCount;
		var rect = transform as RectTransform;

		for (int elementId = 0; elementId < transform.childCount; elementId++)
		{
			var child = transform.GetChild(elementId) as RectTransform;
			float currentAngle = splitAngle * elementId + _offsetAngle;
			child.anchoredPosition = new Vector2(
				Mathf.Cos(currentAngle * Mathf.Deg2Rad) + _centerPos.x,
				Mathf.Sin(currentAngle * Mathf.Deg2Rad) + _centerPos.y) * _radius;
		}
	}
}