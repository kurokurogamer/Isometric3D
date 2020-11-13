using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class EnemySearch : MonoBehaviour
{
    // ナビゲートタイプ
    private enum NAV_TYPE
	{
        PATROL,
        FOLLEW,
        STOP,
        MAX
	}

    [SerializeField, Tooltip("各エネミーのステータスデータ")]
    private EnemyData _enemyDate = null;
    [SerializeField, Tooltip("サーチ範囲視認用カラー")]
    private Color _color = new Color(1, 1, 0, 0.25f);
    [SerializeField, Tooltip("サーチ範囲視認用カラー")]
    private Color _color2 = new Color(1, 0, 0, 0.25f);
    [SerializeField, Tooltip("壁判定用のレイヤーマスク")]
    private LayerMask _layerMask = 0;
    [SerializeField, Tooltip("巡回ポイント")]
    private List<GameObject> _pointList = new List<GameObject>();
    private int _listCount;
    // オブジェクトを有効化した瞬間にアニメーションを再生するスクリプト
    private AutoAnimObj _autoAnimObj;
    // アイコンオブジェクト
    private GameObject _icon = null;
    // 現在の経過時間
    private float _nowSecondTime;
    // 継続サーチ時間
    private float _nowSearchTime;
    // 追従対象保存変数
    private GameObject _target;
    private NavMeshAgent _agent;
    // 円弧表示に使用するradius取得用
    private SphereCollider _sphere;
    // 現在のナビゲートタイプ保存変数
    private NAV_TYPE _navType = NAV_TYPE.PATROL;

    // Start is called before the first frame update
    void Start()
    {
        _icon = Instantiate(_enemyDate.icon, _enemyDate.icon.transform.position, _enemyDate.icon.transform.rotation);
		if (_icon.TryGetComponent(out AutoAnimObj autoAnim))
		{
            _autoAnimObj = autoAnim;
		}

		_icon.transform.SetParent(transform);
        _icon.transform.localPosition = Vector3.zero;
        _target = null;
        _listCount = 0;
        _nowSecondTime = 0.0f;
        _nowSearchTime = 0.0f;
        _agent = GetComponent<NavMeshAgent>();
        _sphere = GetComponent<SphereCollider>();
        // 初期化時に目標地点を設定しておく
        _agent.SetDestination(_pointList[_listCount].transform.position);
    }

    private void FirstContact()
	{

	}

    // 追従処理
    private void Follow()
    {
		if (_nowSearchTime >= 3.0f)
		{
            Patrol();
            _icon.SetActive(false);
        }

        // ターゲットが存在してないならスキップする
        if (_target == null)
        {
            _nowSearchTime += Time.deltaTime;
            return;
        }

        // LineCastでターゲットとの間に壁があるか判定をとる
        if (Physics.Linecast(transform.position, _target.transform.position, out RaycastHit hit, _layerMask, QueryTriggerInteraction.Ignore))
        {
            // ターゲットから自分の座標を引いてターゲットとの方向ベクトルを得る
            var playerDirection = _target.transform.position - transform.position;
            // 自身の正面から見た方向とターゲットとのベクトル方向から角度を得る
            var angle = Vector3.Angle(transform.forward, playerDirection);

            // ヒットしたのがプレイヤータグ&&得られた角度が視認可能角度よりも小さければ処理
            if (hit.transform.tag == "Player" && angle <= _enemyDate.searchAngle)
            {
                Debug.Log("視角内に" + _target.name + "がいます。");

                _nowSearchTime = 0;
                Debug.Log("壁が間にない");
                // ターゲットを目標地点に設定する
                _agent.SetDestination(_target.transform.position);
                // 索敵されたので初回発見時のアニメーションをセットする
                _autoAnimObj.stateName = "Fade";
                // エフェクトオブジェクトを有効化
                _icon.SetActive(true);
                // 音声の再生
                AudioManager.instans.PlayOneSE(_enemyDate.clip);
            }
            else
			{
                // 違う場合は壁か角度内にいないのでサーチ再計算を始める
                _nowSearchTime += Time.deltaTime;
            }
        }
    }

    // 巡回処理
    private void Patrol()
    {
        // 待機時間の更新
        _nowSecondTime += Time.deltaTime;
        // 待機指定時間まで経過していたら
        if (_nowSecondTime >= _enemyDate.secondTime)
        {
            _agent.SetDestination(_pointList[_listCount].transform.position);
            if (_listCount >= _pointList.Count)
            {
                _listCount = 0;
            }
        }

        // ナビゲート対象に近づいた時点で更新する
        if (Vector3.Distance(transform.position, _pointList[_listCount].transform.position) <= 0.1f)
        {
            _listCount++;
            _nowSecondTime = 0f;
            if (_listCount >= _pointList.Count)
            {
                _listCount = 0;
            }
        }

		//	Quaternion targetRot = Quaternion.LookRotation(_pointList[_listCount].transform.position - transform.position);
		//	// プレイヤーの回転
		//	transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _enemyDate.speed);
	}

    // Update is called once per frame
    void Update()
    {
        Follow();
        _icon.transform.rotation = Quaternion.Euler(30, 45, 0);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);

            Debug.Log("範囲外に移動");

            // アニメーションセット
            _autoAnimObj.stateName = "Fade";
			_icon.SetActive(true);

            _target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
            _target = null;
            Debug.Log("範囲外に移動");
		}
	}

    // デバック表示:ビルド時は必要のないメソッドなので処理をスキップするようにする
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
    {
        // OnDrawGizmosメソッドはEdit編集時も実行されているのでエラー対策にGetComponetしておく
        if (_sphere == null)
        {
            _sphere = GetComponent<SphereCollider>();
        }
        // 円弧描画処理
        Handles.color = _color;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_enemyDate.searchAngle, 0f) * transform.forward, _enemyDate.searchAngle * 2f, _sphere.radius);
        //Handles.color = _color2;
        //Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_enemyDate.searchAngle, 0f) * transform.forward, _enemyDate.searchAngle * 2f, _sphere.radius * 0.7f);

        // 経路の表示
        if (_agent)
        {
            Gizmos.color = Color.red;
            var prefPos = transform.position;

            foreach (var pos in _agent.path.corners)
            {
                Gizmos.DrawLine(prefPos, pos);
                prefPos = pos;
            }
        }

        // 自分とターゲットとの間に描画するRayの視覚化
        if (_target != null)
        {
            if (Physics.Linecast(transform.position, _target.transform.position, out RaycastHit hit, _layerMask, QueryTriggerInteraction.Ignore))
            {

                if (hit.transform.tag == "Player")
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawLine(transform.position, hit.point);
            }
        }
    }
}