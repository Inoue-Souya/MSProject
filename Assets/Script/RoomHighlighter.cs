using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outline;  // 枠線用のコンポーネント

    void Start()
    {
        // Outlineコンポーネントを取得
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;  // 初期状態では非表示
    }

    // マウスが部屋に乗ったとき
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (outline != null)
            outline.enabled = true;  // 枠線を表示
    }

    // マウスが離れたとき
    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null)
            outline.enabled = false;  // 枠線を非表示
    }
}
