using UnityEngine;
using System.Collections;

public class FadeInButton : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // �t�F�[�h�C���ɂ����鎞��
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError($"CanvasGroup �R���|�[�l���g���{�^�� '{gameObject.name}' �ɃA�^�b�`����Ă��܂���B");
            return;
        }

        Debug.Log($"CanvasGroup ���������ݒ肳��Ă��܂�: {gameObject.name}");
        canvasGroup.alpha = 0f; // �ŏ��͓���
    }

    public void FadeIn()
    {
        if (canvasGroup != null)
        {
            Debug.Log($"FadeIn �J�n: {gameObject.name}");
            elapsedTime = 0f;
            StartCoroutine(FadeInCoroutine());
        }
        else
        {
            Debug.LogError($"FadeIn ���J�n�ł��܂���BCanvasGroup ���ݒ肳��Ă��܂���: {gameObject.name}");
        }
    }

    private IEnumerator FadeInCoroutine()
    {
        if (canvasGroup == null) yield break;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f; // ���S�ɕ\�����
        Debug.Log($"FadeIn ����: {gameObject.name}");
    }
}
