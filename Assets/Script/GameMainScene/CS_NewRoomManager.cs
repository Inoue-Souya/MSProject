using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms; // ƒV[ƒ““à‚Ì•”‰®‚Ì”z—ñ

    void Start()
    {
        InitializeRooms();
    }

    private void InitializeRooms()
    {
        // ‰ğ•úÏ‚İ‚Ì•”‰®‚ğİ’è‚·‚é
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i < 5) // Å‰‚Ì5•”‰®‚ğ‰ğ•úÏ‚İ‚É‚·‚é
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
