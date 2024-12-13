using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CS_FadeImageTextManager : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // フェード用の黒い画像
    public GameObject[] imagesToShow; // 表示する画像の配列
    public UnityEngine.UI.Text[] textsToShow; // 表示するテキストの配列
    public int maxImageClickCount = 1; // 画像が非表示になるまでのクリック回数
    public int maxTextClickCount = 1; // テキストが非表示になるまでのクリック回数

    private int imageClickCount = 0;
    private int textClickCount = 0;
    private bool isFading = false;

    void Start()
    {
        // 初期設定: 画像とテキストを非表示に
        foreach (var image in imagesToShow)
        {
            image.SetActive(false);
        }

        foreach (var text in textsToShow)
        {
            text.gameObject.SetActive(false);
        }

        // フェードを開始
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        Color fadeColor = fadeImage.color;
        fadeColor.a = 1f;
        fadeImage.color = fadeColor;

        while (fadeColor.a > 0.5f)
        {
            fadeColor.a -= Time.deltaTime / 2f; // 2秒でフェード
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0.5f;
        fadeImage.color = fadeColor;
        fadeImage.gameObject.SetActive(false); // フェード用画像を非表示
        isFading = false;

        // 画像とテキストを表示
        foreach (var image in imagesToShow)
        {
            image.SetActive(true);
        }

        foreach (var text in textsToShow)
        {
            text.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // マウスクリックの処理
        if (!isFading && Input.GetMouseButtonDown(0))
        {
            if (imageClickCount < maxImageClickCount)
            {
                imageClickCount++;

                if (imageClickCount >= maxImageClickCount)
                {
                    // 画像を非表示に
                    foreach (var image in imagesToShow)
                    {
                        image.SetActive(false);
                    }
                }
            }

            if (textClickCount < maxTextClickCount)
            {
                textClickCount++;

                if (textClickCount >= maxTextClickCount)
                {
                    // テキストを非表示に
                    foreach (var text in textsToShow)
                    {
                        text.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
