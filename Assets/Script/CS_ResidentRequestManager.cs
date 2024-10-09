using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_ResidentRequestManager : MonoBehaviour
{
    public Text requestText; // �_�C�A���O�ɕ\������e�L�X�g
    public Button nextButton; // ���̗v�]�{�^��
    private ResidentRequest[] requests;
    private int currentIndex = 0;

    void Start()
    {
        // �T���v���̗v�]���쐬
        requests = new ResidentRequest[]
        {
            new ResidentRequest("�P", 25, "�j"),
            new ResidentRequest("��", 28, "�j"),
            new ResidentRequest("�P", 30, "��")
        };

        // �ŏ��̗v�]��\��
        DisplayRequest(currentIndex);

        // �{�^���̃N���b�N�C�x���g��ݒ�
        nextButton.onClick.AddListener(NextRequest);
    }

    private void DisplayRequest(int index)
    {
        ResidentRequest request = requests[index];
        requestText.text = $"���i: {request.personality}\n�N��: {request.age}\n����: {request.gender}";
    }

    private void NextRequest()
    {
        currentIndex++;
        if (currentIndex >= requests.Length)
        {
            currentIndex = 0; // �ŏ��ɖ߂�
        }
        DisplayRequest(currentIndex);
    }
}
