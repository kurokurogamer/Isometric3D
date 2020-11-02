using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChanger : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public float SetVolume
	{
		set { _mixer.SetFloat("SEVolume", Mathf.Lerp(-80, 0, value)); }
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
