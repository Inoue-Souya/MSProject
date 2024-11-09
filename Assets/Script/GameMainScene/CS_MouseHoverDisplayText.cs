using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_MouseHoverDisplayText : MonoBehaviour
{
    private CS_Room room;
    private CS_DragandDrop dragAndDrop;

    public Text sharedDisplayText;
    public RectTransform sharedPanel;
    public Vector3 offset = new Vector3(0, 1.2f, 0);

    private static CS_MouseHoverDisplayText currentHover; // 現在表示されているオブジェクトの参照

    private void Start()
    {
        room = GetComponent<CS_Room>();
        dragAndDrop = GetComponent<CS_DragandDrop>();

        if (sharedDisplayText != null) sharedDisplayText.gameObject.SetActive(false);
        if (sharedPanel != null) sharedPanel.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (currentHover != null && currentHover != this)
        {
            currentHover.HideDisplay();  // 他のオブジェクトの表示を消す
        }

        // 現在のオブジェクトをcurrentHoverとして設定
        currentHover = this;

        if (sharedDisplayText != null && sharedPanel != null)
        {
            sharedDisplayText.text = GetAttributesText();
            sharedDisplayText.gameObject.SetActive(true);
            sharedPanel.gameObject.SetActive(true);

            UpdatePanelAndTextPosition();
        }
    }

    private void OnMouseExit()
    {
        if (currentHover == this)
        {
            HideDisplay();
            currentHover = null;
        }
    }

    private void HideDisplay()
    {
        if (sharedDisplayText != null && sharedPanel != null)
        {
            sharedDisplayText.gameObject.SetActive(false);
            sharedPanel.gameObject.SetActive(false);
        }
    }

    private void UpdatePanelAndTextPosition()
    {
        Vector3 displayPosition = transform.position + offset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(displayPosition);

        sharedDisplayText.transform.position = screenPosition;
        sharedPanel.position = screenPosition;
    }

    private string GetAttributesText()
    {
        if (gameObject.CompareTag("yo-kai") && dragAndDrop != null)
        {
            return FormatAttributesText(dragAndDrop.characterAttributes);
        }
        else if (room != null)
        {
            return FormatAttributesText(room.attributes);
        }
        return "";
    }

    private string FormatAttributesText(List<RoomAttribute> attributes)
    {
        string text = "";
        foreach (var attribute in attributes)
        {
            text += $"{attribute.attributeName}: {attribute.matchScore}\n";
        }
        return text.TrimEnd();
    }

    private void Update()
    {
        if (currentHover == this && sharedDisplayText != null && sharedDisplayText.gameObject.activeSelf)
        {
            UpdatePanelAndTextPosition();
        }
    }
}