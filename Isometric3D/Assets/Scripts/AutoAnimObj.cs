using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoAnimObj : MonoBehaviour
{
    [SerializeField]
    private string _stateName = "";
    private Animator _animator;

	public string stateName
	{
		get { return _stateName; }
		set { _stateName = value; }
	}

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
        _animator.Play(_stateName);
	}

	// Start is called before the first frame update
	void Start()
    {
    }

	// Update is called once per frame
	void Update()
    {
	}
}
