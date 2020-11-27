using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSkipButton : MonoBehaviour
{
    private FadeUI _ui;
    [SerializeField]
    private List<GameObject> _obj = new List<GameObject>();
    [SerializeField]
    private bool _active = true;
    [SerializeField]
    private string _addSceneName = "";
	[SerializeField]
	private ButtonAction _buttonAction = null;
	

    // Start is called before the first frame update
    void Start()
    {
        _ui = GetComponent<FadeUI>();
    }

    private void Skip()
    {
        _ui.FadeSkip();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3")
            || Input.GetButtonDown("Mouse0") || Input.GetButtonDown("Mouse1") || Input.GetButtonDown("Mouse2"))
        {
            Skip();
            if(!_active)
            {
                _active = true;
                if (_addSceneName != "")
                {
					if (_buttonAction)
					{
						//_buttonAction.Active();
					}
					else
					{
						SceneManager.LoadScene(_addSceneName, LoadSceneMode.Additive);

					}
                }
                this.gameObject.SetActive(false);
            }
        }

        if (_ui.Alpha == 1 && _active)
        {
            for(int i = 0; i < _obj.Count; i++)
            {
                _obj[i].SetActive(true);
            }
            this.enabled = false;
        }
    }
}
