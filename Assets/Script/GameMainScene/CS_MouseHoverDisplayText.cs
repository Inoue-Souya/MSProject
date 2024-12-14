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
    public static bool isOtherPanelActive = false;  // 他のパネルが表示中かを示すフラグ
    public Image MoneyImage;
    public Image hoverImage;  // インスペクターでアタッチ可能な画像
    public Vector3 imageOffset = new Vector3(0, -1.2f, 0);  // 画像の表示位置オフセット
    public Text additionalText;  // 新しいテキストをインスペクターで設定
    public string additionalTextContent = ""; // インスペクターで設定可能なテキスト
    public Vector3 additionalTextOffset = new Vector3(1.0f, -0.5f, 0);  // 新しいテキストのオフセット

    private static CS_MouseHoverDisplayText currentHover;

    private void Start()
    {
        room = GetComponent<CS_Room>();
        dragAndDrop = GetComponent<CS_DragandDrop>();

        // UIの非表示機能呼び出し
        HideDisplay();
    }

    private void OnMouseEnter()
    {
        if (isOtherPanelActive)
        {
            Debug.Log("他のパネルがアクティブなので表示しません。");
            return;
        }

        // 現在の hover が自分以外の場合、以前の hover を非表示にする
        if (currentHover != null && currentHover != this)
        {
            currentHover.HideDisplay();
        }

        currentHover = this;

        // Additional Text を設定して中央に表示
        if (additionalText != null)
        {
            additionalText.text = additionalTextContent; // コンテンツを設定
            additionalText.fontSize = 12;
            additionalText.gameObject.SetActive(true);
            UpdateAdditionalTextPosition(); // Hover Image の中央に配置
        }

        // 他のUI（必要に応じて処理）
        if (hoverImage != null)
        {
            hoverImage.gameObject.SetActive(true);
            Image Image = hoverImage.GetComponent<Image>();
            Color color = Image.color;
            color.a = 1.0f;
        }

        if (gameObject.tag == "Room")
        {
            if (!room.isUnlocked)
            {
                Debug.Log("解放していない部屋です");
                if (additionalText != null && MoneyImage != null)
                {
                    additionalText.text = "×" + room.unlockCost;
                    additionalText.alignment = TextAnchor.MiddleCenter;
                    additionalText.fontSize = 24;
                    MoneyImage.gameObject.SetActive(true);
                    Image Image = hoverImage.GetComponent<Image>();
                    Color color = Image.color;
                    color.a = 0.0f;
                    UpdateAdditionalTextPosition();
                    return;
                }
            }

            // 解放している部屋にはいらない
            hoverImage.gameObject.SetActive(false);
            additionalText.gameObject.SetActive(false);
        }

        if (sharedDisplayText != null && sharedPanel != null && room.isResidents)
        {
            sharedDisplayText.text = GetAttributesText();
            sharedDisplayText.alignment = TextAnchor.MiddleCenter;
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

        if (hoverImage != null)
        {
            hoverImage.gameObject.SetActive(false);  // 画像を非表示
        }

        if (additionalText != null)
        {
            additionalText.gameObject.SetActive(false);  // 新しいテキストを非表示
        }

        if (MoneyImage != null)
        {
            MoneyImage.gameObject.SetActive(false);
        }
    }

    private void UpdatePanelAndTextPosition()
    {
        Vector3 displayPosition = transform.position + offset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(displayPosition);

        sharedDisplayText.transform.position = screenPosition;
        sharedPanel.position = screenPosition;
    }

    private void UpdateImagePosition()
    {
        Vector3 imagePosition = transform.position + imageOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(imagePosition);

        hoverImage.rectTransform.position = screenPosition;
    }

    private void UpdateAdditionalTextPosition()
    {
        if (hoverImage != null && additionalText != null)
        {
            // Hover Image の中心座標を取得
            Vector3 hoverImageCenter = hoverImage.rectTransform.position;

            // Hover Image のサイズから中央を計算
            Vector3 adjustedPosition = hoverImageCenter;

            // Additional Text の位置を調整
            additionalText.rectTransform.position = adjustedPosition;
            //additionalText.color = new Color(255, 255, 255);

            // Debug.Log($"Hover Image Center: {hoverImageCenter}, Adjusted Position: {adjustedPosition}");
        }
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
                text += $"{attribute.attributeName}\n";
            }
        }
        return text.TrimEnd();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // ドラッグ中は表示をオフにする
        {
            HideDisplay();
        }
        else
        {
            if (currentHover == this)
            {
                UpdatePanelAndTextPosition();

                if (hoverImage != null) // hoverImageがnullでない場合のみ呼び出し
                {
                    UpdateImagePosition();
                }

                if (additionalText != null && additionalText.gameObject.activeSelf)
                {
                    UpdateAdditionalTextPosition();
                }
                
            }
        }
    }

    private string FormatRoomAttributesText(List<RoomAttribute> attributes)
    {
        if (attributes == null || attributes.Count == 0)
        {
            return "属性データが設定されていません。";
        }

        string result = "";
        foreach (var attribute in attributes)
        {
            result += $"{attribute.attributeName}: {attribute.matchScore}\n";
        }

        return result.TrimEnd(); // 最後の改行を削除
    }


    private void AdjustAdditionalTextSize()
    {
        if (additionalText != null)
        {
            // テキストのサイズを内容に合わせて調整
            ContentSizeFitter sizeFitter = additionalText.GetComponent<ContentSizeFitter>();
            if (sizeFitter == null)
            {
                sizeFitter = additionalText.gameObject.AddComponent<ContentSizeFitter>();
            }
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }

    private void ConfigureTextSettings()
    {
        if (additionalText != null)
        {
            // Best Fitを無効化
            additionalText.resizeTextForBestFit = false;

            // 適切なフォントサイズを設定
            additionalText.fontSize = 24; // 必要に応じて調整
        }
    }



    // 他のパネルの表示/非表示を管理するメソッド
    public static void SetOtherPanelActive(bool isActive)
    {
        isOtherPanelActive = isActive;
    }
}