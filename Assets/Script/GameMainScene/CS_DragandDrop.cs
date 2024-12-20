//using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_DragandDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    public bool isDragging;
    private bool inRoom;
    private Vector3 originalPosition;
    private Sprite originalSprite;

    public GameObject gaugePrefab; // ゲージのプレハブ
    private GameObject gaugeInstance; // ゲージのインスタンス
    private Slider gaugeSlider; // ゲージのスライダーコンポーネント
    public float gaugeDuration = 5f; // ゲージの持続時間
    private float gaugeTimer;
    private CS_Room cp_room;// 判定をとった部屋情報を保存
    public CS_NewRoomManager roomManager;

    public List<RoomAttribute> characterAttributes;

    public CS_Effect effectController;//パーティクル用

    public AudioClip soundEffect;  // 効果音のAudioClip
    public GameObject audioSourceObject;  // SEを再生するためのAudioSourceがアタッチされたゲームオブジェクト

    private AudioSource audioSource;  // AudioSourceコンポーネント

    public CS_Yo_kaiChange ChangeManager;// 妖怪補充用

    public Sprite IkonSprite;// アイコン用画像保存する変数

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        cp_room = GetComponent<CS_Room>();
        // スプライトの保存
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            originalSprite = sprite.sprite;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on the GameObject.");
        }

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
        // 音を再生
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
        for (int i = 0; i < roomManager.openRoom; i++)
        {
            roomManager.rooms[i].GuideType(this);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (!inRoom)
        {
            CheckRoom();
            audioSource.Stop();
            for (int i = 0; i < roomManager.openRoom; i++)
            {
                roomManager.rooms[i].ResetGuide();
            }
        }
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
            transform.position = originalPosition;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sortingOrder = 2;
            // 毎フレーム、位置を更新
            UpdateGaugePosition(transform.position);

            gaugeTimer -= Time.deltaTime;
            gaugeSlider.value = gaugeTimer / gaugeDuration;

            if (gaugeTimer <= 0f)
            {
                // 作業終了時にリセットとお金の増加

                // 妖怪の補充システムを入れる-----------------------
                ChangeManager.SwapRandomObject(this.name);

                // --------------------------------------------------

                Destroy(gaugeInstance); // ゲージを削除
                cp_room.finishPhase();
                inRoom = false;

                sprite.sprite = originalSprite;
                sprite.sortingOrder = 3;

                //Vector3 pos = transform.position;
                ////pos.z = 0;
                //transform.position = pos;
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
                    ChangeManager.UsedYo_kai(this.name);// 使用済みのアイコンにする
                    PlaceSmallImage(room.transform.position);
                    StartGaugeCountdown(this.transform.position);
                    Transform parent = transform.parent;
                    if(parent!=null)
                    {
                        parent.transform.SetParent(null);
                    }
                }
                else
                {
                    Debug.Log("Room is locked. Cannot drag here.");
                    transform.position = originalPosition;
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
        SetPosition(transform.position);

        // エフェクト再生
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
        if (gaugePrefab != null && gaugeInstance == null) // すでにゲージがない場合のみ生成
        {
            gaugeInstance = Instantiate(gaugePrefab);
            gaugeInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (canvas != null)
            {
                // ゲージをCanvasの子に設定
                gaugeInstance.transform.SetParent(canvas.transform, false);

                // ゲージの表示順を最背面に設定
                gaugeInstance.transform.SetSiblingIndex(0); // 一番後ろに設定
            }


            // ゲージの位置の設定
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                gaugeInstance.transform.position = position;
            }
            else
            {
                gaugeInstance.transform.position = mainCamera.WorldToScreenPoint(position);
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

            // タイマー設定
            gaugeTimer = gaugeDuration;

            // 初期位置設定
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
            // ゲージの位置を調整(ワールド座標につき値は少量)
            Vector3 offsetPos = new Vector3(0, -1, 0); // ここでオフセット値を調整（例: y方向に-1）

            // ワールド座標をスクリーン座標に変換
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition + offsetPos);

            // ゲージの位置をスクリーン座標に設定
            gaugeInstance.transform.position = screenPosition;
        }
    }

    public void SetPosition(Vector3 vector)
    {
        originalPosition = vector;
    }

    public bool GetisDragging()
    {
        return isDragging;
    }

    public bool GetinRoom()
    {
        return inRoom;
    }

    public Sprite GetSprite()
    {
        return originalSprite;
    }
}
