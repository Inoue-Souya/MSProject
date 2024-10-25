using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScoreManager : MonoBehaviour
{
    public int currentScore = 0;

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreUI(); // UIの更新メソッドを呼び出す
    }

    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;
            UpdateScoreUI(); // UIの更新メソッドを呼び出す
            return true;
        }
        return false; // スコアが足りない場合
    }

    private void UpdateScoreUI()
    {
        // スコア表示の更新処理
        Debug.Log("Current Score: " + currentScore);
    }
}
