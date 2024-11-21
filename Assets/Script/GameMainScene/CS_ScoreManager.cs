using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreManager : MonoBehaviour
{
    public Sprite[] numberSprites; // �����̃X�v���C�g������z��
    public int currentScore;
    public Image[] scoreImages; // �X�R�A��\������Image�̔z��
    public GameObject audioSourceObject;  // SE���Đ����邽�߂�AudioSource���A�^�b�`���ꂽ�Q�[���I�u�W�F�N�g
    public AudioClip soundEffect1;  // ���ʉ���AudioClip(�{�[�i�X�Ȃ�)
    public AudioClip soundEffect2;  // ���ʉ���AudioClip(�{�[�i�X����)
    private AudioSource audioSource;  // AudioSource�R���|�[�l���g

    public void Init()
    {
        // �����X�R�A�ݒ�
        UpdateScoreDisplay();

        // �w�肳�ꂽ�Q�[���I�u�W�F�N�g����AudioSource�R���|�[�l���g���擾
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

    }
    public void AddScore(int score, bool soundflag)
    {
        currentScore += score;
        UpdateScoreDisplay();

        // �T�E���h�𗬂�
        if(soundflag)
        {// �{�[�i�X������Ƃ��̃T�E���h
            if (audioSource != null && soundEffect2 != null)
            {
                audioSource.PlayOneShot(soundEffect2);
            }
        }
        else
        {// �{�[�i�X���Ȃ��Ƃ��̃T�E���h
            if (audioSource != null && soundEffect1 != null)
            {
                audioSource.PlayOneShot(soundEffect1);
            }
        }
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
