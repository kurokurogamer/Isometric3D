using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioChanger : MonoBehaviour
{
    [SerializeField]
    private AudioManager.AUDIO _type = AudioManager.AUDIO.SYSTEMSE;
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetVolume()
    {
        AudioManager.instans.SetVolume(_type, _slider.value);
    }

    private void InputCheck()
	{
        Vector2 Axis = Vector2.zero;
        Axis.x = Input.GetAxis("Horizontal");
        Axis.y = Input.GetAxis("Vertical");
        if(Axis.x >= 0.1f || Input.GetKey(KeyCode.Q))
		{
            _slider.value += 0.01f;
		}
        else if(Axis.y <= -0.1f || Input.GetKey(KeyCode.E))
		{
            _slider.value -= 0.01f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }
}
