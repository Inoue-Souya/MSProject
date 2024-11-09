using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms; // �V�[�����̕����̔z��
    public int stopRooms;

    void Start()
    {
        stopRooms = 0;
        InitializeRooms();
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
        if(stopRooms > 2)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
