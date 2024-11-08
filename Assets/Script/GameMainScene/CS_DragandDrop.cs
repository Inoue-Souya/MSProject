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
    private CS_Room cp_room;// ������Ƃ�����������ۑ�

    public List<RoomAttribute> characterAttributes;

    public CS_Effect effectController;//�p�[�e�B�N���p
    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        inRoom = false;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        Debug.Log("Dragging started on: " + gameObject.name);
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckRoom();
        Debug.Log("Dragging ended on: " + gameObject.name);
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
            gaugeTimer -= Time.deltaTime;
            gaugeSlider.value = gaugeTimer / gaugeDuration;

            if (gaugeTimer <= 0f)
            {
                // ��ƏI�����Ƀ��Z�b�g�Ƃ����̑���
                ResetToOriginalPosition();
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
                    cp_room.AddResident(this);      // �d�������L�^
                    inRoom = true;                  // �����t���O�𗧂Ă�
                    cp_room.setinRoomflag(inRoom);  // �����̌��E�g�p���Ԃ̏���t���O�𗧂Ă�
                    PlaceSmallImage(room.transform.position);
                    StartGaugeCountdown(room.transform.position);
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
        gameObject.transform.position = position;
        //GameObject smallImage = Instantiate(smallImagePrefab, position, Quaternion.identity);
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);


        // �G�t�F�N�g�Đ�
        if (effectController != null)
        {
            effectController.PlayPlacementEffect(position);
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
            gaugeInstance = Instantiate(gaugePrefab, position, Quaternion.identity);
            gaugeInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

            gaugeSlider = gaugeInstance.GetComponentInChildren<Slider>();

            if (gaugeSlider != null)
            {
                gaugeSlider.value = 1f;
            }
            else
            {
                Debug.LogWarning("Gauge Slider not found on the instantiated prefab.");
            }

            gaugeTimer = gaugeDuration;
        }
        else
        {
            Debug.LogWarning("Gauge already exists or prefab is null!");
        }
    }

    private void ResetToOriginalPosition()
    {
        Destroy(gaugeInstance); // �Q�[�W���폜
        transform.position = originalPosition; // �I�u�W�F�N�g�����̈ʒu�ɖ߂�
        //transform.localScale = new Vector3(1.0f, 1.0f, 1f);
        // Destroy(this); // ���݂̃C���X�^���X��j��
    }

   
}
