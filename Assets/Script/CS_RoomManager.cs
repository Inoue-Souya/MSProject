using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CS_RoomManager : MonoBehaviour
{
    //public CS_RoomManager Instance { get; private set; }

    private float ICost;          //増築費用
    public float currentRoom;     // 現在の最大部屋数
    public float EmptyRoom; //空き部屋の数
    public Text RoomText; // UIのTextコンポーネント
    public float Cost;    //維持費の変数
    public CS_MoneyManager MoneyManager; //お金の管理変数
    public Button MButton; //入居ボタン
    public Button IButton; //増築ボタン
    //public Button LButton; //退去ボタン

    // Start is called before the first frame update
    void Start()
    {

        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject); // シーンを跨いでも消えないようにする
        //}
        //else
        //{
        //    Destroy(gameObject); // 既に存在する場合は破棄
        //}

        currentRoom = 5f; // 現在の最大部屋数

        EmptyRoom = currentRoom;  //空き部屋の数

        Cost = 8000;                //維持費の変数

        ICost = 1000f;               //増築費用

        RoomText.text = $"部屋: {EmptyRoom}";
    }

    
    public void Movein()    //入居時の処理
    {
        EmptyRoom -= 1;
        UpdateRoomText();
    }

    ///*public void Leaving()　　//退去時の処理
    //{
    //    EmptyRoom += 1;
    //    MoneyManager.AddMoney(1000);      //退去時に増えるお金
    //    UpdateRoomText();
    //}*/


    public void Extension(float amount)　//増築時の処理
    {
        currentRoom += amount;
        EmptyRoom += amount;
        MoneyManager.DecreaseMoney(ICost);  //増築費用
        Cost += 1000;                      //維持費用
        UpdateRoomText();
    }

    private void UpdateRoomText()　　　　// 空き部屋数を表示
    {
        if (RoomText != null)
        {
            RoomText.text = $"部屋: {EmptyRoom}"; 
        }
    }

    private void Update()
    {
        // 入居ボタンの有効化/無効化
        if (MButton != null)
        {
            if (EmptyRoom <= 0)
            {
                Button bt = MButton.GetComponent<Button>();
                bt.interactable = false;
            }
            else
            {
                Button bt = MButton.GetComponent<Button>();
                bt.interactable = true;
            }
        }

        // 入居ボタンの有効化/無効化
        if (IButton != null)
        {
            if (MoneyManager.GetMoney() < ICost)
            {
                Button bt = IButton.GetComponent<Button>();
                bt.interactable = false;
            }
            else
            {
                Button bt = IButton.GetComponent<Button>();
                bt.interactable = true;
            }
        }
        // 退去ボタンの有効化/無効化
        //if (LButton != null)
        //{
        //    if (EmptyRoom == currentRoom)
        //    {
        //        Button bt = LButton.GetComponent<Button>();
        //        bt.interactable = false;
        //    }
        //    else
        //    {
        //        Button bt = LButton.GetComponent<Button>();
        //        bt.interactable = true;
        //    }
        //}
        //else
        //{
        //    Debug.LogError("LButton is not assigned in the Inspector.");
        //}
    }
}
