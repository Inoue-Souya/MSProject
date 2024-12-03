using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 追加
using UnityEngine.EventSystems; // EventTrigger用

public class CS_StorySwitch : MonoBehaviour
{
    public Button fadeOutButton; // フェードアウト開始用のボタン
    public UnityEngine.UI.Image[] images;
    public float slideSpeed = 5f;
    public AudioClip clickClip;
    public AudioClip bgmClip;
    public float bgmVolume = 0.5f;
    public float clickVolume = 1.0f;
    private AudioSource bgmSource;
    private AudioSource clickSource;
    private int currentIndex = 0;
    private bool isSliding = false; // スライド中かどうかを管理するフラグ
    public float fadeDuration = 1f;
    private bool isFading = false;
    public string nextSceneName = "NextScene"; // 次のシーン名
    private bool isButtonClicked = false; // ボタンが押されたかどうかを管理するフラグ
    private bool isPointerOverButton = false; // ポインタがボタン上にあるかどうか

    public CS_FadeIn fade;

    private Coroutine blinkCoroutine; // 点滅用のCoroutine

    void Start()
    {
        // ボタンを最初に非表示にする
        if (fadeOutButton != null)
        {
            fadeOutButton.gameObject.SetActive(false); // 非表示に設定
        }

        // BGM用のAudioSourceを作成し、初期設定
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();

        // クリック音用のAudioSourceを作成
        clickSource = gameObject.AddComponent<AudioSource>();
        clickSource.clip = clickClip;
        clickSource.volume = clickVolume;

        // ボタンのクリックイベントにStartFadeOutを登録
        if (fadeOutButton != null)
        {
            fadeOutButton.onClick.AddListener(() =>
            {
                if (!isSliding) // スライド中でない場合のみボタンを押せる
                {
                    isButtonClicked = true;   // フラグを設定
                    PlayClickSound();         // クリック音を再生
                    StartFadeOut();           // フェードアウトを開始
                }
            });

            // ボタンにEventTriggerを追加し、カーソルが上に乗ったことを検出
            EventTrigger trigger = fadeOutButton.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            pointerEnterEntry.callback.AddListener((data) => OnPointerEnter());
            trigger.triggers.Add(pointerEnterEntry);

            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            pointerExitEntry.callback.AddListener((data) => OnPointerExit());
            trigger.triggers.Add(pointerExitEntry);
        }
    }

    void Update()
    {
        if (fade.fadeFinish)
        {
            // スライド中でなく、カーソルがボタン上にない場合のみスライド処理を実行
            if (Input.GetMouseButtonDown(0) && currentIndex < images.Length && !isSliding && !isFading && !isButtonClicked && !isPointerOverButton)
            {
                PlayClickSound();
                if (currentIndex == images.Length - 1)
                {
                    StartFadeOut();
                }
                else
                {
                    StartSliding();
                }
            }

            if (isSliding)
            {
                SlideCurrentImage();
            }

            if (isFading)
            {
                FadeOutCurrentImage();
            }

            // 最初のフェードインが完了した後にボタンを表示する
            if (fade.fadeFinish && fadeOutButton != null && !fadeOutButton.gameObject.activeSelf)
            {
                // Coroutineで遅延を追加してボタンを表示
                StartCoroutine(ShowButtonWithDelay(0f)); // 1秒遅延
            }
        }
    }

    IEnumerator ShowButtonWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待つ
        fadeOutButton.gameObject.SetActive(true); // ボタンを表示
    }

    void PlayClickSound()
    {
        if (clickSource != null && clickSource.clip != null)
        {
            clickSource.Play(); // クリック音を再生
        }
    }

    void StartSliding()
    {
        if (currentIndex < images.Length)
        {
            images[currentIndex].transform.localPosition = Vector3.zero;
            isSliding = true; // スライディングを開始
            fadeOutButton.interactable = false; // スライド中はボタンを無効化
        }
    }

    void SlideCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex];
        float imageWidth = currentImage.rectTransform.rect.width; // 画像の幅を取得

        if (currentIndex < images.Length - 1)
        {
            if (currentImage.transform.localPosition.x > -imageWidth)
            {
                currentImage.transform.localPosition += Vector3.left * slideSpeed * Time.deltaTime;
            }
            else
            {
                currentIndex++;
                isSliding = false; // スライディング終了
                fadeOutButton.interactable = true; // スライド終了後、ボタンを再度有効化
            }
        }
        else
        {
            currentImage.transform.localPosition = Vector3.zero;
        }
    }

    void StartFadeOut()
    {
        if (!isFading) // すでにフェード中でない場合のみ実行
        {
            isFading = true;
            fadeOutButton.interactable = false; // フェードアウト中はボタンを無効化
            Color currentColor = images[currentIndex].color;
            images[currentIndex].color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        }
    }

    void FadeOutCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex];
        Color currentColor = currentImage.color;
        float darkenAmount = Time.deltaTime / fadeDuration;
        currentImage.color = new Color(
            Mathf.Lerp(currentColor.r, 0f, darkenAmount),
            Mathf.Lerp(currentColor.g, 0f, darkenAmount),
            Mathf.Lerp(currentColor.b, 0f, darkenAmount),
            currentColor.a
        );

        // フェード進行状況をログで確認
        UnityEngine.Debug.Log("フェード進行中: " + currentImage.color);
        if (currentImage.color.r <= 0.1f && currentImage.color.g <= 0.1f && currentImage.color.b <= 0.1f)
        {
            isFading = false;
            UnityEngine.Debug.Log("フェードが完了しました。次のシーンに移動します。");
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        UnityEngine.Debug.Log("シーン遷移: " + nextSceneName);
        // 次のシーンをロード
        SceneManager.LoadScene("Stage1");
    }

    // ボタン上にカーソルが乗ったときにスライドを無効化し、ボタンを点滅させる
    void OnPointerEnter()
    {
        isPointerOverButton = true; // ボタン上にカーソルが乗った
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // 既存の点滅を停止
        }
        blinkCoroutine = StartCoroutine(BlinkButton()); // 点滅を再開
    }

    // ボタンからカーソルが離れたときに点滅を停止し、ボタンを元の色に戻す
    void OnPointerExit()
    {
        isPointerOverButton = false; // ボタンからカーソルが離れた
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // 点滅を停止
            blinkCoroutine = null; // 参照をクリア
            ChangeButtonOpacity(1f); // ボタンの透明度を元に戻す
        }
    }

    // ボタンの色を変更するメソッド
    void ChangeButtonOpacity(float alpha)
    {
        if (fadeOutButton != null)
        {
            Color color = fadeOutButton.image.color;
            color.a = alpha;
            fadeOutButton.image.color = color;
        }
    }

    // ボタンを点滅させるコルーチン
    IEnumerator BlinkButton()
    {
        while (isPointerOverButton)
        {
            ChangeButtonOpacity(0f); // 透明にする
            yield return new WaitForSeconds(0.5f); // 0.5秒待つ
            ChangeButtonOpacity(1f); // 元の表示に戻す
            yield return new WaitForSeconds(0.5f); // 0.5秒待つ
        }
    }
}
