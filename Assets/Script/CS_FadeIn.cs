using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class CS_FadeIn : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage;  // �t�F�[�h�p��Image�R���|�[�l���g
    public float fadeDuration = 2f;  // �t�F�[�h�C���ɂ����鎞��
    public bool fadeFinish;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeImage == null)
        {
            // Iyage���ݒ肳��Ă��Ȃ��ꍇ�̓G���[���b�Z�[�W��\��
            UnityEngine.Debug.Log("Fade image is not assigned!");
            return;
        }

        fadeFinish = false;

        // �t�F�[�h�C�����J�n
        StartCoroutine(FadeInCoroutine());
    }

    // �t�F�[�h�C���̃R���[�`��
    IEnumerator FadeInCoroutine()
    {
        float timeElapsed = 0f;

        // ���݂̃A���t�@�l���擾�i�����l��1.0f�i���S�ɕs�����j�ɐݒ肳��Ă���͂��j
        Color startColor = fadeImage.color;
        startColor.a = 1f;
        fadeImage.color = startColor;

        // �t�F�[�h�C���̏���
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;  // ���̃t���[���܂őҋ@
        }

        // �t�F�[�h�C���������ɓ����x�����S��0�ɂ���
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        fadeImage.gameObject.SetActive(false);
        fadeFinish = true;
    }
}
