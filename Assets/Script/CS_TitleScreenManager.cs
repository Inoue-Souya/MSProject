using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CS_TitleScreenManager : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public Image titleLogo;
    public TextMeshProUGUI titleText;

    // BGMÇ∆å¯â âπópÇÃAudioSourceÉtÉBÅ[ÉãÉh
    public AudioSource rainBGM;
    public AudioSource thunderSFX;

    public float fadeDuration = 1.0f;
    public float logoDropDuration = 2.0f;
    public float logoDisplayTime = 2.0f;

    private bool isFading = false;

    void Start()
    {
        fadeGroup.alpha = 0;
        titleLogo.gameObject.SetActive(false);
        rainBGM.loop = true;
        rainBGM.Play();  // âJBGMÇçƒê∂
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !isFading)
        {
            rainBGM.Stop();  // âJBGMÇí‚é~
            thunderSFX.Play();  // óãâπÇçƒê∂
            StartCoroutine(FadeToLogoAndLoadScene("StorySwitch"));
        }
    }

    IEnumerator FadeToLogoAndLoadScene(string sceneName)
    {
        isFading = true;
        titleText.gameObject.SetActive(false);

        yield return StartCoroutine(FadeIn());

        thunderSFX.Stop();  // îOÇÃÇΩÇﬂí‚é~
        yield return StartCoroutine(DropTitleLogo());

        yield return new WaitForSeconds(logoDisplayTime);

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = 1;
    }

    IEnumerator DropTitleLogo()
    {
        float timer = 0f;
        Vector2 startPos = new Vector2(0, 500);
        Vector2 endPos = new Vector2(0, 0);

        titleLogo.gameObject.SetActive(true);  // ÉçÉSÇï\é¶

        while (timer < logoDropDuration)
        {
            timer += Time.deltaTime;
            titleLogo.rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, timer / logoDropDuration);
            yield return null;
        }
    }
}
