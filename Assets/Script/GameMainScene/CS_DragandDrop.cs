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

    public GameObject gaugePrefab; // ゲージのプレハブ
    private GameObject gaugeInstance; // ゲージのインスタンス
    private Slider gaugeSlider; // ゲージのスライダーコンポーネント
    public float gaugeDuration = 5f; // ゲージの持続時間
    private float gaugeTimer;
    private CS_Room cp_room;// 判定をとった部屋情報を保存

    public List<RoomAttribute> characterAttributes;

    public CS_Effect effectController;//パーティクル用

    public AudioClip soundEffect;  // 効果音のAudioClip
    public GameObject audioSourceObject;  // SEを再生するためのAudioSourceがアタッチされたゲームオブジェクト

    private AudioSource audioSource;  // AudioSourceコンポーネント


    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        inRoom = false;

        // 指定されたゲームオブジェクトからAudioSourceコンポーネントを取得
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
        // 音を再生
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

        // ゲージが表示されている場合、時間を減少させる
        if (gaugeInstance != null)
        {
            gaugeTimer -= Time.deltaTime;
            gaugeSlider.value = gaugeTimer / gaugeDuration;

            if (gaugeTimer <= 0f)
            {
                // 作業終了時にリセットとお金の増加
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
                    cp_room.AddResident(this, gaugeDuration);      // 妖怪情報を記録
                    inRoom = true;                  // 入室フラグを立てる
                    cp_room.setinRoomflag(inRoom);  // 部屋の限界使用時間の消費フラグを立てる
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
        gameObject.transform.position = position;
        //gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);


        // エフェクト再生
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
        if (gaugePrefab != null && gaugeInstance == null) // すでにゲージがない場合のみ生成
        {
            gaugeInstance = Instantiate(gaugePrefab);
            gaugeInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            // ゲージの位置を調整
            Vector3 offset = new Vector3(0, 60, 0); // ここでオフセット値を調整（例: y方向に50）

            // ゲージの位置の設定
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                gaugeInstance.transform.position = position + offset;
            }
            else
            {
                gaugeInstance.transform.position = mainCamera.WorldToScreenPoint(position) + offset;
            }

            // ゲージサイズの設定
            RectTransform gaugeRect = gaugeInstance.GetComponent<RectTransform>();
            if (gaugeRect != null)
            {
                gaugeRect.sizeDelta = new Vector2(100, 20); // 幅100、高さ20（好みに合わせて調整）
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
    }
}
