//using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_DragandDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;
    private bool inRoom;
    private Vector3 originalPosition;

    public GameObject gaugePrefab; // �Q�[�W�̃v���n�u
    private GameObject gaugeInstance; // �Q�[�W�̃C���X�^���X
    private Slider gaugeSlider; // �Q�[�W�̃X���C�_�[�R���|�[�l���g
    public float gaugeDuration = 5f; // �Q�[�W�̎�������
    private float gaugeTimer;
    private Vector3 gaugeFixedWorldPosition; // �Q�[�W�Œ�ʒu��ۑ�����ϐ�
    private CS_Room cp_room;// ������Ƃ�����������ۑ�

    public List<RoomAttribute> characterAttributes;

    public CS_Effect effectController;//�p�[�e�B�N���p

    public AudioClip soundEffect;  // ���ʉ���AudioClip
    public GameObject audioSourceObject;  // SE���Đ����邽�߂�AudioSource���A�^�b�`���ꂽ�Q�[���I�u�W�F�N�g

    private AudioSource audioSource;  // AudioSource�R���|�[�l���g

    public CS_Yo_kaiChange ChangeManager;// �d����[�p


    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        inRoom = false;

        // �w�肳�ꂽ�Q�[���I�u�W�F�N�g����AudioSource�R���|�[�l���g���擾
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        Debug.Log("Dragging started on: " + gameObject.name);
        // �����Đ�
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckRoom();
        Debug.Log("Dragging ended on: " + gameObject.name);
        audioSource.Stop();
    }

    void Update()
    {
        if (isDragging && !inRoom)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        // �Q�[�W���\������Ă���ꍇ�A���Ԃ�����������
        if (gaugeInstance != null)
        {

            // ���t���[���A�ʒu���X�V
            UpdateGaugePosition(transform.position);

            gaugeTimer -= Time.deltaTime;
            gaugeSlider.value = gaugeTimer / gaugeDuration;

            if (gaugeTimer <= 0f)
            {
                // ��ƏI�����Ƀ��Z�b�g�Ƃ����̑���

                // �d���̕�[�V�X�e��������-----------------------
                ChangeManager.SwapRandomObject(this.name);

                // --------------------------------------------------

                Destroy(gaugeInstance); // �Q�[�W���폜
                cp_room.finishPhase();
                inRoom = false;
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    private void CheckRoom()
    {
        Debug.Log("Start CheckRoom.");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Room"))
            {
                CS_Room room = collider.GetComponent<CS_Room>();
                cp_room = room;
                if (room.isUnlocked && !cp_room.GettinRoom())
                {
                    cp_room.AddResident(this, gaugeDuration);      // �d�������L�^
                    inRoom = true;                  // �����t���O�𗧂Ă�
                    cp_room.setinRoomflag(inRoom);  // �����̌��E�g�p���Ԃ̏���t���O�𗧂Ă�
                    PlaceSmallImage(room.transform.position);
                    StartGaugeCountdown(this.transform.position);
                }
                else
                {
                    Debug.Log("Room is locked. Cannot drag here.");
                }
                break;
            }
            else
            {
                transform.position = originalPosition;
            }
        }
    }

    private void PlaceSmallImage(Vector3 position)
    {
        Vector3 offsetPos = new Vector3(0.5f, 0.0f, 0.0f);
        gameObject.transform.position = position + offsetPos;

        // �G�t�F�N�g�Đ�
        if (effectController != null)
        {
            effectController.PlayPlacementEffect(gameObject.transform.position);
        }
        else
        {
            Debug.LogWarning("Effect controller is not assigned in CS_DragandDrop.");
        }
    }

    private void StartGaugeCountdown(Vector3 position)
    {
        if (gaugePrefab != null && gaugeInstance == null) // ���łɃQ�[�W���Ȃ��ꍇ�̂ݐ���
        {
            gaugeInstance = Instantiate(gaugePrefab);
            gaugeInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();



            // �Q�[�W�̈ʒu�̐ݒ�
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                gaugeInstance.transform.position = position;
            }
            else
            {
                gaugeInstance.transform.position = mainCamera.WorldToScreenPoint(position);
            }

            // �Q�[�W�T�C�Y�̐ݒ�
            RectTransform gaugeRect = gaugeInstance.GetComponent<RectTransform>();
            if (gaugeRect != null)
            {
                gaugeRect.sizeDelta = new Vector2(100, 20); // ��100�A����20�i�D�݂ɍ��킹�Ē����j
            }
            else
            {
                Debug.LogWarning("RectTransform not found on gauge instance.");
            }

            gaugeSlider = gaugeInstance.GetComponentInChildren<Slider>();
            if (gaugeSlider != null)
            {
                gaugeSlider.value = 1f;
            }
            else
            {
                Debug.LogWarning("Gauge Slider not found on the instantiated prefab.");
            }

            // �^�C�}�[�ݒ�
            gaugeTimer = gaugeDuration;

            // �����ʒu�ݒ�
            UpdateGaugePosition(position);
        }
        else
        {
            Debug.LogWarning("Gauge already exists or prefab is null!");
        }
    }

    private void UpdateGaugePosition(Vector3 worldPosition)
    {
        if (gaugeInstance != null)
        {
            // �Q�[�W�̈ʒu�𒲐�(���[���h���W�ɂ��l�͏���)
            Vector3 offsetPos = new Vector3(0, -1, 0); // �����ŃI�t�Z�b�g�l�𒲐��i��: y������-1�j

            // ���[���h���W���X�N���[�����W�ɕϊ�
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition + offsetPos);

            // �Q�[�W�̈ʒu���X�N���[�����W�ɐݒ�
            gaugeInstance.transform.position = screenPosition;
        }
    }

    public void SetPosition(Vector3 vector)
    {
        originalPosition = vector;
    }
}
