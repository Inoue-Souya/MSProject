using UnityEngine;

public class CS_FadeInButton : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // �t�F�[�h�C���ɂ����鎞��
    private CanvasGroup canvasGroup;
    private float elapsedTime = 0f;

    void Start()
    {
        // �{�^���� CanvasGroup ���A�^�b�`����Ă��邱�Ƃ��m�F
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup �R���|�[�l���g���{�^���ɃA�^�b�`����Ă��܂���B");
            return;
        }

        // �ŏ��͓����ɐݒ�
        canvasGroup.alpha = 0f;
    }

    public void FadeIn()
    {
        // �t�F�[�h�C�����J�n����
        StartCoroutine(FadeInCoroutine());
    }

    private System.Collections.IEnumerator FadeInCoroutine()
    {
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // ���X�ɃA���t�@�l�𑝉�
            yield return null;
        }
    }
}
