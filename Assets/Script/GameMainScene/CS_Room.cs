using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CS_Room : MonoBehaviour
{
    public List<RoomAttribute> attributes; // �����̓������X�g
    public CS_ScoreManager scoreManager;
    public CS_NewRoomManager roomManager;
    [SerializeField]
    private CS_Compare compare;

    // �ύX���鑮�����̌�⃊�X�g
    private List<string> nameOptions = new List<string> { "��������", "�q������", "����������",
        "���a", "���D��" ,"������","�Ⴂ","����������","��������","�e����"};

    [Header("��������Ɋւ�����")]
    public int unlockCost = 10;     // ������������邽�߂̃X�R�A�R�X�g
    public bool isUnlocked = false; // �������������Ă��邩
    private bool inRoomflag;        // �����̐�L�t���O
    private int bonus_score;        // ������v�œ�����{�[�i�X
    private bool bonus_flag;        // �{�[�i�X�𓾂���t���O

    [Header("�f�_")]
    //public int default_Point;       // �Œ�������邨��
    private float DurationTime;     // �����̐�L���Ԃ�ۑ�����ϐ�

    [SerializeField]
    //private float roomHP;           // �����̍ő嗘�p����
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
    public AudioClip soundEffect3;  // ���ʉ���AudioClip(�����g�p�s��)
    private AudioSource audioSource;  // AudioSource�R���|�[�l���g

    // �V�������\�b�h��ǉ�
    // �R���C�_�[�R���|�[�l���g���i�[
    private Collider2D collider2D;
    // �R���C�_�[�𖳌��ɂ��鎞�ԁi�b�j
    public float disableTime = 10f;

    private string nowState = "��������";
    private bool typeFlg;   //���ʂ����邩�ǂ����̔��菈��

    //�F�K�C�h�p�I�u�W�F�N�g�Q��
    public GameObject guideObj;
    // �C���X�^���X�p�ϐ�
    private GameObject instance;
    private List<GameObject> instances = new List<GameObject>(); // �����̃C���X�^���X���Ǘ����郊�X�g


    public void InitializeRoom(bool unlockStatus)
    {
        if (roomManager.openRoom == 29)
        {
            // �����_���Ȏ��ԂŃR���C�_�[���ėL����
            float randomTime = Random.Range(20f, disableTime);
            StartCoroutine(ReenableColliderAfterTime(randomTime));
        }
        else
        {
            isUnlocked = unlockStatus;
        }
    }

    private void Start()
    {
        // �����X�R�A�ݒ�
        scoreManager.Init();
        inRoomflag = false;
        bonus_flag = false;
        typeFlg = false;

        ReSetIsUnlocked();

        // �w�肳�ꂽ�Q�[���I�u�W�F�N�g����AudioSource�R���|�[�l���g���擾
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

        // �R���C�_�[�����������A�������������Ă��Ȃ��ꍇ�͗L���ɂ��Ă���
        collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = !isUnlocked;
        }

        if (isUnlocked)
        {
            // �������������ꂽ�ꍇ�A�R���C�_�[�𖳌���
            ToggleCollider(false);

            // �Z���������������̂ŁAisUnlocked��false�ɂ���
            isUnlocked = false;

            //�q�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (childObject != null)
            {
                childObject.SetActive(isUnlocked);
            }


            // �����_���Ȏ��ԂŃR���C�_�[���ėL����
            float randomTime = Random.Range(10f, disableTime);
            StartCoroutine(ReenableColliderAfterTime(randomTime));

            typeFlg = false;

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
            //roomHP -= hpDecreaseRate * Time.deltaTime;

            // After 5 seconds, stop decreasing and reset the flag
            if (elapsedTime >= DurationTime)
            {
                inRoomflag = false;
                elapsedTime = 0f; // Reset timer

                if (typeFlg)
                {
                    // �������������ꂽ�ꍇ�A�R���C�_�[�𖳌���
                    ToggleCollider(false);

                    // �Z���������������̂ŁAisUnlocked��false�ɂ���
                    isUnlocked = false;

                    // �����_���Ȏ��ԂŃR���C�_�[���ėL����
                    float randomTime = Random.Range(40f, disableTime);
                    StartCoroutine(ReenableColliderAfterTime(randomTime));

                    typeFlg = false;
                }


                // �T�E���h�𗬂�
                if (audioSource != null && soundEffect1 != null)
                {
                    audioSource.PlayOneShot(soundEffect1);
                }
            }
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

        if (inRoomflag && childSpriteRenderer != null && newSprite != null && typeFlg)
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
        //bonus_score = default_Point; // �V�����Z����ǉ����邽�тɃX�R�A�����Z�b�g����i�ݐς��邽�߁j
        //totalScore = default_Point;
        bonus_flag = false;

        // �d���̕�����L���Ԃ��L�^
        DurationTime = Duration;

        // �L�����N�^�[�̓����ƃ}�b�`����X�R�A���v�Z
        foreach (var roomAttribute in attributes)
        {
            foreach (var characterAttribute in character.characterAttributes)
            {
                //�������ƍ�����
                switch (compare.CompareCharactor(nameOptions.IndexOf(nowState), characterAttribute.attributeName))
                {
                    case 0:
                        // �}�b�`�����ꍇ�A�X�R�A��ݐς���
                        bonus_score += roomAttribute.matchScore / 2 * 3;  // bonus_score �ɉ��Z
                        totalScore += roomAttribute.matchScore / 2 * 3;  // totalScore �ɉ��Z
                        bonus_flag = true;
                        typeFlg = true;
                        if (roomManager.inResident > 0)
                        {
                            roomManager.inResident--;
                        }
                        break;
                    case 1:
                        // �}�b�`�����ꍇ�A�X�R�A��ݐς���
                        bonus_score += roomAttribute.matchScore;  // cp_score �ɉ��Z
                        totalScore += roomAttribute.matchScore;  // totalScore �ɉ��Z
                        bonus_flag = true;
                        typeFlg = true;
                        if (roomManager.inResident > 0)
                        {
                            roomManager.inResident--;
                        }
                        break;
                    case 2:
                        break;
                    default:
                        break;

                }
            }
        }

        ResetGuide();

        // ���ʂ����O�ɕ\��
        Debug.Log($"{character.name} matched with room {gameObject.name}, total score: {totalScore}");
    }

    private void ReSetIsUnlocked()
    {

        // �ύX���鐔��10�ɐݒ�
        int changeCount = Mathf.Min(10, attributes.Count); // �����̐���5��菭�Ȃ��ꍇ�͂����D��

        for (int i = 0; i < changeCount; i++)
        {
            // �����_���Ƀ��X�g���瑮������I��
            string randomName = nameOptions[Random.Range(0, nameOptions.Count)];

            // �����_����RoomAttribute��I�����Ă��̑�������ύX
            RoomAttribute randomRoom = attributes[Random.Range(0, attributes.Count)];
            randomRoom.attributeName = randomName;
            nowState = randomName;
        }
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

    // �R���C�_�[�𖳌��� / �L�������郁�\�b�h
    void ToggleCollider(bool isActive)
    {
        if (collider2D != null)
        {
            collider2D.enabled = isActive;
        }
    }

    // ��莞�Ԍ�ɃR���C�_�[���ēx�L��������R���[�`��
    System.Collections.IEnumerator ReenableColliderAfterTime(float time)
    {
        // �w�莞�ԑҋ@
        yield return new WaitForSeconds(time);

        // �R���C�_�[���ēx�L����
        ToggleCollider(true);

        roomManager.inResident++;

        isUnlocked = true;

        ReSetIsUnlocked();
    }

    public void GuideType(CS_DragandDrop character)
    {
        if (isUnlocked && !inRoomflag)
        {
            // �L�����N�^�[�̓����ƃ}�b�`����X�R�A���v�Z
            foreach (var roomAttribute in attributes)
            {
                foreach (var characterAttribute in character.characterAttributes)
                {
                    //instance = Instantiate(guideObj);
                    //Instantiate(instance, this.transform.position, Quaternion.identity);

                    instance = Instantiate(guideObj);  // �C���X�^���X�𐶐�
                    instance.transform.position = this.transform.position;
                    instances.Add(instance);  // ���X�g�ɃC���X�^���X��ǉ�
                    SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();

                    //�������ƍ�����
                    switch (compare.CompareCharactor(nameOptions.IndexOf(nowState), characterAttribute.attributeName))
                    {
                        //�������Q�̏ꍇ
                        case 0:
                            spriteRenderer.color = new Color(0f, 1f, 0f, 0.2f); // �ΐF�ɂ���
                            break;
                        //�������ʂ̏ꍇ
                        case 1:
                            spriteRenderer.color = new Color(0f, 0f, 0f, 0f); // �������Ȃ�
                            break;
                        //���ʂȂ��̏ꍇ
                        case 2:
                            spriteRenderer.color = new Color(0.4f, 0.3f, 1f, 0.4f); // �F�ɂ���
                            break;
                        default:
                            break;

                    }

                }
            }
        }
    }

    public void ResetGuide()
    {
        if (isUnlocked)
        {
            if (instance != null)
            {
                // ���X�g�ɕێ�����Ă���S�ẴC���X�^���X���폜
                foreach (var inst in instances)
                {
                    Destroy(inst);  // �C���X�^���X���폜
                }
                instances.Clear();  // ���X�g���N���A            
            }
        }
    }
}