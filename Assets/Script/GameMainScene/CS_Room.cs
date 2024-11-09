using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CS_Room : MonoBehaviour
{
    public List<RoomAttribute> attributes; // 部屋の特性リスト
    public CS_ScoreManager scoreManager;
    public CS_NewRoomManager roomManager;
    //public CS_ScoreDisplay scoreDisplay; // ScoreDisplayへの参照
    public int unlockCost = 10; // 部屋を解放するためのスコアコスト
    public bool isUnlocked = false; // 部屋が解放されているか
    private bool inRoomflag;
    private int cp_score;

    [SerializeField]
    private float roomHP;

    private int totalScore;
    private float elapsedTime = 0f;

    // 新しいメソッドを追加
    public void InitializeRoom(bool unlockStatus)
    {
        isUnlocked = unlockStatus;
    }

    private void Start()
    {
        // 初期スコア設定
        scoreManager.Init();
        roomHP = 100.0f;
        inRoomflag = false;
    }

    private void Update()
    {
        if (inRoomflag)
        {
            // Increase elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Calculate the HP decrease rate
            float hpDecreaseRate = cp_score / 5f; // cp_score reduced over 5 seconds

            // Decrease roomHP gradually
            roomHP -= hpDecreaseRate * Time.deltaTime;

            // After 5 seconds, stop decreasing and reset the flag
            if (elapsedTime >= 5f)
            {
                inRoomflag = false;
                elapsedTime = 0f; // Reset timer
            }
        }

        if(roomHP <= 0f)
        {
            roomManager.stopRooms++;
            Destroy(this);
        }
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
                    // 値を代入するのみ
                    cp_score = roomAttribute.matchScore;
                    Debug.Log("matchScore:" + roomAttribute.matchScore);
                    totalScore = roomAttribute.matchScore * 100; // マッチした場合スコアを加算
                }
            }
        }
        Debug.Log($"{character.name} matched with room {gameObject.name}, score: {totalScore}");
    }

    public void finishPhase()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(totalScore);
        }
    }

    public void setinRoomflag(bool room)
    {
        inRoomflag = room;
    }

    public bool GettinRoom()
    {
        return inRoomflag;
    }
}