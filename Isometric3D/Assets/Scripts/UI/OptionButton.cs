using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オプションUI表示用のボタン
public class OptionButton : ButtonAction
{
    // 有効化するカメラとUIのオブジェクト
    [SerializeField, Tooltip("Optionシーンのサブカメラをアタッチ")]
    private GuidReference _optionObject;

	public override void Action()
	{
        if(_optionObject.gameObject != null)
		{
            _optionObject.gameObject.SetActive(true);
            // 時間を停止させる
            Time.timeScale = 0;
		}
    }
}
