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
    public static bool isOtherPanelActive = false;  // ���̃p�l�����\�������������t���O
    public Image MoneyImage;
    public Image hoverImage;  // �C���X�y�N�^�[�ŃA�^�b�`�\�ȉ摜
    public Vector3 imageOffset = new Vector3(0, -1.2f, 0);  // �摜�̕\���ʒu�I�t�Z�b�g
    public Text additionalText;  // �V�����e�L�X�g���C���X�y�N�^�[�Őݒ�
    public string additionalTextContent = ""; // �C���X�y�N�^�[�Őݒ�\�ȃe�L�X�g
    public Vector3 additionalTextOffset = new Vector3(1.0f, -0.5f, 0);  // �V�����e�L�X�g�̃I�t�Z�b�g

    private static CS_MouseHoverDisplayText currentHover;

    private void Start()
    {
        room = GetComponent<CS_Room>();
        dragAndDrop = GetComponent<CS_DragandDrop>();

        // UI�̔�\���@�\�Ăяo��
        HideDisplay();
    }

    private void OnMouseEnter()
    {
        if (isOtherPanelActive)
        {
            Debug.Log("���̃p�l�����A�N�e�B�u�Ȃ̂ŕ\�����܂���B");
            return;
        }

        // ���݂� hover �������ȊO�̏ꍇ�A�ȑO�� hover ���\���ɂ���
        if (currentHover != null && currentHover != this)
        {
            currentHover.HideDisplay();
        }

        currentHover = this;

        // Additional Text ��ݒ肵�Ē����ɕ\��
        if (additionalText != null)
        {
            additionalText.text = additionalTextContent; // �R���e���c��ݒ�
            additionalText.fontSize = 12;
            additionalText.gameObject.SetActive(true);
            UpdateAdditionalTextPosition(); // Hover Image �̒����ɔz�u
        }

        // ����UI�i�K�v�ɉ����ď����j
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
                Debug.Log("������Ă��Ȃ������ł�");
                if (additionalText != null && MoneyImage != null)
                {
                    additionalText.text = "�~" + room.unlockCost;
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

            // ������Ă��镔���ɂ͂���Ȃ�
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
            hoverImage.gameObject.SetActive(false);  // �摜���\��
        }

        if (additionalText != null)
        {
            additionalText.gameObject.SetActive(false);  // �V�����e�L�X�g���\��
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
            // Hover Image �̒��S���W���擾
            Vector3 hoverImageCenter = hoverImage.rectTransform.position;

            // Hover Image �̃T�C�Y���璆�����v�Z
            Vector3 adjustedPosition = hoverImageCenter;

            // Additional Text �̈ʒu�𒲐�
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
        if (Input.GetMouseButton(0)) // �h���b�O���͕\�����I�t�ɂ���
        {
            HideDisplay();
        }
        else
        {
            if (currentHover == this)
            {
                UpdatePanelAndTextPosition();

                if (hoverImage != null) // hoverImage��null�łȂ��ꍇ�̂݌Ăяo��
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
            return "�����f�[�^���ݒ肳��Ă��܂���B";
        }

        string result = "";
        foreach (var attribute in attributes)
        {
            result += $"{attribute.attributeName}: {attribute.matchScore}\n";
        }

        return result.TrimEnd(); // �Ō�̉��s���폜
    }


    private void AdjustAdditionalTextSize()
    {
        if (additionalText != null)
        {
            // �e�L�X�g�̃T�C�Y����e�ɍ��킹�Ē���
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
            // Best Fit�𖳌���
            additionalText.resizeTextForBestFit = false;

            // �K�؂ȃt�H���g�T�C�Y��ݒ�
            additionalText.fontSize = 24; // �K�v�ɉ����Ē���
        }
    }



    // ���̃p�l���̕\��/��\�����Ǘ����郁�\�b�h
    public static void SetOtherPanelActive(bool isActive)
    {
        isOtherPanelActive = isActive;
    }
}