using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : MenuSelect
{
	[SerializeField, Tooltip("テキストエリア"), TextArea(2, 5)]
	private string[] _exText = new string[0];
	[SerializeField, Tooltip("メニュー項目のテキスト")]
	private Text _m = null;

	[SerializeField, Tooltip("有効にするUIのリスト")]
	private List<GameObject> _uiList = null;

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
	}

	private void SetUI()
	{
		if (_m)
		{
			_m.text = _exText[_id];
		}
	}


	protected override void Check()
	{
		// キャンセルボタンを押したときの処理
		if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.N))
		{
			if (_clip)
			{
				AudioManager.instance.PlayOneSE(_clip);
			}
			_startUI.gameObject.SetActive(false);
		}
		// 決定ボタンを押したときの処理
		else if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.O))
		{
			if (_clip)
			{
				AudioManager.instance.PlayOneSE(_clip);
			}
			if (_id < _uiList.Count)
			{
				_startUI.gameObject.SetActive(false);
				_uiList[_id].SetActive(true);
			}
			_buttonList[_id].Action();
		}
	}


	// Update is called once per frame
	protected override void Update()
    {
		base.Update();
    }
}
