using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
	public enum FollowMode
	{
		NON,
		MOVE,
		WARP,
	}

	// 追従モード
	[SerializeField, Tooltip("チェック用にインスペクターで出してます")]
	private FollowMode _mode;
	// 追従オブジェクト
	private GameObject _target;
	// 初期座標
	private Vector3 _localPos;
	[SerializeField, Tooltip("オブジェクトの移動速度")]
	private float _speed = 3.0f;
	[Header("Debug")]
	[SerializeField, Tooltip("デバックの有無")]
	private bool _debug = false;
	[SerializeField, Tooltip("確認したい場所のオブジェクトを入れる")]
	private GameObject _debugObj = null;

	// Start is called before the first frame update
	void Start()
	{
		_mode = FollowMode.NON;
		_target = null;
		_localPos = transform.localPosition;
	}

	public void ChangeMode(FollowMode mode, GameObject target)
	{
		if (_target == null)
		{
			_mode = FollowMode.NON;
			return;
		}
		_mode = mode;
	}

	private void MoveMode()
	{
		// デバック機能使用中はそちらを優先する
		if (_debug && _debugObj != null)
		{
			// モードを強制的に変更する
			if (_mode == FollowMode.NON)
			{
				_mode = FollowMode.MOVE;
			}
			switch (_mode)
			{
				case FollowMode.MOVE:
					transform.position = Vector3.Lerp(transform.position, _debugObj.transform.position, Time.deltaTime * _speed);
					break;
				case FollowMode.WARP:
					transform.position = _debugObj.transform.position;
					break;
				case FollowMode.NON:
				default:
					break;
			}
			return;
		}
		// ターゲットがなければ強制的にモードを戻す
		if(_target == null)
		{
			_mode = FollowMode.NON;
		}
		// モードに合わせて移動を開始する
		switch (_mode)
		{
			case FollowMode.NON:
				// ターゲットの参照リセット
				_target = null;
				transform.localPosition = Vector3.Lerp(transform.localPosition, _localPos, Time.deltaTime * _speed);
				break;
			case FollowMode.MOVE:
				transform.position = Vector3.Lerp(transform.position, _target.transform.position, Time.deltaTime * _speed);
				break;
			case FollowMode.WARP:
				transform.position = _target.transform.position;
				break;
			default:
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		MoveMode();
	}
}