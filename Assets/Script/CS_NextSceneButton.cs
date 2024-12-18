using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class CS_NextSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 次のシーンの名前を指定
    [SerializeField] private string nextSceneName;

    // 点滅の設定
    [SerializeField] private UnityEngine.UI.Image targetImage; // 点滅させるボタンの背景
    [SerializeField] private float blinkSpeed = 1.0f; // 点滅の速さ

    private Color originalColor; // 元の色
    private bool isBlinking = false; // 点滅中かどうか

    private void Start()
    {
        // ボタンのImageコンポーネントを取得
        if (targetImage == null)
        {
            targetImage = GetComponent<UnityEngine.UI.Image>();
        }

        if (targetImage != null)
        {
            originalColor = targetImage.color; // 元の色を記憶
        }
      
    }

    // 次のシーンをロードする
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
       
    }

    // マウスカーソルがボタン上に入った時
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            isBlinking = true;
            StartCoroutine(BlinkTransparency());
        }
    }

    // マウスカーソルがボタンから離れた時
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            isBlinking = false;
            targetImage.color = originalColor; // 元の色に戻す
        }
    }

    // 透明と表示の点滅処理
    private System.Collections.IEnumerator BlinkTransparency()
    {
        while (isBlinking)
        {
            // 完全に透明にする
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            yield return new WaitForSeconds(1.0f / blinkSpeed);

            // 元の透明度に戻す
            targetImage.color = originalColor;
            yield return new WaitForSeconds(1.0f / blinkSpeed);
        }
    }
}
