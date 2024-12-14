using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class CS_Room : MonoBehaviour
{
    public List<RoomAttribute> attributes; // 部屋の特性リスト
    public CS_ScoreManager scoreManager;
    public CS_NewRoomManager roomManager;
    [SerializeField]
    private CS_Compare compare;

    // 変更する属性名の候補リスト
    private List<string> nameOptions = new List<string> { "動物嫌い", "子供嫌い", "引きこもり",
        "臆病", "女好き" ,"寒がり","若い","おおざっぱ","頭が悪い","親嫌い"};

    //住民画像リスト
    public List<Sprite> residentBeforeImages = new List<Sprite>();
    public List<Sprite> residentAfterImages = new List<Sprite>();

    // リアクション吹き出し用配列
    public List<GameObject> reactionImage = new List<GameObject> { };
    private int reactionImageNum;
    private Vector3 imagePos;


    [Header("部屋解放に関する情報")]
    public int unlockCost = 10;     // 部屋を解放するためのスコアコスト
    public bool isUnlocked = false; // 部屋が解放されているか
    private bool inRoomflag;        // 部屋の占有フラグ
    private int bonus_score;        // 特徴一致で得られるボーナス
    private bool bonus_flag;        // ボーナスを得られるフラグ
    public bool isResidents;       // 住民の入居に関するフラグ


    [Header("素点")]
    //public int default_Point;       // 最低限得られるお金
    private float DurationTime;     // 部屋の占有時間を保存する変数

    [SerializeField]
    //private float roomHP;           // 部屋の最大利用時間
    private int totalScore;         // 得られるお金の合計値
    private float elapsedTime = 0f;

    public GameObject childObject;      // 子オブジェクトの参照
    public SpriteRenderer childSpriteRenderer;      // 子オブジェクトのスプライトRenderer
    //public Sprite oldSprite;        // 変更前のスプライト
    //public Sprite newSprite;        // 変更後のスプライト

    [Header("サウンド関連")]
    public GameObject audioSourceObject;  // SEを再生するためのAudioSourceがアタッチされたゲームオブジェクト
    public AudioClip soundEffect1;  // 効果音のAudioClip(ボーナスなし)
    public AudioClip soundEffect2;  // 効果音のAudioClip(ボーナスあり)
    public AudioClip soundEffect3;  // 効果音のAudioClip(部屋使用不可)
    private AudioSource audioSource;  // AudioSourceコンポーネント

    // 新しいメソッドを追加
    // コライダーコンポーネントを格納
    private Collider2D collider2D;
    // コライダーを無効にする時間（秒）
    public float disableTime = 10f;

    private string nowState = "動物嫌い";
    private bool typeFlg;   //効果があるかどうかの判定処理

    //色ガイド用オブジェクト参照
    public GameObject guideObj;
    // インスタンス用変数
    private GameObject instance;
    private List<GameObject> instances = new List<GameObject>(); // 複数のインスタンスを管理するリスト
    
    public void InitializeRoom(bool unlockStatus)
    {
        if (roomManager.openRoom == roomManager.rooms.Length - 1)
        {
            SceneManager.LoadScene("GameClearScene");
        }
        else
        {
            isUnlocked = true;
            // ランダムな時間でコライダーを再有効化
            float randomTime = Random.Range(5f, disableTime);
            StartCoroutine(ReenableColliderAfterTime(randomTime));
        }
    }

    private void Start()
    {
        // 初期スコア設定
        scoreManager.Init();
        inRoomflag = false;
        bonus_flag = false;
        typeFlg = false;
        reactionImageNum = 0;

        ReSetIsResidents();
        imagePos = this.transform.position;
        imagePos.x -= 1.2f;
        imagePos.y += 0.75f;
        imagePos.z = -3.0f;
        //座標をリセットする
        //for (int i = 0; i < 3; i++)
        //{
        //    reactionImage[i].transform.position = imagePos;
        //    reactionImage[i].SetActive(false);
        //}

        // 指定されたゲームオブジェクトからAudioSourceコンポーネントを取得
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

        // コライダーを初期化し、部屋が解放されていない場合は有効にしておく
        collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = !isResidents;
        }

        if (isResidents)
        {
            // 条件が満たされた場合、コライダーを無効化
            ToggleCollider(false);

            // 住民を消去したいので、isUnlockedをfalseにする
            isResidents = false;

            //子オブジェクトを非アクティブにする
            if (childObject != null)
            {
                childObject.SetActive(isUnlocked);
            }


            // ランダムな時間でコライダーを再有効化
            float randomTime = Random.Range(5f, disableTime);
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

            reactionImage[reactionImageNum].transform.position = imagePos;
            //リアクション用吹き出しをtrueにする
            reactionImage[reactionImageNum].SetActive(true);

            // After 5 seconds, stop decreasing and reset the flag
            if (elapsedTime >= DurationTime)
            {
                inRoomflag = false;
                elapsedTime = 0f; // Reset timer

                // サウンドを流す
                if (audioSource != null && soundEffect1 != null && typeFlg)
                {
                    audioSource.PlayOneShot(soundEffect1);
                }

                if (typeFlg)
                {
                    // 条件が満たされた場合、コライダーを無効化
                    ToggleCollider(false);

                    // 住民を消去したいので、isUnlockedをfalseにする
                    isResidents = false;

                    // ランダムな時間でコライダーを再有効化
                    float randomTime = Random.Range(5f, disableTime);
                    StartCoroutine(ReenableColliderAfterTime(randomTime));

                    typeFlg = false;

                }
                if (reactionImage != null)
                {
                    reactionImage[reactionImageNum].SetActive(false);
                }

            }
        }

        // IsUnlocked が true であれば、子オブジェクトをアクティブにする
        if (isResidents)
        {
            if (childObject != null)
            {
                childObject.SetActive(true); // 子オブジェクトをアクティブにする
            }

        }
        else
        {
            if (childObject != null)
            {
                childObject.SetActive(false); // IsUnlocked が false なら、子オブジェクトを非アクティブにする
            }
        }

        if (inRoomflag && childSpriteRenderer != null && typeFlg)
        {
            childSpriteRenderer.sprite = residentAfterImages[nameOptions.IndexOf(nowState)];
        }
        else
        {
            childSpriteRenderer.sprite = residentBeforeImages[nameOptions.IndexOf(nowState)];
        }

    }

    public void AddResident(CS_DragandDrop character, float Duration)
    {
        if (!isUnlocked)
        {
            Debug.Log("This room is locked.");
            return; // 部屋が解放されていない場合は何もしない
        }

        // 初期化しておく
        //bonus_score = default_Point; // 新しい住民を追加するたびにスコアをリセットする（累積するため）
        //totalScore = default_Point;
        bonus_flag = false;

        // 妖怪の部屋占有時間を記録
        DurationTime = Duration;

        // キャラクターの特性とマッチするスコアを計算
        foreach (var roomAttribute in attributes)
        {
            foreach (var characterAttribute in character.characterAttributes)
            {
                //相性を照合する
                switch (compare.CompareCharactor(nameOptions.IndexOf(nowState), characterAttribute.attributeName))
                {
                    case 0:
                        // マッチした場合、スコアを累積する
                        bonus_score += roomAttribute.matchScore / 2 * 3;  // bonus_score に加算
                        totalScore += roomAttribute.matchScore / 2 * 3;  // totalScore に加算
                        bonus_flag = true;
                        typeFlg = true;
                        reactionImageNum = 0;
                        if (roomManager.inResident > 0)
                        {
                            roomManager.inResident--;
                        }
                        break;
                    case 1:
                        // マッチした場合、スコアを累積する
                        bonus_score += roomAttribute.matchScore;  // cp_score に加算
                        totalScore += roomAttribute.matchScore;  // totalScore に加算
                        bonus_flag = true;
                        typeFlg = true;
                        if (roomManager.inResident > 0)
                        {
                            roomManager.inResident--;
                        }
                        reactionImageNum = 1;
                        break;
                    case 2:
                        reactionImageNum = 2;
                        typeFlg = false;
                        break;
                    default:
                        break;

                }
            }
        }

        ResetGuide();

        // 結果をログに表示
        Debug.Log($"{character.name} matched with room {gameObject.name}, total score: {totalScore}");
    }

    private void ReSetIsResidents()
    {

        // 変更する数を10に設定
        int changeCount = Mathf.Min(10, attributes.Count); // 属性の数が5より少ない場合はそれを優先

        for (int i = 0; i < changeCount; i++)
        {
            // ランダムにリストから属性名を選択
            string randomName = nameOptions[Random.Range(0, nameOptions.Count)];

            // ランダムにRoomAttributeを選択してその属性名を変更
            RoomAttribute randomRoom = attributes[Random.Range(0, attributes.Count)];
            randomRoom.attributeName = randomName;
            nowState = randomName;
        }
    }
    public void finishPhase()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(totalScore, bonus_flag,transform.position);
        }
    }

    public void setinRoomflag(bool room)
    {
        inRoomflag = room;

        // サウンドを流す
        if (audioSource != null && soundEffect2 != null && typeFlg)
        {
            audioSource.PlayOneShot(soundEffect2);
        }
    }

    public bool GettinRoom()
    {
        return inRoomflag;
    }

    // コライダーを無効化 / 有効化するメソッド
    void ToggleCollider(bool isActive)
    {
        if (collider2D != null)
        {
            collider2D.enabled = isActive;
        }
    }

    // 一定時間後にコライダーを再度有効化するコルーチン
    System.Collections.IEnumerator ReenableColliderAfterTime(float time)
    {
        // 指定時間待機
        yield return new WaitForSeconds(time);

        // コライダーを再度有効化
        ToggleCollider(true);

        roomManager.inResident++;

        isResidents = true;

        ReSetIsResidents();
    }

    public void GuideType(CS_DragandDrop character)
    {
        if (isResidents && !inRoomflag)
        {
            // キャラクターの特性とマッチするスコアを計算
            foreach (var roomAttribute in attributes)
            {
                foreach (var characterAttribute in character.characterAttributes)
                {
                    //instance = Instantiate(guideObj);
                    //Instantiate(instance, this.transform.position, Quaternion.identity);

                    instance = Instantiate(guideObj);  // インスタンスを生成
                    instance.transform.position = this.transform.position;
                    instances.Add(instance);  // リストにインスタンスを追加
                    SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();

                    //相性を照合する
                    switch (compare.CompareCharactor(nameOptions.IndexOf(nowState), characterAttribute.attributeName))
                    {
                        //相性抜群の場合
                        case 0:
                            spriteRenderer.color = new Color(0f, 1f, 0f, 0.2f); // 緑色にする
                            break;
                        //相性普通の場合
                        case 1:
                            spriteRenderer.color = new Color(0.4f, 0.4f, 0f, 0.4f); // 青色にする
                            break;
                        //効果なしの場合
                        case 2:
                            spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f); // 青色にする
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
        if (isResidents)
        {
            if (instance != null)
            {
                // リストに保持されている全てのインスタンスを削除
                foreach (var inst in instances)
                {
                    Destroy(inst);  // インスタンスを削除
                }
                instances.Clear();  // リストをクリア            
            }
        }
    }
}