using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms; // �V�[�����̕����̔z��

    void Start()
    {
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
}
