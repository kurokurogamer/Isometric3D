using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
	public static SceneCtl instans = null;

	private Coroutine _coroutine;

	private void Awake()
	{
		if (instans == null)
		{
			instans = this;
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
		// テスト
		//AddScene("GameUI");
	}

	// シーンのロード
	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}

	// シーンのロード(非同期読み込み)
	public void LoadSceneAsync(string name)
	{
		_coroutine = StartCoroutine(Load(name));
	}

	// シーンの追加
	public void AddScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}

	// シーンの削除
	public void UnLoadScene(string name)
	{
		SceneManager.UnloadSceneAsync(name);
	}

	private IEnumerator Load(string name)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(name);
		async.allowSceneActivation = false;

		while (!async.isDone)
		{
			if (async.progress >= 0.9)
			{
				async.allowSceneActivation = true;
			}
			yield return null;
		}
		yield return null;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
