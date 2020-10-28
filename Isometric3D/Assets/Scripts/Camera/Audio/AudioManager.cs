using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AUDIO_TYPE
	{
        SE,
        BGM,
        VOICE,
        MAX
	}

    public static AudioManager instans = null;

    private List<AudioSource> _sourceList;

	private void Awake()
	{
		if(instans == null)
		{
            instans = this;
            DontDestroyOnLoad(instans);
		}
        else
		{
            Destroy(this.gameObject);
		}
	}
	// Start is called before the first frame update
	void Start()
    {
        _sourceList = new List<AudioSource>();
        foreach(Transform child in transform)
		{
            // nullチェック
            if(child.TryGetComponent(out AudioSource source))
			{
                _sourceList.Add(source);
			}
		}
    }



    public void PlayOneShot(AudioClip clip)
	{
        _sourceList[(int)AUDIO_TYPE.SE].PlayOneShot(clip);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
