using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreManager : MonoBehaviour
{
    public int currentScore;

    public Text scoreText; // Textコンポーネントへの参照

    public void Init()
    {
        // 初期スコア設定
        UpdateScoreUI(); // UIの更新メソッドを呼び出す
    }
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

    public void UpdateScoreUI()
    {
        // スコア表示の更新処理
        scoreText.text = "現在: " + currentScore + "怨"; // スコアを更新
    }
}
