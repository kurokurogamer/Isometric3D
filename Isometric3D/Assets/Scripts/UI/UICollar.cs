using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollar : MonoBehaviour
{
	[SerializeField, Tooltip("Collar")]
	private Color _color = Color.white;
	[SerializeField, Tooltip("イメージリスト")]
	private List<Image> _imageList = new List<Image>();
	[SerializeField, Tooltip("イメージリスト")]
	private List<Text> _textList = new List<Text>();

	private void Awake()
	{
		ChangeColor();
	}

	// Start is called before the first frame update
	void Start()
    {
    }

	private void ChangeColor()
	{
		foreach(Image image in _imageList)
		{
			image.color = _color;
		}
		foreach(Text text in _textList)
		{
			text.color = _color;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
