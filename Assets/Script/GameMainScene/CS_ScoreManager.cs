using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScoreManager : MonoBehaviour
{
    public int currentScore = 0;

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreUI(); // UI�̍X�V���\�b�h���Ăяo��
    }

    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;
            UpdateScoreUI(); // UI�̍X�V���\�b�h���Ăяo��
            return true;
        }
        return false; // �X�R�A������Ȃ��ꍇ
    }

    private void UpdateScoreUI()
    {
        // �X�R�A�\���̍X�V����
        Debug.Log("Current Score: " + currentScore);
    }
}
