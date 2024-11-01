//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CS_DragandDrop : MonoBehaviour
//{
//    private Vector3 offset;
//    private Camera mainCamera;
//    private bool isDragging;
//    private Vector3 originalPosition; // ���̈ʒu��ۑ�����ϐ�
//    public GameObject smallImagePrefab; // �������摜�̃v���n�u

//    public List<RoomAttribute> characterAttributes; // �L�����N�^�[�̓���

//    void Start()
//    {
//        mainCamera = Camera.main;
//        originalPosition = transform.position; // ���̈ʒu���L�^
//        characterAttributes = new List<RoomAttribute>(); // �����̏�����
//        // ������ǉ������
//        characterAttributes.Add(new RoomAttribute { attributeName = "AB", matchScore = 10 });
//        //characterAttributes.Add(new RoomAttribute { attributeName = "C", matchScore = 5 });

//    }

//    void OnMouseDown()
//    {
//        isDragging = true;
//        offset = transform.position - GetMouseWorldPosition();
//        Debug.Log("Dragging started on: " + gameObject.name);
//    }

//    void OnMouseUp()
//    {
//        isDragging = false;
//        CheckRoom();
//        // �h���b�O���I�������猳�̈ʒu�ɖ߂�
//        transform.position = originalPosition;
//        Debug.Log("Dragging ended on: " + gameObject.name);
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))  // �h���b�O�J�n
//        {
//            OnMouseDown();
//        }

//        if (Input.GetMouseButtonUp(0))  // �h���b�O�I��
//        {
//            OnMouseUp();
//        }

//        if (isDragging)
//        {
//            transform.position = GetMouseWorldPosition() + offset;
//        }
//    }

//    private Vector3 GetMouseWorldPosition()
//    {
//        Vector3 mouseScreenPos = Input.mousePosition;
//        mouseScreenPos.z = mainCamera.transform.position.z;
//        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
//    }

//    private void CheckRoom()
//    {
//        // �����ɏZ�܂킹�鏈��
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
//        foreach (var collider in colliders)
//        {
//            if (collider.CompareTag("Room")) // �����̃^�O��ݒ肵�Ă���
//            {
//                CS_Room room = collider.GetComponent<CS_Room>();
//                if (room.isUnlocked)
//                {
//                    room.AddResident(this);
//                    PlaceSmallImage(room.transform.position);
//                    Destroy(gameObject);

//                }
//                else
//                {
//                    Debug.Log("Room is locked. Cannot drag here.");
//                }
//                break;
//            }
//        }
//    }

//    private void PlaceSmallImage(Vector3 position)
//    {
//        // �������摜�𕔉��̈ʒu�ɐ���
//        if (smallImagePrefab != null)
//        {
//            GameObject smallImage = Instantiate(smallImagePrefab, position, Quaternion.identity);
//            // �K�v�ɉ����āA�����ŏ������摜�̈ʒu��X�P�[���𒲐�
//            smallImage.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // �T�C�Y�𒲐�
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_DragandDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;
    private bool inRoomflag;
    private Vector3 originalPosition;
    public GameObject gaugePrefab; // �Q�[�W�̃v���n�u
    private GameObject gaugeInstance; // �Q�[�W�̃C���X�^���X
    private Slider gaugeSlider; // �Q�[�W�̃X���C�_�[�R���|�[�l���g
    public float gaugeDuration = 5f; // �Q�[�W�̎�������
    private float gaugeTimer;
    private CS_Room cp_room;// ������Ƃ�����������ۑ�

    public List<RoomAttribute> characterAttributes;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        characterAttributes = new List<RoomAttribute>();
        characterAttributes.Add(new RoomAttribute { attributeName = "AB", matchScore = 10 });
        inRoomflag = false;
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    OnMouseDown();
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    OnMouseUp();
        //}

        if (isDragging && !inRoomflag)
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
                cp_room.AddResident(this);
                inRoomflag = false;
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
                if (room.isUnlocked)
                {
                    inRoomflag = true;
                    //room.AddResident(this);
                    PlaceSmallImage(room.transform.position);
                    StartGaugeCountdown(room.transform.position);
                    //Destroy(gameObject);
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
