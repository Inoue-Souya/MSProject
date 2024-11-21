using UnityEngine;

public class CS_FadeInButton : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // フェードインにかかる時間
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0f;

    void Start()
    {
        // ボタンに CanvasGroup がアタッチされていることを確認
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup コンポーネントがボタンにアタッチされていません。");
            return;
        }

        // 最初は透明に設定
        canvasGroup.alpha = 0f;
    }

    public void FadeIn()
    {
        // フェードインを開始する
        StartCoroutine(FadeInCoroutine());
    }

    private System.Collections.IEnumerator FadeInCoroutine()
    {
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // 徐々にアルファ値を増加
            yield return null;
        }
    }
}
