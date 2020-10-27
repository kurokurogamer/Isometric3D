using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤー")]
    private GameObject _player;
    [SerializeField, Tooltip("距離")]
    private float _distans = 0;
    [SerializeField, Tooltip("カメラサイズ")]
    private float _cameraSize;
    [SerializeField]
    private float _speed = 1.0f;
    public float CameraSize
	{
		set { _cameraSize = value; }
		get { return _cameraSize; }
	}

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
    }

    private void Follow()
	{
        transform.position = _player.transform.position - transform.forward * _distans;
	}

    private void ChangeSize()
	{
        _camera.orthographicSize = _cameraSize;
    }

    private void ChangeRotate()
	{
        Vector3 rot = transform.eulerAngles;
        if(Input.GetKey(KeyCode.Q))
		{
            rot = Vector3.Lerp(rot, new Vector3(90, 0, 0), _speed * Time.deltaTime);

        }
        else
		{
            rot = Vector3.Lerp(rot, new Vector3(30, 45, 0), _speed * Time.deltaTime);
		}
        transform.rotation = Quaternion.Euler(rot);
	}

    // Update is called once per frame
    void Update()
    {
        ChangeSize();
    }

	private void LateUpdate()
	{
        ChangeRotate();
        Follow();
    }
}
