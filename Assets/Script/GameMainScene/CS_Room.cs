using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CS_Room : MonoBehaviour
{
    public List<RoomAttribute> attributes; // 部屋の特性リスト
    public CS_ScoreDisplay scoreDisplay; // ScoreDisplayへの参照
    public int unlockCost = 10; // 部屋を解放するためのスコアコスト
    public bool isUnlocked = false; // 部屋が解放されているか

    private int totalScore;

    // 新しいメソッドを追加
    public void InitializeRoom(bool unlockStatus)
    {
        isUnlocked = unlockStatus;
    }

    private void Start()
    {
        totalScore = 0;
    }


    public void AddResident(CS_DragandDrop character)
    {
        if (!isUnlocked)
        {
            Debug.Log("This room is locked.");
            return; // 部屋が解放されていない場合は何もしない
        }

        // キャラクターの特性とマッチするスコアを計算
        foreach (var roomAttribute in attributes)
        {
            foreach (var characterAttribute in character.characterAttributes)
            {
                if (roomAttribute.attributeName == characterAttribute.attributeName)
                {
                    totalScore += roomAttribute.matchScore * 100; // マッチした場合スコアを加算
                }
            }
        }

        Debug.Log($"{character.name} matched with room {gameObject.name}, score: {totalScore}");

        // スコアを表示する
        if (scoreDisplay != null)
        {
            scoreDisplay.UpdateScore(totalScore);
        }

    }
    public void UnlockRoom(int score)
    {
        if (score >= unlockCost)
        {
            isUnlocked = true;
            Debug.Log($"{gameObject.name} is now unlocked!");
            // スコアからコストを引く処理を追加
        }
        else
        {
            Debug.Log("Not enough score to unlock the room.");
        }
    }
}