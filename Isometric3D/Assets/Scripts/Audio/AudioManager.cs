using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public enum AUDIO
    {
        SYSTEMSE,
        GAMESE,
        BGM,
        VOICE,
        MAX
    }

    public static AudioManager instans = null;

    private Dictionary<AUDIO, AudioSource> _sourcDictionary;
    [SerializeField]
    private AudioMixer _mixer = null;

    private Coroutine _coroutine;

    private void Awake()
    {
        if (instans == null)
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
        AUDIO type = AUDIO.SYSTEMSE;
        foreach (Transform child in transform)
        {
            // GetComponet&nullチェック
            if (child.TryGetComponent(out AudioSource source))
            {
                Debug.Log(type + " = " + source.gameObject.name);
                _sourcDictionary.Add(type, source);
                // タイプを一つ進める
                type++;
            }
        }
    }

    // サウンド再生(重複あり)
    public void Play(AUDIO type, ulong delay = 0)
    {
        _sourcDictionary[type].Play(delay);
    }

    // サウンド再生(重複なし)
    public void PlayOneShot(AUDIO type, AudioClip clip)
    {
        _sourcDictionary[type].PlayOneShot(clip);
    }


    public void PlayOneSE(AudioClip clip, AUDIO type = AUDIO.SYSTEMSE)
    {
        _sourcDictionary[type].PlayOneShot(clip);
    }

    public void PlayOneBGM(AudioClip clip)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(LoopBGM(clip));
        }

        // _sourcDictionary[AUDIO.BGM].PlayOneShot(clip);
    }

    public void PlayOneVoice(AudioClip clip)
    {
        _sourcDictionary[AUDIO.VOICE].PlayOneShot(clip);
    }

    public void Stop(AUDIO type)
    {
        _sourcDictionary[type].Stop();
    }

    private IEnumerator LoopBGM(AudioClip clip)
    {
        while (true)
        {
            // 再生中でなければ再生する
            if (!_sourcDictionary[AUDIO.BGM].isPlaying)
            {
                _sourcDictionary[AUDIO.BGM].PlayOneShot(clip);
            }
            yield return null;
        }
    }

    public void AllStop()
    {
        // ループで全てのタイプのサウンドを停止する
        foreach (var sound in _sourcDictionary)
        {
            sound.Value.Stop();
        }
    }

    // 音量変更用関数
    public void SetVolume(AUDIO type, float volume)
    {
        switch (type)
        {
            case AUDIO.SYSTEMSE:
                _mixer.SetFloat("SystemSEVolume", Mathf.Lerp(-80, 0, volume));
                break;
            case AUDIO.GAMESE:
                _mixer.SetFloat("GameSEVolume", Mathf.Lerp(-80, 0, volume));
                break;
            case AUDIO.BGM:
                _mixer.SetFloat("BGMSEVolume", Mathf.Lerp(-80, 0, volume));
                break;
            case AUDIO.VOICE:
                _mixer.SetFloat("VOICEVolume", Mathf.Lerp(-80, 0, volume));
                break;
            case AUDIO.MAX:
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}