using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField, Tooltip("キャラクターのスピード")]
    private float _moveSpeed = 4f;

    // isometric用の移動軸の設定
    private Vector3 _forward, _right;

    private Animator _animator = null;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの向きに対してのキャラクターの動きの設定
        // 正面
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        // 右はカメラに対して90度回転させた方向
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 移動
    public void Move()
    {
        // 何かしらのキーが入力されたら
        if (Input.GetAxis("HorizontalKey") != 0 || Input.GetAxis("VerticalKey") != 0)
        {
            Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
            // 左右の移動量
            Vector3 rightMovement = _right * _moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
            // 上下の移動量
            Vector3 upMovement = _forward * _moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
            Vector3 pos = rightMovement + upMovement;

            // 向きの設定
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
            transform.forward = heading;

            // 移動
            transform.position += pos;
            if (_animator != null)
            {
                _animator.SetBool("Walk", true);
            }
        }
        else if (_animator != null)
        {
            _animator.SetBool("Walk", false);
        }
    }
}
