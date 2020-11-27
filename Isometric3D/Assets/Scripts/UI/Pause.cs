using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	[SerializeField, Tooltip("PauseUI")]
	private GameObject _pauseUI = null;

	// Start is called before the first frame update
	void Start()
	{
	}

	private void PauseChange()
	{
		if (Input.GetButtonDown("Menu") || Input.GetKeyDown(KeyCode.M))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				_pauseUI.SetActive(true);
			}
			else
			{
				Time.timeScale = 1;
				_pauseUI.SetActive(false);
			}
		}
	}


	// Update is called once per frame
	void Update()
	{
		PauseChange();
	}
}
