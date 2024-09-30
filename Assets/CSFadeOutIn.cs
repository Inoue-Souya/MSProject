using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSFadeOutIn : MonoBehaviour
{
    public CanvasGroup canvasGroup; // フェードするオブジェクトのCanvasGroup
    public CanvasGroup textGroup;
    public float fadeDuration = 1.0f; // フェードにかかる時間

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartFadeOutIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOutIn());
    }

    private IEnumerator FadeOutIn()
    {
        // フェードインアウト開始

        // フェードアウト
        yield return StartCoroutine(Fade(0, 1, canvasGroup));

        yield return StartCoroutine(Fade(0, 1, textGroup));
        // フェードアウト後の待機時間（必要なら）
        yield return new WaitForSeconds(1.0f);

        // フェードイン
        yield return StartCoroutine(Fade(1, 0, textGroup));

        yield return StartCoroutine(Fade(1, 0, canvasGroup));

        // フェードインアウト終了処理
        gameObject.SetActive(false);
    }

    // フェード処理（from:開始alpha, to:終了alpha, フェードするUI）
    private IEnumerator Fade(float from, float to, CanvasGroup group)
    {
        float elapsed = 0.0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            group.alpha = alpha;
            yield return null;
        }

        // 最終的なalpha値を設定
        group.alpha = to;
    }
}
