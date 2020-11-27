using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
#pragma warning disable 0649
	[System.Serializable]
	protected struct MinMax
	{
		public float min;
		public float max;
	}

	public enum FADE_MODE
	{
		IN,		// フェイドイン
		OUT,	// フェイドアウト
		NON
	}

	[SerializeField, Tooltip("アクティブ時に自動実行するかのフラグ")]
	private bool _onAwake = true;
	// 有効化のフラグ
	private bool _active = true;
	public bool Active
	{
		set { _active = value; }
	}
	[SerializeField]
	private float _fadeSpeed = 1.0f;
	private float _nowTime;
	// フェイド状態
	[SerializeField, Tooltip("フェイド状態:IN=不透明に,OUT:透過に")]
	private FADE_MODE _mode = FADE_MODE.IN;
	public FADE_MODE Mode
	{
		get { return _mode; }
		set { _mode = value; }
	}
	[SerializeField, Tooltip("ループフラグ")]
	private bool _loop = true;

	[SerializeField, Tooltip("透過値の最小値と最大値")]
	protected MinMax _gage = new MinMax();

	// 現在の透明度
	protected float _alpha;

	public float Alpha
	{
		get { return _alpha; }
	}

	[SerializeField]
	private AudioClip _clip;

	protected virtual void Awake()
	{
		if (_onAwake)
		{
			_active = true;
			_nowTime = 1;
			//gameObject.SetActive(false);
		}
		else
		{
			_active = false;
		}
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.min;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.max;
				break;
			case FADE_MODE.NON:
			default:
				break;
		}
	}

	protected virtual void OnDisable()
	{
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.min;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.max;
				break;
			case FADE_MODE.NON:
			default:
				break;
		}
	}

	private void FadeIn()
	{
		_alpha += _fadeSpeed * Time.deltaTime;
		if(_alpha > _gage.max)
		{
			_alpha = _gage.max;
			if (_loop)
			{
				_mode = FADE_MODE.OUT;
			}
		}
	}

	private void FadeOut()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;
		if (_alpha < _gage.min)
		{
			_alpha = _gage.min;
			if (_loop)
			{
				_mode = FADE_MODE.IN;
			}
		}
	}

	private void Flash()
	{
		_nowTime += Time.deltaTime;
		if(_nowTime < _fadeSpeed)
		{
			return;
		}
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.max;
				_mode = FADE_MODE.OUT;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.min;
				_mode = FADE_MODE.IN;
				break;
			case FADE_MODE.NON:
				break;
			default:
				break;
		}
		_nowTime = 0;
	}

	protected void Fade()
	{
		if(!_active)
		{
			return;
		}
		switch (_mode)
		{
			case FADE_MODE.IN:
				FadeIn();
				break;
			case FADE_MODE.OUT:
				FadeOut();
				break;
			case FADE_MODE.NON:
			default:
				break;
		}
	}

	public void FadeSkip()
	{
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.max;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.min;
				break;
			case FADE_MODE.NON:
				break;
			default:
				break;
		}
	}

	private void Update()
	{
		if(_active)
		{
			Fade();
		}
	}
}
