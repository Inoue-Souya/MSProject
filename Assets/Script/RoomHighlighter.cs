using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outline;  // �g���p�̃R���|�[�l���g

    void Start()
    {
        // Outline�R���|�[�l���g���擾
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;  // ������Ԃł͔�\��
    }

    // �}�E�X�������ɏ�����Ƃ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (outline != null)
            outline.enabled = true;  // �g����\��
    }

    // �}�E�X�����ꂽ�Ƃ�
    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null)
            outline.enabled = false;  // �g�����\��
    }
}
