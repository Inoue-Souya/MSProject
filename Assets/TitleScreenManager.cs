using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public Button optionButton;  // オプションボタン
    public CanvasGroup fadeGroup;  // フェード用のCanvasGroup

    public float fadeDuration = 1.0f;  // フェード時間
    private bool isFading = false;

    void Start()
    {
        // オプションボタンにリスナーを追加
        optionButton.onClick.AddListener(OnOptionButtonClicked);

        // フェード用のCanvasGroupを初期化
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 0;  // 画面は最初から明るい状態からスタート
        }
    }

    void Update()
    {
        // Enterキーまたは右クリックでシーン遷移
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(1)) && !isFading)
        {
            StartCoroutine(FadeOutAndLoadScene("StorySwitch"));  // フェードアウトとシーン遷移
        }
    }

    // オプションボタンがクリックされたときの処理
    void OnOptionButtonClicked()
    {
        Debug.Log("Option button clicked!");
    }

    // フェードアウトしてシーンをロード（明るい状態から暗くなる）
    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isFading = true;
        float timer = 0f;

        // フェードアウト（画面が暗くなる）
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);  // 0から1に線形補間でフェードアウト
            yield return null;
        }

        // シーンをロード
        SceneManager.LoadScene(sceneName);
    }
}
