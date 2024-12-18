using System.Collections;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    public GameObject[] slides; // スライド画像の配列
    public float transitionDuration = 1.0f; // スライドの切り替え時間
    private int currentSlideIndex = 0; // 現在のスライドのインデックス

    void Start()
    {
        // 初期設定: すべてのスライドを非表示にする
        foreach (GameObject slide in slides)
        {
            slide.SetActive(false);
        }

        // 最初のスライドを表示する
        slides[currentSlideIndex].SetActive(true);
    }

    public void NextSlide()
    {
        StartCoroutine(SlideTransition(true));
    }

    public void PrevSlide()
    {
        StartCoroutine(SlideTransition(false));
    }

    private IEnumerator SlideTransition(bool isNext)
    {
        // 次のスライドのインデックスを計算
        int nextSlideIndex = isNext ? (currentSlideIndex + 1) % slides.Length : (currentSlideIndex - 1 + slides.Length) % slides.Length;

        // 現在のスライドと次のスライドを取得
        GameObject currentSlide = slides[currentSlideIndex];
        GameObject nextSlide = slides[nextSlideIndex];

        // 次のスライドを表示し、初期位置を設定
        nextSlide.SetActive(true);
        RectTransform nextSlideRect = nextSlide.GetComponent<RectTransform>();
        RectTransform currentSlideRect = currentSlide.GetComponent<RectTransform>();

        // 次のスライドの位置を画面外から開始
        Vector3 nextSlideStartPosition = isNext ? new Vector3(Screen.width, 0, 0) : new Vector3(-Screen.width, 0, 0);
        nextSlideRect.position = nextSlideStartPosition;

        // アニメーション開始
        float elapsedTime = 0f;
        Vector3 currentSlideEndPosition = isNext ? new Vector3(-Screen.width, 0, 0) : new Vector3(Screen.width, 0, 0);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / transitionDuration;

            // 現在のスライドを画面外へ移動
            currentSlideRect.position = Vector3.Lerp(currentSlideRect.position, currentSlideEndPosition, progress);

            // 次のスライドを画面内へ移動
            nextSlideRect.position = Vector3.Lerp(nextSlideStartPosition, currentSlide.transform.position, progress);

            yield return null;
        }

        // 最終的な位置を設定
        currentSlideRect.position = currentSlideEndPosition;
        nextSlideRect.position = currentSlide.transform.position;

        // 現在のスライドを非表示にする
        currentSlide.SetActive(false);

        // 現在のスライドインデックスを更新
        currentSlideIndex = nextSlideIndex;
    }
}
