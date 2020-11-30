using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
	public static SceneCtl instance = null;

	private Coroutine _coroutine;
	[SerializeField]
	private FadeUI _fade = null;
	[SerializeField]
	private GameObject _ui;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		_coroutine = null;
	}

	// シーンのロード
	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	// シーンのロード(非同期読み込み)
	public void LoadSceneAsync(string name)
	{
		if (_coroutine == null)
		{
			_coroutine = StartCoroutine(Load(name, LoadSceneMode.Single));
		}
	}

	// シーンの追加
	public void AddScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}

	// シーンの追加(非同期読み込み)
	public void AddSceneAsync(string name)
	{
		if (_coroutine == null)
		{
			_coroutine = StartCoroutine(Load(name, LoadSceneMode.Additive));
		}
	}

	// シーンの削除
	public void UnLoadScene(string name)
	{
		SceneManager.UnloadSceneAsync(name);
	}

	// 非同期読み込み処理
	private IEnumerator Load(string name, LoadSceneMode loadType = LoadSceneMode.Single)
	{
		GameObject obj = null;
		foreach(Transform child in transform)
		{
			obj = child.gameObject;
		}
		Debug.Log("読み込み開始");
		AsyncOperation async = SceneManager.LoadSceneAsync(name, loadType);
		async.allowSceneActivation = false;
		_ui.SetActive(true);
		yield return new WaitForSeconds(3.0f);
		while (!async.isDone)
		{
			if (async.progress >= 0.9f)
			{
				Debug.Log("読み込み完了");
				async.allowSceneActivation = true;
			}
			yield return null;
		}
		_coroutine = null;
		_ui.SetActive(false);
		_fade.Mode = FadeUI.FADE_MODE.OUT;
		_fade.Active = true;
		yield return new WaitForSeconds(3.0f);

		if (obj != null)
		{
			obj.SetActive(false);
		}
		Debug.Log("読み込み終了");

		yield return null;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
