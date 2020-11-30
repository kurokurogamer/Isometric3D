using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
	private enum INPUT_TYPE
	{
		Horizontal,
		Vertical,
		MAX
	}

	[SerializeField, Tooltip("入力タイプ")]
	private INPUT_TYPE _inputType = INPUT_TYPE.Vertical;
	[SerializeField]
	private Vector3 offset = Vector3.zero;

	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	// 経過時間を図る
	private float _nowTime;
	private float _nowTimeDelay;

	protected MenuSelect _startUI = null;

	protected List<ButtonAction> _buttonList;

	[SerializeField, Tooltip("カーソル")]
	protected RectTransform _cursor = null;
	private Text _cursorText = null;

	// UI用サウンド
	[SerializeField]
	protected AudioClip _clip;
	// 入力情報
	protected float _axis;
	// 選択中のメニューID
	protected int _id;
	public int ID
	{
		get { return _id; }
	}

	// Start is called before the first frame update
	protected virtual void Start()
	{
		_nowTime = 0;
		_nowTimeDelay = 0;

		_startUI = this;
		//_clip = transform.root.GetComponent<UIAudio>();
		_buttonList = new List<ButtonAction>();
		// 子のオブジェクト取得
		foreach (RectTransform child in transform)
		{
			// ボタンを取得する
			if (child.TryGetComponent(out ButtonAction buttonAction))
			{
				_buttonList.Add(buttonAction);
			}
		}
		if(_cursor)
		{
			foreach(RectTransform child in _cursor)
			{
				if(child.TryGetComponent(out Text text))
				{
					_cursorText = text;
				}
			}
		}

		// 入力情報の初期化
		_axis = 0;
		_id = 0;
	}

	// 入力の取得
	protected void SetInput()
	{
		// 入力タイプによって
		switch (_inputType)
		{
			case INPUT_TYPE.Horizontal:
				_axis = Input.GetAxis("Horizontal");
				break;
			case INPUT_TYPE.Vertical:
				_axis = Input.GetAxis("Vertical");
				if (Input.GetKey(KeyCode.UpArrow))
				{
					_axis = 1.0f;
				}
				else if(Input.GetKey(KeyCode.DownArrow))
				{
					_axis = -1.0f;
				}

				break;
			case INPUT_TYPE.MAX:
			default:
				break;
		};
	}

	// メニューのセレクト
	protected bool Select()
	{
		// 動かすメニューがなければ処理しない
		if(_buttonList.Count < 0)
		{
			return false;
		}
		// ボタンを押しているか
		if (_axis > 0 || _axis < 0)
		{
			if (_nowTimeDelay <= 0)
			{
				_nowTime = _interval;
			}
			_nowTimeDelay += Time.unscaledDeltaTime;
		}
		else
		{
			_nowTimeDelay = 0;
			return false;
		}

		// 反応時間を超えていたら
		if (_nowTimeDelay > _delay)
		{
			_nowTime += Time.unscaledDeltaTime;
		}

		// インターバル時間を超えていたら処理を行う
		if (_nowTime >= _interval)
		{
			if (_clip)
			{
				// 操作時にSEを鳴らす
				AudioManager.instance.PlayOneSE(_clip);
			}
			//if (_cursor)
			//{
			//	if (_cursor.TryGetComponent(out TextSlider _slider))
			//	{
			//		_slider.SliderReset();
			//	}
			//}
			// メニューIDの変更
			if (_axis < 0)
			{
				_id++;
			}
			else if (_axis > 0)
			{
				_id--;
			}

			// メニューの上下リセット
			if (_id < 0)
			{
				_id = _buttonList.Count - 1;
			}
			else if (_id > _buttonList.Count - 1)
			{
				_id = 0;
			}


			// カーソルが設定されているなら使用する
			if (_cursor != null)
			{
				_cursor.position = _buttonList[_id].transform.position + offset;
			}

			// カーソルのテキストをメニューのものにする
			//if (_cursorText)
			//{
			//	_cursorText.text = _menuTextList[_id].text;
			//}

			_nowTime = 0;
			return true;
		}
		return false;
	}

	protected virtual void Check()
	{
		if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.N))
		{
			// キャンセルサウンドを鳴らす
			if (_clip)
			{
				AudioManager.instance.PlayOneSE(_clip);
			}
		}
		else if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.O))
		{
			// 決定サウンドを鳴らす
			if (_clip)
			{
				AudioManager.instance.PlayOneSE(_clip);
			}
			_buttonList[_id].Action();
			//this.enabled = false;
		}
	}


	// Update is called once per frame
	protected virtual void Update()
    {
		SetInput();
		Select();
		Check();
    }
}
