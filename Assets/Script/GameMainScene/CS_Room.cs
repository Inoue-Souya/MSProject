using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CS_Room : MonoBehaviour
{
    public List<RoomAttribute> attributes; // �����̓������X�g
    public CS_ScoreManager scoreManager;
    public CS_NewRoomManager roomManager;

    [Header("��������Ɋւ�����")]
    public int unlockCost = 10;     // ������������邽�߂̃X�R�A�R�X�g
    public bool isUnlocked = false; // �������������Ă��邩
    private bool inRoomflag;        // �����̐�L�t���O
    private int bonus_score;        // ������v�œ�����{�[�i�X
    private bool bonus_flag;        // �{�[�i�X�𓾂���t���O

    [Header("�f�_")]
    public int default_Point;       // �Œ�������邨��
    private float DurationTime;     // �����̐�L���Ԃ�ۑ�����ϐ�

    [SerializeField]
    private float roomHP;           // �����̍ő嗘�p����
    private int totalScore;         // �����邨���̍��v�l
    private float elapsedTime = 0f;

    public GameObject childObject;      // �q�I�u�W�F�N�g�̎Q��
    public SpriteRenderer childSpriteRenderer;      // �q�I�u�W�F�N�g�̃X�v���C�gRenderer
    public Sprite oldSprite;        // �ύX�O�̃X�v���C�g
    public Sprite newSprite;        // �ύX��̃X�v���C�g

    [Header("�T�E���h�֘A")]
    public GameObject audioSourceObject;  // SE���Đ����邽�߂�AudioSource���A�^�b�`���ꂽ�Q�[���I�u�W�F�N�g
    public AudioClip soundEffect1;  // ���ʉ���AudioClip(�{�[�i�X�Ȃ�)
    public AudioClip soundEffect2;  // ���ʉ���AudioClip(�{�[�i�X����)
    private AudioSource audioSource;  // AudioSource�R���|�[�l���g

    // �V�������\�b�h��ǉ�
    public void InitializeRoom(bool unlockStatus)
    {
        isUnlocked = unlockStatus;
    }

    private void Start()
    {
        // �����X�R�A�ݒ�
        scoreManager.Init();
        inRoomflag = false;
        bonus_flag = false;

        // �w�肳�ꂽ�Q�[���I�u�W�F�N�g����AudioSource�R���|�[�l���g���擾
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (inRoomflag)
        {
            // Increase elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Calculate the HP decrease rate
            float hpDecreaseRate = bonus_score / DurationTime; // cp_score reduced over 5 seconds

            // Decrease roomHP gradually
            roomHP -= hpDecreaseRate * Time.deltaTime;

            // After 5 seconds, stop decreasing and reset the flag
            if (elapsedTime >= DurationTime)
            {
                inRoomflag = false;
                elapsedTime = 0f; // Reset timer

                // �T�E���h�𗬂�
                if (audioSource != null && soundEffect1 != null)
                {
                    audioSource.PlayOneShot(soundEffect1);
                }
            }
        }

        if(roomHP <= 0f)
        {
            // ���E�������𑝂₷
            roomManager.stopRooms++;

            // �����̓����蔻�����������
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(collider);

            // �Z���������������̂ŁAisUnlocked��false�ɂ���
            isUnlocked = false;

            // ��x�������s�������̂�
            // roomHP��1�ȏ�ɂ��Ēʂ�Ȃ��悤�ɂ���
            roomHP = 1;
        }

        // IsUnlocked �� true �ł���΁A�q�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        if (isUnlocked)
        {
            if (childObject != null)
            {
                childObject.SetActive(true); // �q�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            }
        }
        else
        {
            if (childObject != null)
            {
                childObject.SetActive(false); // IsUnlocked �� false �Ȃ�A�q�I�u�W�F�N�g���A�N�e�B�u�ɂ���
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
            return; // �������������Ă��Ȃ��ꍇ�͉������Ȃ�
        }

        // ���������Ă���
        bonus_score = default_Point; // �V�����Z����ǉ����邽�тɃX�R�A�����Z�b�g����i�ݐς��邽�߁j
        totalScore = default_Point;
        bonus_flag = false;

        // �d���̕�����L���Ԃ��L�^
        DurationTime = Duration;

        // �L�����N�^�[�̓����ƃ}�b�`����X�R�A���v�Z
        foreach (var roomAttribute in attributes)
        {
            foreach (var characterAttribute in character.characterAttributes)
            {
                if (roomAttribute.attributeName == characterAttribute.attributeName)
                {
                    // �}�b�`�����ꍇ�A�X�R�A��ݐς���
                    bonus_score += roomAttribute.matchScore;  // cp_score �ɉ��Z
                    totalScore += roomAttribute.matchScore;  // totalScore �ɉ��Z

                    bonus_flag = true;

                    Debug.Log("Matched Attribute: " + roomAttribute.attributeName);
                    Debug.Log("Match Score: " + roomAttribute.matchScore);
                }
            }
        }

        // ���ʂ����O�ɕ\��
        Debug.Log($"{character.name} matched with room {gameObject.name}, total score: {totalScore}");
    }

    public void finishPhase()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(totalScore, bonus_flag);
        }
    }

    public void setinRoomflag(bool room)
    {
        inRoomflag = room;

        // �T�E���h�𗬂�
        if (audioSource != null && soundEffect2 != null)
        {
            audioSource.PlayOneShot(soundEffect2);
        }
    }

    public bool GettinRoom()
    {
        return inRoomflag;
    }
}