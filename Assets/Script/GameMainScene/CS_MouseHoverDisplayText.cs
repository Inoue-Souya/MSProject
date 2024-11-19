using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_MouseHoverDisplayText : MonoBehaviour
{
    private CS_Room room;
    private CS_DragandDrop dragAndDrop;

    public CS_CameraZoom mainCamera;

    public Text sharedDisplayText;
    public RectTransform sharedPanel;
    public Vector3 offset = new Vector3(0, 1.2f, 0);
    public static bool isOtherPanelActive = false;  // 他のパネルが表示中かを示すフラグ

    private static CS_MouseHoverDisplayText currentHover;

    private void Start()
    {
        room = GetComponent<CS_Room>();
        dragAndDrop = GetComponent<CS_DragandDrop>();

        if (sharedDisplayText != null)
        {
            sharedDisplayText.gameObject.SetActive(false);
            sharedDisplayText.alignment = TextAnchor.MiddleCenter;
        }
        if (sharedPanel != null) sharedPanel.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (isOtherPanelActive) return;  // 他のパネルが表示中の場合は表示しない

        if (currentHover != null && currentHover != this)
        {
            currentHover.HideDisplay();
        }

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

    public void HideDisplay()
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
            return FormatAttributesText(dragAndDrop.characterAttributes, excludeMatchScore: true);
        }
        else if (room != null)
        {
            return FormatAttributesText(room.attributes, excludeMatchScore: false);
        }
        return "";
    }

    private string FormatAttributesText(List<RoomAttribute> attributes, bool excludeMatchScore)
    {
        string text = "";
        foreach (var attribute in attributes)
        {
            if (excludeMatchScore)
            {
                text += $"{attribute.attributeName}\n";
            }
            else
            {
                text += $"{attribute.attributeName}＋{attribute.matchScore}\n";
            }
        }
        return text.TrimEnd();
    }

    private void Update()
    {
        if (mainCamera.startPhase)// 最初の開始フラグが立ってからUI表示機能開始
        {
            if (currentHover == this && sharedDisplayText != null && sharedDisplayText.gameObject.activeSelf)
            {
                UpdatePanelAndTextPosition();
            }
        }
    }

    // 他のパネルの表示/非表示を管理するメソッド
    public static void SetOtherPanelActive(bool isActive)
    {
        isOtherPanelActive = isActive;
    }
}