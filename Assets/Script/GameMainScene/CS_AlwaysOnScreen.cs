using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AlwaysOnScreen : MonoBehaviour
{
    public GameObject targetObject;  // ��ɉ�ʉ����ɕ\���������I�u�W�F�N�g
    public RectTransform uiElement;  // UI�v�f��RectTransform (Canvas��)
    public Vector3 ikonPosition;

    void Update()
    {
        // ��ʂ̕��ƍ������擾
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ��ʉ����̃X�N���[�����W (y���͍ŏ��l)
        Vector3 screenPos = new Vector3(screenWidth / 2, 0, 0);  // X�͉�ʒ����AY�͉���

        // �X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // Y���͉�ʉ����ɌŒ肵�AX���͌��݂̈ʒu��ێ�
        worldPos.y = Camera.main.ViewportToWorldPoint(new Vector3(0,0.1f,0)).y;  // ��ʂ̉�����Y�ʒu

        worldPos.x = ikonPosition.x;

        // Z���̈ʒu��ێ��i2D�Ȃ̂�Z���͕ς��Ȃ��j
        worldPos.z = targetObject.transform.position.z;

        // �I�u�W�F�N�g�̈ʒu���X�V
        targetObject.transform.position = worldPos;

        // UI�v�f�̏ꍇ�ARectTransform���g���Đݒ肷��
        if (uiElement != null)
        {
            Vector3 uiWorldPos = Camera.main.WorldToScreenPoint(worldPos); // ���[���h���W���X�N���[�����W��
            uiElement.position = uiWorldPos;
        }
    }
}
