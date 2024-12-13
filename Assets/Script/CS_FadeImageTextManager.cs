using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CS_FadeImageTextManager : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // �t�F�[�h�p�̍����摜
    public GameObject[] imagesToShow; // �\������摜�̔z��
    public UnityEngine.UI.Text[] textsToShow; // �\������e�L�X�g�̔z��
    public int maxImageClickCount = 1; // �摜����\���ɂȂ�܂ł̃N���b�N��
    public int maxTextClickCount = 1; // �e�L�X�g����\���ɂȂ�܂ł̃N���b�N��

    private int imageClickCount = 0;
    private int textClickCount = 0;
    private bool isFading = false;

    void Start()
    {
        // �����ݒ�: �摜�ƃe�L�X�g���\����
        foreach (var image in imagesToShow)
        {
            image.SetActive(false);
        }

        foreach (var text in textsToShow)
        {
            text.gameObject.SetActive(false);
        }

        // �t�F�[�h���J�n
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
            fadeColor.a -= Time.deltaTime / 2f; // 2�b�Ńt�F�[�h
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0.5f;
        fadeImage.color = fadeColor;
        fadeImage.gameObject.SetActive(false); // �t�F�[�h�p�摜���\��
        isFading = false;

        // �摜�ƃe�L�X�g��\��
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
        // �}�E�X�N���b�N�̏���
        if (!isFading && Input.GetMouseButtonDown(0))
        {
            if (imageClickCount < maxImageClickCount)
            {
                imageClickCount++;

                if (imageClickCount >= maxImageClickCount)
                {
                    // �摜���\����
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
                    // �e�L�X�g���\����
                    foreach (var text in textsToShow)
                    {
                        text.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
