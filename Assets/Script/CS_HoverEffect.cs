using UnityEngine;
using UnityEngine.EventSystems;

public class CS_HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.0f); // �z�o�[���̃X�P�[��
    public float scaleDuration = 0.2f;  // �X�P�[���ύX�ɂ����鎞��

    private Vector3 originalScale;  // ���̃X�P�[��
    private bool isHovered = false;

    void Start()
    {
        // �����X�P�[����ۑ�
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �z�o�[�����Ƃ��̏���
        isHovered = true;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �z�o�[���O�����Ƃ��̏���
        isHovered = false;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 currentScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(currentScale, targetScale, elapsedTime / scaleDuration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
