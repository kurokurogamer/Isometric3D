using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : FadeUI
{
	// フェイドするテキスト
	private Text _text;
	// Start is called before the first frame update
	protected override void Awake()
	{
		base.Awake();
		_text = GetComponent<Text>();
		if (_text)
		{
			_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _alpha);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (!_text)
		{
			_text = GetComponent<Text>();
		}
		if (_text)
		{
			_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _alpha);
		}
	}

	// Update is called once per frame
	void Update()
    {
		Fade();
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _alpha);
	}
}
