using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms; // シーン内の部屋の配列
    public int inResident;  //ゲームオーバー用変数
    public int openRoom;    //　解放済みの部屋数

    void Start()
    {
        //InitializeRooms();
        inResident = 0;
        openRoom = 5;
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

    private void Update()
    {
        //gameObject.SetActive(true);
        //if (inResident >= openRoom)
        //{
        //    SceneManager.LoadScene("GameOverScene");
        //}
    }
}
