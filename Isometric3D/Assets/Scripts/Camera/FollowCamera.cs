using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤー")]
    private GameObject _target = null;
    [SerializeField, Tooltip("距離")]
    private float _distans = 10.0f;
    [SerializeField, Tooltip("カメラサイズ")]
    private float _cameraSize = 10.0f;
    // カメラ回転角度の変更速度
    [SerializeField]
    private float _rotSpeed = 1.0f;
    // カメラ回転角度の変更速度
    [SerializeField]
    private float _zoomSpeed = 5.0f;

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

    // ターゲットオブジェクト追従処理
    private void Follow()
	{
		transform.position = _target.transform.position - transform.forward * _distans;
	}

    // カメラズーム処理
    private void ChangeSize()
	{
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraSize, Time.deltaTime * _zoomSpeed);
    }

    // カメラ回転処理
    private void ChangeRotate()
	{
        Vector3 rot = transform.eulerAngles;
        // キー入力でカメラ角度を変更(デバック用)
        if(Input.GetKey(KeyCode.Q))
		{
            rot = Vector3.Lerp(rot, new Vector3(90, 0, 0), _rotSpeed * Time.deltaTime);

        }
        else
		{
            rot = Vector3.Lerp(rot, new Vector3(30, 45, 0), _rotSpeed * Time.deltaTime);
		}
        // 最終的に決まった回転角度に向かって回転する
        transform.rotation = Quaternion.Euler(rot);
	}

    // Update is called once per frame
    void Update()
    {
        ChangeSize();
    }

	private void LateUpdate()
	{
        // カメラの移動処理系はUpdate後に行う
        ChangeRotate();
        Follow();
    }
}
