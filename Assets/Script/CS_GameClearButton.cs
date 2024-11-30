using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // シーン管理に必要
using System.Collections; // コルーチンを使うために必要

public class CS_GameClearButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup; // CanvasGroupの参照を追加
    private Button button; // Buttonコンポーネントの参照
    [SerializeField] private Vector3 originalScale = new Vector3(1f, 1f, 1f); // 初期サイズを指定
    [SerializeField] private Vector3 hoverScale = new Vector3(0.8f, 0.8f, 0.8f); // カーソルが乗った時のサイズ
    [SerializeField] private float scaleDuration = 0.5f; // 縮小にかかる時間
    [SerializeField] private float fadeDuration = 1f; // フェードインにかかる時間
    [SerializeField] private string targetSceneName;   // 遷移先のシーン名

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); // CanvasGroupコンポーネントを取得
        button = GetComponent<Button>(); // Buttonコンポーネントを取得
        rectTransform.localScale = originalScale; // ゲーム開始時に指定された元のサイズにセット
        canvasGroup.alpha = 0f; // 最初は透明に設定
        button.interactable = false; // フェード中はクリックを無効化

        // フェードイン処理
        StartCoroutine(FadeIn(fadeDuration));
    }

    // フェードインのコルーチン
    private IEnumerator FadeIn(float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / duration); // 透明度を変化させる
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // 最後に確実に完全に表示させる
        button.interactable = true; // フェード完了後にクリックを有効化
    }

    // カーソルがボタンに乗ったとき
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable) // ボタンが有効な場合のみ動作
        {
            StopAllCoroutines(); // 現在のコルーチンを停止
            // カーソルが乗った時にスムーズに縮小
            StartCoroutine(SmoothScale(rectTransform.localScale, hoverScale, scaleDuration));
        }
    }

    // カーソルがボタンから外れたとき
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable) // ボタンが有効な場合のみ動作
        {
            StopAllCoroutines(); // 現在のコルーチンを停止
            // カーソルが外れた時にスムーズに元のサイズに戻す
            StartCoroutine(SmoothScale(rectTransform.localScale, originalScale, scaleDuration));
        }
    }

    // ボタンがクリックされたとき
    public void OnButtonClick()
    {
        if (!string.IsNullOrEmpty(targetSceneName)) // シーン名が指定されている場合
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    // スムーズにスケールを変更するコルーチン
    private IEnumerator SmoothScale(Vector3 startScale, Vector3 endScale, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = endScale; // 最後に確実に目標スケールにする
    }
}
