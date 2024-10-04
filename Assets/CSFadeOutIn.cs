using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSFadeOutIn : MonoBehaviour
{
    public CanvasGroup canvasGroup; // �t�F�[�h����I�u�W�F�N�g��CanvasGroup
    public CanvasGroup textGroup;
    public float fadeDuration = 1.0f; // �t�F�[�h�ɂ����鎞��

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartFadeOutIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOutIn());
    }

    private IEnumerator FadeOutIn()
    {
        // �t�F�[�h�C���A�E�g�J�n

        // �t�F�[�h�A�E�g
        yield return StartCoroutine(Fade(0, 1, canvasGroup));

        yield return StartCoroutine(Fade(0, 1, textGroup));
        // �t�F�[�h�A�E�g��̑ҋ@���ԁi�K�v�Ȃ�j
        yield return new WaitForSeconds(1.0f);

        // �t�F�[�h�C��
        yield return StartCoroutine(Fade(1, 0, textGroup));

        yield return StartCoroutine(Fade(1, 0, canvasGroup));

        // �t�F�[�h�C���A�E�g�I������
        gameObject.SetActive(false);
    }

    // �t�F�[�h�����ifrom:�J�nalpha, to:�I��alpha, �t�F�[�h����UI�j
    private IEnumerator Fade(float from, float to, CanvasGroup group)
    {
        float elapsed = 0.0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            group.alpha = alpha;
            yield return null;
        }

        // �ŏI�I��alpha�l��ݒ�
        group.alpha = to;
    }
}
