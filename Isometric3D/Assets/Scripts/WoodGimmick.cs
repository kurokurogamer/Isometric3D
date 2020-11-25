using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodGimmick : MonoBehaviour
{
    // エフェクトオブジェクト
    [SerializeField, Tooltip("パーティクルシステムが付いているオブジェクト(設定しなければ子のオブジェクトから自動で取得します)")]
    private GameObject _effect;
    private ParticleSystem _particle;
    private Animator _animator;
    private bool _active;
    private bool _ignition;
    private float _nowTime;

    public bool Active
	{
		get { return _active; }
	}

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_effect == null)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out ParticleSystem particle))
                {
                    _particle = particle;
                    _particle.Stop();
                    _effect = child.gameObject;
                }
            }
        }
        else
		{
            GameObject effect = Instantiate(_effect, transform.position, transform.rotation);
            transform.SetParent(effect.transform);
            _particle = effect.GetComponent<ParticleSystem>();
            _particle.Stop();
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (_ignition)
        {
            _nowTime += Time.deltaTime;
        }
        if (_nowTime >= 0.5f && !_active)
        {
            if (_animator != null)
            {
                _animator.Play("Fire");
            }
            else if (_particle != null)
            {
                _particle.Play();
            }
            _active = true;
            Destroy(this.gameObject, 10.0f);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("衝突したオブジェクト" + other);
        if (other.tag == "Much")
        {
            if (_animator != null)
            {
                _animator.Play("Fire");
            }
            else if (_particle != null)
            {
                _particle.Play();
            }
            _active = true;
            Destroy(this.gameObject, 10.0f);
        }
    }

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Tree" && !_ignition)
		{
            Debug.Log("木が近くにある");
            WoodGimmick gimmick = other.GetComponent<WoodGimmick>();
            _ignition = gimmick._active;
		}
	}
}
