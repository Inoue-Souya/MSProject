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
    private float DurationTime;// 部屋の占有時間を保存する変数

    [SerializeField]
    private float roomHP;

    private int totalScore;
    private float elapsedTime = 0f;

    public GameObject childObject;      // 子オブジェクトの参照
    public SpriteRenderer childSpriteRenderer;      // 子オブジェクトのスプライトRenderer
    public Sprite oldSprite;        // 変更前のスプライト
    public Sprite newSprite;        // 変更後のスプライト

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
            float hpDecreaseRate = cp_score / DurationTime; // cp_score reduced over 5 seconds

            // Decrease roomHP gradually
            roomHP -= hpDecreaseRate * Time.deltaTime;

            // After 5 seconds, stop decreasing and reset the flag
            if (elapsedTime >= DurationTime)
            {
                inRoomflag = false;
                elapsedTime = 0f; // Reset timer
            }
        }

        if(roomHP <= 0f)
        {
            // 限界部屋数を増やす
            roomManager.stopRooms++;

            // 部屋の当たり判定を消去する
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(collider);

            // 一度だけ実行したいので
            // roomHPを1以上にして通らないようにする
            roomHP = 1;
        }

        // IsUnlocked が true であれば、子オブジェクトをアクティブにする
        if (isUnlocked)
        {
            if (childObject != null)
            {
                childObject.SetActive(true); // 子オブジェクトをアクティブにする
            }
        }
        else
        {
            if (childObject != null)
            {
                childObject.SetActive(false); // IsUnlocked が false なら、子オブジェクトを非アクティブにする
            }
        }

        if (inRoomflag && childSpriteRenderer != null && newSprite != null)
        {
            childSpriteRenderer.sprite = newSprite;
        }
        else
        {
            childSpriteRenderer.sprite = oldSprite;
        }

    }

    public void AddResident(CS_DragandDrop character, float Duration)
    {
        if (!isUnlocked)
        {
            Debug.Log("This room is locked.");
            return; // 部屋が解放されていない場合は何もしない
        }

        // 初期化しておく
        cp_score = 0; // 新しい住民を追加するたびにスコアをリセットする（累積するため）
        totalScore = 0;

        // 妖怪の部屋占有時間を記録
        DurationTime = Duration;

        // キャラクターの特性とマッチするスコアを計算
        foreach (var roomAttribute in attributes)
        {
            foreach (var characterAttribute in character.characterAttributes)
            {
                if (roomAttribute.attributeName == characterAttribute.attributeName)
                {
                    // マッチした場合、スコアを累積する
                    cp_score += roomAttribute.matchScore;  // cp_score に加算
                    totalScore += roomAttribute.matchScore;  // totalScore に加算

                    Debug.Log("Matched Attribute: " + roomAttribute.attributeName);
                    Debug.Log("Match Score: " + roomAttribute.matchScore);
                }
            }
        }

        // 結果をログに表示
        Debug.Log($"{character.name} matched with room {gameObject.name}, total score: {totalScore}");
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