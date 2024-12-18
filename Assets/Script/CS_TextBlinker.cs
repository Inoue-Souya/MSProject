using UnityEngine;
using TMPro;

public class CS_TextBlinker : MonoBehaviour
{
    public TMP_Text textMeshPro;    // TextMesh Proの参照
    public float fadeDuration = 1f; // フェードイン・フェードアウトにかかる時間（秒）

    private float alpha = 0f;       // 現在のアルファ値
    private bool isFadingIn = true; // フェードイン中かどうか

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }
    }

    private void Update()
    {
        // アルファ値を時間経過に応じて調整
        float fadeStep = Time.deltaTime / fadeDuration;
        alpha += isFadingIn ? fadeStep : -fadeStep;

        // アルファ値が0～1の範囲を超えたら方向を反転
        if (alpha >= 1f)
        {
            alpha = 1f;
            isFadingIn = false;
        }
        else if (alpha <= 0f)
        {
            alpha = 0f;
            isFadingIn = true;
        }

        // TextMesh Proのアルファ値を設定
        SetTextAlpha(alpha);
    }

    private void SetTextAlpha(float alphaValue)
    {
        if (textMeshPro != null)
        {
            Color color = textMeshPro.color;
            color.a = alphaValue; // アルファ値を変更
            textMeshPro.color = color;
        }
    }
}
