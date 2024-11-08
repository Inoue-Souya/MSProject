using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreManager : MonoBehaviour
{
    public Sprite[] numberSprites; // �����̃X�v���C�g������z��
    public int currentScore;
    public Image[] scoreImages; // �X�R�A��\������Image�̔z��

    public void Init()
    {
        // �����X�R�A�ݒ�
        UpdateScoreDisplay();
    }
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreDisplay();
    }

    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;
            UpdateScoreDisplay();
            return true;
        }
        return false; // �X�R�A������Ȃ��ꍇ
    }

    public void UpdateScoreDisplay()
    {
        // ���݂̃X�R�A�𐔎��̉摜�ɕϊ�����
        string scoreString = currentScore.ToString();
        for (int i = 0; i < scoreImages.Length; i++)
        {
            if (i < scoreString.Length)
            {
                // �����ɉ������X�v���C�g��\��
                int number = int.Parse(scoreString[i].ToString());
                scoreImages[i].sprite = numberSprites[number];
                scoreImages[i].gameObject.SetActive(true);
            }
            else
            {
                scoreImages[i].gameObject.SetActive(false); // �X�R�A�̌������Ȃ��ꍇ�͔�\��
            }
        }
    }
}
