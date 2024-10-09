using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CS_RoomManager : MonoBehaviour
{
    //public CS_RoomManager Instance { get; private set; }

    private float ICost;          //���z��p
    public float currentRoom;     // ���݂̍ő啔����
    public float EmptyRoom; //�󂫕����̐�
    public Text RoomText; // UI��Text�R���|�[�l���g
    public float Cost;    //�ێ���̕ϐ�
    public CS_MoneyManager MoneyManager; //�����̊Ǘ��ϐ�
    public Button MButton; //�����{�^��
    public Button IButton; //���z�{�^��
    //public Button LButton; //�ދ��{�^��

    // Start is called before the first frame update
    void Start()
    {

        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject); // �V�[�����ׂ��ł������Ȃ��悤�ɂ���
        //}
        //else
        //{
        //    Destroy(gameObject); // ���ɑ��݂���ꍇ�͔j��
        //}

        currentRoom = 5f; // ���݂̍ő啔����

        EmptyRoom = currentRoom;  //�󂫕����̐�

        Cost = 8000;                //�ێ���̕ϐ�

        ICost = 1000f;               //���z��p

        RoomText.text = $"����: {EmptyRoom}";
    }

    
    public void Movein()    //�������̏���
    {
        EmptyRoom -= 1;
        UpdateRoomText();
    }

    ///*public void Leaving()�@�@//�ދ����̏���
    //{
    //    EmptyRoom += 1;
    //    MoneyManager.AddMoney(1000);      //�ދ����ɑ����邨��
    //    UpdateRoomText();
    //}*/


    public void Extension(float amount)�@//���z���̏���
    {
        currentRoom += amount;
        EmptyRoom += amount;
        MoneyManager.DecreaseMoney(ICost);  //���z��p
        Cost += 1000;                      //�ێ���p
        UpdateRoomText();
    }

    private void UpdateRoomText()�@�@�@�@// �󂫕�������\��
    {
        if (RoomText != null)
        {
            RoomText.text = $"����: {EmptyRoom}"; 
        }
    }

    private void Update()
    {
        // �����{�^���̗L����/������
        if (MButton != null)
        {
            if (EmptyRoom <= 0)
            {
                Button bt = MButton.GetComponent<Button>();
                bt.interactable = false;
            }
            else
            {
                Button bt = MButton.GetComponent<Button>();
                bt.interactable = true;
            }
        }

        // �����{�^���̗L����/������
        if (IButton != null)
        {
            if (MoneyManager.GetMoney() < ICost)
            {
                Button bt = IButton.GetComponent<Button>();
                bt.interactable = false;
            }
            else
            {
                Button bt = IButton.GetComponent<Button>();
                bt.interactable = true;
            }
        }
        // �ދ��{�^���̗L����/������
        //if (LButton != null)
        //{
        //    if (EmptyRoom == currentRoom)
        //    {
        //        Button bt = LButton.GetComponent<Button>();
        //        bt.interactable = false;
        //    }
        //    else
        //    {
        //        Button bt = LButton.GetComponent<Button>();
        //        bt.interactable = true;
        //    }
        //}
        //else
        //{
        //    Debug.LogError("LButton is not assigned in the Inspector.");
        //}
    }
}
