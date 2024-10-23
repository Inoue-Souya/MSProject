using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScaleDown : MonoBehaviour
{
    public Transform targetImage; // �X�P�[����ύX������UI��RectTransform
    public float scaleFactor = 0.8f; // �X�P�[���̔䗦
    public float spaceHeight = 100f; // ���ɋ󂯂�X�y�[�X�̍���

    private bool flgPose = false;

    public void OnButtonClick()
    {
        if(!flgPose)
        {
            flgPose = true;
            ScaleDown();
        }
        else
        {
            flgPose = false;

            // �X�P�[��������������
            targetImage.localScale /= scaleFactor;
            // ���ɖ߂�����
            Vector3 newPosition = targetImage.localPosition;
            newPosition.y -= spaceHeight; // ��Ɉړ�
            targetImage.localPosition = newPosition;
        }
    }

    private void ScaleDown()
    {
        // �X�P�[��������������
        targetImage.localScale *= scaleFactor;

        // ���ɃX�y�[�X���󂯂邽�߂Ɉʒu��ύX
        Vector3 newPosition = targetImage.localPosition;
        newPosition.y += spaceHeight; // ��Ɉړ�
        targetImage.localPosition = newPosition;
    }
}
