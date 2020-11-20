using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 1.0f;
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private bool _debug = false;
    private Vector3 _point;
    private float _nowScale;

    public Vector3 point
	{
		get { return _point; }
		set { _point = value; }
	}

    // Start is called before the first frame update
    void Start()
    {
        _point = Vector3.zero;
        _nowScale = 1.0f;
    }

    private void ScreenPos()
	{
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, _point);
        transform.position = screenPos;
	}

    private void Scale()
	{
        _nowScale = Mathf.Sin(Time.time / _speed) / 4 + 1.0f;
        transform.localScale = new Vector3(_nowScale, _nowScale, 1);
	}

    private void Rotate()
	{
        transform.Rotate(new Vector3(0, 0, _rotSpeed));
	}

    // Update is called once per frame
    void Update()
    {
        if (!_debug)
        {
            Rotate();
        }
        //ScreenPos();
        Scale();
    }
}
