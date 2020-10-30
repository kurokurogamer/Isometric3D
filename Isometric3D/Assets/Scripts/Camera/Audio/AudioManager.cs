using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AUDIO
	{
        SE,
        BGM,
        VOICE,
        MAX
	}

    public static AudioManager instans = null;

    private Dictionary<AUDIO, AudioSource> _sourcDictionary;

    [SerializeField]
    private List<AudioSource> _sourceList;

    private Coroutine _coroutine;

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
        // Dictionaryタイプ
        _sourcDictionary = new Dictionary<AUDIO, AudioSource>();
        AUDIO type = AUDIO.SE;
        foreach (Transform child in transform)
        {
            // nullチェック
            if (child.TryGetComponent(out AudioSource source))
            {
                Debug.Log(type + " = " + source.gameObject.name);
                _sourcDictionary.Add(type, source);
                // タイプを一つ進める
                type++;
            }
        }
        // Listタイプ
        _sourceList = new List<AudioSource>();
        foreach(Transform child in transform)
		{
            // nullチェック
            if(child.TryGetComponent(out AudioSource source))
			{
                Debug.Log(type + " = " + source.gameObject.name);
                _sourceList.Add(source);
			}
		}
    }

    public void Play(AUDIO type, ulong delay = 0)
    {
        _sourcDictionary[type].Play(delay);
        //_sourceList[(int)type].Play(delay);
    }

	public void PlayOneSE(AudioClip clip)
	{
        _sourcDictionary[AUDIO.SE].PlayOneShot(clip);
        // _sourceList[(int)AUDIO.SE].PlayOneShot(clip);
	}

    public void PlayOneBGM(AudioClip clip)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(LoopBGM(clip));
        }

        // _sourcDictionary[AUDIO.BGM].PlayOneShot(clip);
        // _sourceList[(int)AUDIO.BGM].PlayOneShot(clip);
    }

    public void PlayOneVoice(AudioClip clip)
    {
        _sourcDictionary[AUDIO.VOICE].PlayOneShot(clip);
        // _sourceList[(int)AUDIO.VOICE].PlayOneShot(clip);
    }

    public void Stop(AUDIO type)
	{
        _sourcDictionary[type].Stop();
        // _sourceList[(int)type].Stop();
	}

    private IEnumerator LoopBGM(AudioClip clip)
	{
        while(true)
		{
            // 再生中でなければ再生する
            if(!_sourcDictionary[AUDIO.BGM].isPlaying)
			{
                _sourcDictionary[AUDIO.BGM].PlayOneShot(clip);
			}
            yield return null;
		}
	}

    public void AllStop()
	{
        foreach (var sound in _sourcDictionary)
        {
            sound.Value.Stop();
            // 同じ処理
            // _sourcDictionary[sound.Key].Stop(); ;
        }
        // ループで全てのAudioSourceのStopを呼ぶ
        foreach (AudioSource source in _sourceList)
		{
            source.Stop();
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
