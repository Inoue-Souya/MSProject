using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreDisplay : MonoBehaviour
{
    public Text scoreText; // Text�R���|�[�l���g�ւ̎Q��

    public void UpdateScore(int score)
    {
        scoreText.text = "����: " + score +"��"; // �X�R�A���X�V
    }
}
