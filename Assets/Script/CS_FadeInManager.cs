using System.Collections;
using UnityEngine;

public class CS_FadeInManager : MonoBehaviour
{
    public CanvasGroup fadeGroup;  // �t�F�[�h�p�� CanvasGroup
    public float fadeDuration = 1.5f;  // �t�F�[�h�A�E�g�̎���

    void Start()
    {
        // �V�[���J�n���Ƀt�F�[�h�A�E�g�����s
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;

        // �t�F�[�h�A�E�g����
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        // �Ō�Ɋ��S�ɓ����ɂ���
        fadeGroup.alpha = 0;
        fadeGroup.gameObject.SetActive(false);  // ���S�Ƀt�F�[�h�A�E�g��A�I�u�W�F�N�g�𖳌���
    }
}
