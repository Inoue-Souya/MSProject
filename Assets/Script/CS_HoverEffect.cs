using UnityEngine;
using UnityEngine.EventSystems;

public class CS_HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.0f); // ホバー時のスケール
    public float scaleDuration = 0.2f;  // スケール変更にかかる時間

    private Vector3 originalScale;  // 元のスケール
    private bool isHovered = false;

    void Start()
    {
        // 初期スケールを保存
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ホバーしたときの処理
        isHovered = true;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ホバーを外したときの処理
        isHovered = false;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 currentScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(currentScale, targetScale, elapsedTime / scaleDuration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
