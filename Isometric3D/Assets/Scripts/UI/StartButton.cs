using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ButtonAction
{
    private CanvasGroup _canvasGroup;
    private FadeUI _fade;
    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = transform.root.GetComponent<CanvasGroup>();
        _fade = transform.root.GetComponent<FadeUI>();
    }

	public override void Action()
	{
        Debug.Log("はじめから始める");
        _canvasGroup.alpha = 0;
        _fade.Active = true;
        //throw new System.NotImplementedException();
	}

	// Update is called once per frame
	void Update()
    {
        _canvasGroup.alpha = _fade.Alpha;
    }
}
