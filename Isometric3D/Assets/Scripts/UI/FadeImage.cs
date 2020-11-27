using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : FadeUI
{
	// フェイドするイメージ
	private Image _image;
	
	protected override void Awake()
	{
		base.Awake();
		_image = GetComponent<Image>();
		if (_image)
		{
			_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _alpha);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (!_image)
		{
			_image = GetComponent<Image>();
		}
		Debug.Log("処理");
		if (_image)
		{
			_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _alpha);
		}
	}

	// Update is called once per frame
	void Update()
    {
		Fade();
		_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _alpha);
	}

}
