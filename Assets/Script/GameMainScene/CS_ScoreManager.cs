using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreManager : MonoBehaviour
{
    public int currentScore;

    public Text scoreText; // Text�R���|�[�l���g�ւ̎Q��

    public void Init()
    {
        // �����X�R�A�ݒ�
        UpdateScoreUI(); // UI�̍X�V���\�b�h���Ăяo��
    }
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

    public void UpdateScoreUI()
    {
        // �X�R�A�\���̍X�V����
        scoreText.text = "����: " + currentScore + "��"; // �X�R�A���X�V
    }
}
