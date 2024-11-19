using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class CS_FadeIn : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage;  // フェード用のImageコンポーネント
    public float fadeDuration = 2f;  // フェードインにかかる時間
    public bool fadeFinish;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeImage == null)
        {
            // Iyageが設定されていない場合はエラーメッセージを表示
            UnityEngine.Debug.Log("Fade image is not assigned!");
            return;
        }

        fadeFinish = false;

        // フェードインを開始
        StartCoroutine(FadeInCoroutine());
    }

    // フェードインのコルーチン
    IEnumerator FadeInCoroutine()
    {
        float timeElapsed = 0f;

        // 現在のアルファ値を取得（初期値は1.0f（完全に不透明）に設定されているはず）
        Color startColor = fadeImage.color;
        startColor.a = 1f;
        fadeImage.color = startColor;

        // フェードインの処理
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;  // 次のフレームまで待機
        }

        // フェードイン完了時に透明度を完全に0にする
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        fadeImage.gameObject.SetActive(false);
        fadeFinish = true;
    }
}
