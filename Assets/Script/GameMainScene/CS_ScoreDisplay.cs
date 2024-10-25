using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreDisplay : MonoBehaviour
{
    public Text scoreText; // Textコンポーネントへの参照

    public void UpdateScore(int score)
    {
        scoreText.text = "現在: " + score +"怨"; // スコアを更新
    }
}
