using System.Collections;
using UnityEngine;

public class CS_FadeInManager : MonoBehaviour
{
    public CanvasGroup fadeGroup;  // フェード用の CanvasGroup
    public float fadeDuration = 1.5f;  // フェードアウトの時間

    void Start()
    {
        // シーン開始時にフェードアウトを実行
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;

        // フェードアウト処理
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        // 最後に完全に透明にする
        fadeGroup.alpha = 0;
        fadeGroup.gameObject.SetActive(false);  // 完全にフェードアウト後、オブジェクトを無効化
    }
}
