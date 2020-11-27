using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour
{
    // 回転させる軸のタイプ
    private enum ROTATE_TYPE
    {
        X,
        Y,
        Z,
        MAX
    }

    [SerializeField, Tooltip("回転したい軸を選択")]
    private ROTATE_TYPE _type = ROTATE_TYPE.X;
    // 1回あたりに回転する角度の大きさ
    [SerializeField, Tooltip("1回転ごとの角度の大きさ")]
    private float _angle = 5.0f;
    // 回転を更新する秒間間隔
    [SerializeField, Tooltip("1回転ごとの更新時間に必要な時間")]
    private float _secondTime = 1.0f;
    private float _nowTime = 0;

    // 有効化されたときにリセットする
    private void OnEnable()
    {
        _nowTime = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 秒数計測
        _nowTime += Time.deltaTime;
        // 更新時間まで経過していないなら処理をスキップする
        if (_nowTime <= _secondTime)
        {
            return;
        }
        // リセット
        _nowTime = 0;
        // 軸のタイプごとに回転させる
        switch (_type)
        {
            case ROTATE_TYPE.X:
                transform.Rotate(_angle, 0, 0);
                break;
            case ROTATE_TYPE.Y:
                transform.Rotate(0, _angle, 0);
                break;
            case ROTATE_TYPE.Z:
                transform.Rotate(0, 0, _angle);
                break;
            case ROTATE_TYPE.MAX:
            default:
                break;
        }
    }
}
