using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CS_Room : MonoBehaviour
{
    public List<RoomAttribute> attributes; // �����̓������X�g
    public CS_ScoreManager scoreManager;
    //public CS_ScoreDisplay scoreDisplay; // ScoreDisplay�ւ̎Q��
    public int unlockCost = 10; // ������������邽�߂̃X�R�A�R�X�g
    public bool isUnlocked = false; // �������������Ă��邩

    private int totalScore;

    // �V�������\�b�h��ǉ�
    public void InitializeRoom(bool unlockStatus)
    {
        isUnlocked = unlockStatus;
    }

    private void Start()
    {
        // �����X�R�A�ݒ�
        scoreManager.Init();
    }


    public void AddResident(CS_DragandDrop character)
    {
        if (!isUnlocked)
        {
            Debug.Log("This room is locked.");
            return; // �������������Ă��Ȃ��ꍇ�͉������Ȃ�
        }

        // �L�����N�^�[�̓����ƃ}�b�`����X�R�A���v�Z
        foreach (var roomAttribute in attributes)
        {
            foreach (var characterAttribute in character.characterAttributes)
            {
                if (roomAttribute.attributeName == characterAttribute.attributeName)
                {
                    totalScore += roomAttribute.matchScore * 100; // �}�b�`�����ꍇ�X�R�A�����Z
                    if (scoreManager != null)
                    {
                        scoreManager.AddScore(totalScore);
                    }
                }
            }
        }

        Debug.Log($"{character.name} matched with room {gameObject.name}, score: {totalScore}");
    }
}