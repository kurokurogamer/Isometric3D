using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blur : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private int _iteration = 1;

    public int iteration
	{
		get { return _iteration; }
		set { _iteration = value; }
	}

    // 4点をサンプリングして色を作るマテリアル
    [SerializeField]
    private Material _material = null;

    private RenderTexture[] _renderTextures = new RenderTexture[30];

    private void OnRenderImage(RenderTexture source, RenderTexture dest)
    {

        float width = source.width;
        float height = source.height;
        var currentSource = source;

        var i = 0;
        RenderTexture currentDest = null;
        
        // ダウンサンプリング
        for (; i < _iteration; i++)
        {
            width /= 1.1f;
            height /= 1.1f;
            if (width < 2 || height < 2)
            {
                break;
            }
            currentDest = _renderTextures[i] = RenderTexture.GetTemporary((int)width, (int)height, 0, source.format);

            // Blit時にマテリアルとパスを指定する
            Graphics.Blit(currentSource, currentDest, _material, 0);

            currentSource = currentDest;
        }

        // アップサンプリング
        for (i -= 2; i >= 0; i--)
        {
            currentDest = _renderTextures[i];

            // Blit時にマテリアルとパスを指定する
            Graphics.Blit(currentSource, currentDest, _material, 1);

            _renderTextures[i] = null;
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDest;
        }

        // 最後にdestにBlit
        Graphics.Blit(currentSource, dest, _material, 1);
        RenderTexture.ReleaseTemporary(currentSource);
    }

	private void OnDisable()
	{
		_iteration = 0;
	}

	private void Update()
	{
		_iteration = (int)Mathf.MoveTowards(_iteration, 10, Time.unscaledDeltaTime * 50.0f);
	}
}
