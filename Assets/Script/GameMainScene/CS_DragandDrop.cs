//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CS_DragandDrop : MonoBehaviour
//{
//    private Vector3 offset;
//    private Camera mainCamera;
//    private bool isDragging;
//    private Vector3 originalPosition; // 元の位置を保存する変数
//    public GameObject smallImagePrefab; // 小さい画像のプレハブ

//    public List<RoomAttribute> characterAttributes; // キャラクターの特性

//    void Start()
//    {
//        mainCamera = Camera.main;
//        originalPosition = transform.position; // 元の位置を記録
//        characterAttributes = new List<RoomAttribute>(); // 特性の初期化
//        // 特性を追加する例
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
//        // ドラッグが終了したら元の位置に戻す
//        transform.position = originalPosition;
//        Debug.Log("Dragging ended on: " + gameObject.name);
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))  // ドラッグ開始
//        {
//            OnMouseDown();
//        }

//        if (Input.GetMouseButtonUp(0))  // ドラッグ終了
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
//        // 部屋に住まわせる処理
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
//        foreach (var collider in colliders)
//        {
//            if (collider.CompareTag("Room")) // 部屋のタグを設定しておく
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
//        // 小さい画像を部屋の位置に生成
//        if (smallImagePrefab != null)
//        {
//            GameObject smallImage = Instantiate(smallImagePrefab, position, Quaternion.identity);
//            // 必要に応じて、ここで小さい画像の位置やスケールを調整
//            smallImage.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // サイズを調整
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
    public GameObject gaugePrefab; // ゲージのプレハブ
    private GameObject gaugeInstance; // ゲージのインスタンス
    private Slider gaugeSlider; // ゲージのスライダーコンポーネント
    public float gaugeDuration = 5f; // ゲージの持続時間
    private float gaugeTimer;
    private CS_Room cp_room;// 判定をとった部屋情報を保存

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

        // ゲージが表示されている場合、時間を減少させる
        if (gaugeInstance != null)
        {
            gaugeTimer -= Time.deltaTime;
            gaugeSlider.value = gaugeTimer / gaugeDuration;

            if (gaugeTimer <= 0f)
            {
                // 作業終了時にリセットとお金の増加
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
        if (gaugePrefab != null && gaugeInstance == null) // すでにゲージがない場合のみ生成
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
        Destroy(gaugeInstance); // ゲージを削除
        transform.position = originalPosition; // オブジェクトを元の位置に戻す
        //transform.localScale = new Vector3(1.0f, 1.0f, 1f);
        // Destroy(this); // 現在のインスタンスを破棄
    }
}
