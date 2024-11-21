using UnityEngine;
using System.Collections;

public class FadeInButton : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // フェードインにかかる時間
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError($"CanvasGroup コンポーネントがボタン '{gameObject.name}' にアタッチされていません。");
            return;
        }

        Debug.Log($"CanvasGroup が正しく設定されています: {gameObject.name}");
        canvasGroup.alpha = 0f; // 最初は透明
    }

    public void FadeIn()
    {
        if (canvasGroup != null)
        {
            Debug.Log($"FadeIn 開始: {gameObject.name}");
            elapsedTime = 0f;
            StartCoroutine(FadeInCoroutine());
        }
        else
        {
            Debug.LogError($"FadeIn を開始できません。CanvasGroup が設定されていません: {gameObject.name}");
        }
    }

    private IEnumerator FadeInCoroutine()
    {
        if (canvasGroup == null) yield break;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f; // 完全に表示状態
        Debug.Log($"FadeIn 完了: {gameObject.name}");
    }
}
