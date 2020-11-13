using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyScriptable/Create EnemyDate")]
public class EnemyData : ScriptableObject
{
	// エネミーの名前
	[SerializeField]
	private string _enemyName = "";
	// 歩く速さ
	[SerializeField]
	private float _speed = 1.0f;
	// 巡回時間
	[SerializeField]
	private float _secondTime = 2.0f;
	// 待機時間
	[SerializeField]
	private float _waitTime = 3.0f;
	// サーチ可能角度
	[SerializeField]
	private float _searchAngle = 60f;
	// 歩く音
	[SerializeField]
	private AudioClip _clip = null;
	// 発見時のアイコン
	[SerializeField]
	private GameObject _icon = null;
	
	public string enemyName
	{
		get { return _enemyName; }
	}
	
	public float speed
	{
		get { return _speed; }
	}

	public float secondTime
	{
		get { return _secondTime; }
	}

	public float waitTime
	{
		get { return _waitTime; }
	}

	public float searchAngle
	{
		get { return _searchAngle; }
	}

	public AudioClip clip
	{
		get { return _clip; }
	}

	public GameObject icon
	{
		get { return _icon; }
	}
}
