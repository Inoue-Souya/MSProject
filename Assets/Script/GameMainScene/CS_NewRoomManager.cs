using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms; // �V�[�����̕����̔z��
    public int inResident;  //�Q�[���I�[�o�[�p�ϐ�
    public int openRoom;    //�@����ς݂̕�����

    void Start()
    {
        //InitializeRooms();
        inResident = 0;
        openRoom = 5;
    }

    private void InitializeRooms()
    {
        // ����ς݂̕�����ݒ肷��
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i < 5) // �ŏ���5����������ς݂ɂ���
            {
                rooms[i].InitializeRoom(true);
            }
            else
            {
                rooms[i].InitializeRoom(false);
            }
        }
    }

    private void Update()
    {
        //gameObject.SetActive(true);
        //if (inResident >= openRoom)
        //{
        //    SceneManager.LoadScene("GameOverScene");
        //}
    }
}
