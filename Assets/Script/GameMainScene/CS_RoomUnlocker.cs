using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_RoomUnlocker : MonoBehaviour
{
    public CS_Room room; // ������镔��
    public CS_ScoreManager scoreManager; // �X�R�A�}�l�[�W���[�ւ̎Q��

    public void TryUnlockRoom()
    {
        if (scoreManager.SpendScore(room.unlockCost))
        {
            room.UnlockRoom(room.unlockCost);
        }
        else
        {
            Debug.Log("Not enough score to unlock this room.");
        }
    }
}
