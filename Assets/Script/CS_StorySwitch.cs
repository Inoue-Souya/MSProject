using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CS_StorySwitch : MonoBehaviour
{
    public UnityEngine.UI.Image[] images;
    public float slideSpeed = 5f;
    public AudioClip clickClip;
    public AudioClip bgmClip;
    public float bgmVolume = 0.5f;
    public float clickVolume = 1.0f;
    private AudioSource bgmSource;
    private AudioSource clickSource;
    private int currentIndex = 0;
    private bool isSliding = false;
    public float fadeDuration = 1f;
    private bool isFading = false;

    void Start()
    {
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
    }

    void Update()
    {
        // 左クリックが押されたとき
        if (Input.GetMouseButtonDown(0) && currentIndex < images.Length && !isSliding && !isFading)
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
            isSliding = true;
        }
    }

    void SlideCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex];
        if (currentIndex < images.Length - 1)
        {
            if (currentImage.transform.localPosition.x > -Screen.width)
            {
                currentImage.transform.localPosition += Vector3.left * slideSpeed * Time.deltaTime;
            }
            else
            {
                currentIndex++;
                isSliding = false;
            }
        }
        else
        {
            currentImage.transform.localPosition = Vector3.zero;
        }
    }

    void StartFadeOut()
    {
        isFading = true;
        Color currentColor = images[currentIndex].color;
        images[currentIndex].color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
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

        if (currentImage.color.r <= 0f && currentImage.color.g <= 0f && currentImage.color.b <= 0f)
        {
            isFading = false;
        }
    }
}
