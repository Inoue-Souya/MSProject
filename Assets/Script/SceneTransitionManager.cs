using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage; // 画面全体を暗くするための Image コンポーネント
    public float fadeDuration = 1.0f; // フェードアウトにかかる時間

    private bool isTransitioning = false;

    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 0f); // 最初は透明
        }
        else
        {
            Debug.LogError("FadeImage が設定されていません！");
        }
    }

    // フェードアウトしてからシーン遷移
    public void FadeToScene(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeOutAndLoadScene(sceneName)); // フェードアウトとシーン遷移をコルーチンで実行
        }
    }

    private System.Collections.IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        float elapsedTime = 0f;

        // フェードアウト処理（画面を暗くする）
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // 透明度を増やして画面を暗くする
            fadeImage.color = new Color(0f, 0f, 0f, alpha); // 黒でフェードアウト
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 1f); // 完全に暗くする
        Debug.Log("フェードアウト完了");

        // シーン遷移
        SceneManager.LoadScene(sceneName);
    }
}
