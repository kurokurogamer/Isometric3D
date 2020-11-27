using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextWave : MonoBehaviour
{
    [SerializeField]
    private float _distanceMove = 1;
    [SerializeField]
    private float _animationSpeed = 1;
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI tmpro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _textMeshPro.ForceMeshUpdate();
        var textInfo = _textMeshPro.textInfo;
        if(textInfo.characterCount == 0)
		{
            return;
		}

        //1文字毎にloop
        for (int index = 0; index < textInfo.characterCount; index++)
        {
            //1文字単位の情報
            var charaInfo = textInfo.characterInfo[index];

            //ジオメトリない文字はスキップ
            if (!charaInfo.isVisible)
            {
                continue;
            }

            //Material参照しているindex取得
            int materialIndex = charaInfo.materialReferenceIndex;

            //頂点参照しているindex取得
            int vertexIndex = charaInfo.vertexIndex;

            //テキスト全体の頂点を格納(変数のdestは、destinationの略)
            Vector3[] destVertices = textInfo.meshInfo[materialIndex].vertices;

            //移動する分
            float sinValue = Mathf.Sin(Time.time * _animationSpeed + 0.5f * (index + 1));

            // メッシュ情報にアニメーション後の頂点情報を入れる
            destVertices[vertexIndex + 0] += _distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 1] += _distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 2] += _distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 3] += _distanceMove * (Vector3.down * sinValue);

        }

        //ジオメトリ更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            //メッシュ情報を、実際のメッシュ頂点へ反映
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            _textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
