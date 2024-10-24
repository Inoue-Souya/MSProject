using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms; // シーン内の部屋の配列

    void Start()
    {
        InitializeRooms();
    }

    private void InitializeRooms()
    {
        // 解放済みの部屋を設定する
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i < 5) // 最初の5部屋を解放済みにする
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
