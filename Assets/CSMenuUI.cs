using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSMenuUI : MonoBehaviour
{
    public GameObject contextMenu; // �R���e�L�X�g���j���[�p��UI�p�l��

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

        // ���N���b�N�܂���ESC�L�[�Ń��j���[�����
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            contextMenu.SetActive(false);
        }
    }
}
