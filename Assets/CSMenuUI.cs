using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // �K�v�Ȗ��O��Ԃ�ǉ�

public class CSMenuUI : MonoBehaviour
{
    public GameObject contextMenu; // �R���e�L�X�g���j���[�p��UI�p�l��

    private RectTransform panelRect;

    private void Start()
    {
        contextMenu.SetActive(false);
    }

    private void Update()
    {
        // �E�N���b�N�����m
        if (Input.GetMouseButtonDown(1))
        {
            // �}�E�X�̈ʒu���擾
            Vector2 mousePosition = Input.mousePosition;

            // �p�l����\������
            contextMenu.SetActive(true);

            // �p�l���̃T�C�Y���擾
            RectTransform panelRect = contextMenu.GetComponent<RectTransform>();
            Vector2 panelSize = panelRect.sizeDelta;

            // ��ʂ̃T�C�Y���擾
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // �p�l���̍��オ�}�E�X�ʒu�Ɉ�v����悤�ɐݒ�
            float xPos = mousePosition.x;
            float yPos = mousePosition.y; 

            // �E�ɂ͂ݏo���ꍇ�͈ʒu�𒲐�
            if (xPos + panelSize.x > screenWidth)
            {
                xPos = screenWidth - panelSize.x; // �p�l���̕����������ʒu�ɐݒ�
            }

            // ���ɂ͂ݏo���ꍇ�̒���
            if (yPos - panelSize.y < 0)
            {
                yPos = panelSize.y; // ���[�����킹��
            }

            // �p�l���̈ʒu��ݒ�i�L�����o�X���̃��[�J�����W�n�ɕϊ��j
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect.parent as RectTransform, new Vector2(xPos, yPos), null, out localPoint);
            panelRect.anchoredPosition = localPoint; // ������̈ʒu��ݒ�

            // �p�l���̍���̈ʒu�����킹�邽�߁A����ɒ���
            panelRect.anchoredPosition += new Vector2(panelSize.x / 2, -panelSize.y / 2); // Y�������Ƀp�l���̍�����ǉ�
        }

        // ���N���b�N�Ń��j���[�����
        if (Input.GetMouseButtonDown(0))
        {
            // �N���b�N�����I�u�W�F�N�g��UI�v�f�i�{�^���Ȃǁj���ǂ������m�F
            if (!IsPointerOverUIElement())
            {
                // UI�v�f�i�{�^���Ȃǁj�ȊO���N���b�N���ꂽ�ꍇ�Ƀ��j���[�����
                contextMenu.SetActive(false);
            }
        }

        // ESC�L�[�Ń��j���[�����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            contextMenu.SetActive(false);
        }
    }

    // �N���b�N���ꂽ�ꏊ��UI�v�f���ǂ����𔻒肷�郁�\�b�h
    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // �N���b�N�����ꏊ��UI�v�f�����邩�ǂ����𔻒�
        return results.Count > 0;
    }
}
