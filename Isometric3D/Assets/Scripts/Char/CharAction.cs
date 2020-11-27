using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAction : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

	public void Play()
	{
        _animator.Play("Much");
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
