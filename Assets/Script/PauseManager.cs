using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public Button hoverButton;         // ホバーで画像を表示するボタン
    public Image hoverImage;          // 表示する画像


    private Transform originalParent;  // 元の親オブジェクトを保存
    private int originalSiblingIndex; // 元の順番を保存

    public GameObject pauseMenu;
    public GameObject slidePanel;
    public GameObject blockPanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button returnToTitleButton;
    public Button nextSlideButton;
    public Button prevSlideButton;
 private Vector3 originalScale;      // ボタンの元のスケール
    public float hoverScale = 0.9f;     // ホバー時のスケール倍率
    public float animationSpeed = 0.1f; // アニメーション速度

    // アニメーションの速度を制御する変数
    public float hoverAnimationSpeed = 5.0f;
    private Coroutine hoverCoroutine;

    [SerializeField]
    private List<SlideUIControl> slides;

    private int currentSlideIndex = 0;
    private bool isSliding = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
        slidePanel.SetActive(false);
        blockPanel.SetActive(false);

        pauseButton.onClick.AddListener(TogglePauseMenu);
        resumeButton.onClick.AddListener(ResumeGame);
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        nextSlideButton.onClick.AddListener(NextSlide);
        prevSlideButton.onClick.AddListener(PrevSlide);

        InitializeSlides();

        hoverImage.gameObject.SetActive(false); // 初期状態で非表示

        // ボタンのイベント設定
        EventTrigger trigger = hoverButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { ShowHoverImage(); });
        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { HideHoverImage(); });
        trigger.triggers.Add(pointerExit);
        // 画像の親情報を保存
        originalParent = hoverImage.transform.parent;
        originalSiblingIndex = hoverImage.transform.GetSiblingIndex();
        // 元のスケールを記憶
        originalScale = returnToTitleButton.transform.localScale;

        // ボタンにイベントリスナーを設定
        UnityEngine.EventSystems.EventTrigger trigger2 = returnToTitleButton.gameObject.AddComponent<EventTrigger>();

        // ホバー開始時
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => OnButtonHover(true));
        trigger2.triggers.Add(entryEnter);

        // ホバー終了時
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => OnButtonHover(false));
        trigger2.triggers.Add(entryExit);
    }

    private void InitializeSlides()
    {
        for (int i = 0; i < slides.Count; i++)
        {
            slides[i].state = (i == 0) ? 1 : 0; // 最初のスライドだけ中央に表示
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            slidePanel.SetActive(true);
            blockPanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        slidePanel.SetActive(false);
        blockPanel.SetActive(false);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Scene");
    }

    public void NextSlide()
    {
        if (isSliding || slides.Count <= 1) return;

        StartCoroutine(SlideToNext());
    }

    public void PrevSlide()
    {
        if (isSliding || slides.Count <= 1) return;

        StartCoroutine(SlideToPrevious());
    }
    private IEnumerator SlideToNext()
    {
        isSliding = true;

        // 次のスライドのインデックスを計算
        int nextSlideIndex = (currentSlideIndex + 1) % slides.Count;

        // 現在のスライドを右にスライドアウト
        slides[currentSlideIndex].state = 2;

        // 次のスライドを初期位置 (左) に設定し、中央へスライドイン
        slides[nextSlideIndex].state = 0;
        slides[nextSlideIndex].transform.localPosition = slides[nextSlideIndex].outPos01;
        slides[nextSlideIndex].state = 1;

        // アニメーション完了まで待機
        yield return new WaitForSecondsRealtime(1.5f);

        // インデックス更新とスライド終了処理
        currentSlideIndex = nextSlideIndex;
        isSliding = false;
    }
    private IEnumerator SlideToPrevious()
    {
        isSliding = true;

        // 前のスライドのインデックスを計算
        int prevSlideIndex = (currentSlideIndex - 1 + slides.Count) % slides.Count;

        // 現在のスライドを左にスライドアウト
        slides[currentSlideIndex].state = 2;

        // 前のスライドを右から中央に移動させる設定
        slides[prevSlideIndex].state = 0; // 初期状態
        slides[prevSlideIndex].transform.localPosition = slides[prevSlideIndex].outPos02; // 右に設定
        slides[prevSlideIndex].state = 1; // 中央にスライドイン

        // アニメーション完了まで待機
        yield return new WaitForSecondsRealtime(1.5f);

        // インデックスを更新してスライド処理を完了
        currentSlideIndex = prevSlideIndex;
        isSliding = false;
    }
    private void OnButtonHover(bool isHovering)
    {
        if (isHovering)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(returnToTitleButton.transform, hoverScale));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(returnToTitleButton.transform, originalScale.x));
        }
    }

    private IEnumerator ScaleButton(Transform buttonTransform, float targetScale)
    {
        Vector3 target = originalScale * targetScale;

        while (Vector3.Distance(buttonTransform.localScale, target) > 0.01f)
        {
            // 現在のスケールが元のスケールを超えないように制限
            Vector3 newScale = Vector3.Lerp(buttonTransform.localScale, target, animationSpeed);
            newScale = Vector3.Min(newScale, originalScale); // 元のスケールを超えないようにする
            buttonTransform.localScale = newScale;

            yield return null;
        }

        buttonTransform.localScale = target;
    }

    private void ShowHoverImage()
    {
        hoverImage.gameObject.SetActive(true);

        // ピボットを左上に設定
        hoverImage.rectTransform.pivot = new Vector2(0, 1);
        hoverImage.rectTransform.localScale = Vector3.zero; // 初期スケールをゼロに設定

        // 最前面に移動
        hoverImage.transform.SetParent(hoverImage.canvas.transform, true);
        hoverImage.transform.SetAsLastSibling();

        // アニメーション開始
        if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
        hoverCoroutine = StartCoroutine(AnimateHoverImage(Vector3.one)); // 標準スケール (1, 1, 1)
    }


    private void HideHoverImage()
    {
        if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
        hoverCoroutine = StartCoroutine(AnimateHoverImage(Vector3.zero, () =>
        {
            // アニメーション完了後に非表示と元の親に戻す
            hoverImage.gameObject.SetActive(false);
            hoverImage.transform.SetParent(originalParent, true);
            hoverImage.transform.SetSiblingIndex(originalSiblingIndex);
        }));
    }

    private IEnumerator AnimateHoverImage(Vector3 targetScale, System.Action onComplete = null)
    {
        RectTransform rt = hoverImage.rectTransform;
        Vector3 initialScale = rt.localScale;

        // アニメーションのための時間を指定
        float duration = 0.2f; // 例: 0.5秒でアニメーション
        float time = 0;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / duration; // アニメーションの進行度

            // スケールを補間して設定
            rt.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // 最終スケールを確定
        rt.localScale = targetScale;
    }



}
