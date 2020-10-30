using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashUI : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 1.0f;
    private RectTransform _rectTrans;
    private float _nowScale;
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private GameObject _target = null;

    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _rectTrans = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.color = new Color(1, 1, 1, 0);
        _nowScale = 1.0f;
    }

    private void ScreenPos()
	{
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.transform.position);
        _rectTrans.transform.position = screenPos;
	}

    private void Flash()
	{
        _nowScale = Mathf.Sin(Time.time / _speed) / 4 + 0.5f;
        _rectTrans.transform.localScale = new Vector3(_nowScale, _nowScale, 1);
	}

    private void Rotate()
	{
        _rectTrans.Rotate(new Vector3(0, 0, _rotSpeed));
	}

    // Update is called once per frame
    void Update()
    {
        if (!_debug)
        {
            Rotate();
        }
        ScreenPos();
        Flash();
    }
}
