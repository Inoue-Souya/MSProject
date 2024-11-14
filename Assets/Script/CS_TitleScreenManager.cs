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

    // BGM�ƌ��ʉ��p��AudioSource�t�B�[���h
    public AudioSource rainBGM;
    public AudioSource thunderSFX;

    // �A���Đ��p�̒ǉ��T�E���h�t�B�[���h
    public AudioSource ambientSound;

    public float fadeDuration = 1.0f;
    public float logoDropDuration = 2.0f;
    public float logoDisplayTime = 2.0f;

    private bool isFading = false;

    void Start()
    {
        fadeGroup.alpha = 0;
        titleLogo.gameObject.SetActive(false);

        rainBGM.loop = true;
        rainBGM.Play();  // �JBGM���Đ�

        ambientSound.loop = true;
        ambientSound.Play();  // �펞�Đ������T�E���h���Đ�
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !isFading)
        {
            rainBGM.Stop();  // �JBGM���~
            thunderSFX.Play();  // �������Đ�
            StartCoroutine(FadeToLogoAndLoadScene("StorySwitch"));
        }
    }

    IEnumerator FadeToLogoAndLoadScene(string sceneName)
    {
        isFading = true;
        titleText.gameObject.SetActive(false);

        yield return StartCoroutine(FadeIn());

        thunderSFX.Stop();  // �O�̂��ߒ�~
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

        titleLogo.gameObject.SetActive(true);  // ���S��\��

        while (timer < logoDropDuration)
        {
            timer += Time.deltaTime;
            titleLogo.rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, timer / logoDropDuration);
            yield return null;
        }
    }
}
