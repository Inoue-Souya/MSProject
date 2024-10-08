using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_MoneyManager : MonoBehaviour
{
    public static CS_MoneyManager Instance { get; private set; }

    public float currentMoney;
    public Text moneyText; // UIのTextコンポーネント

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも消えないようにする
        }
        else
        {
            Destroy(gameObject); // 既に存在する場合は破棄
        }

        currentMoney = 1000f; // 初期金額
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        CS_UIManager.Instance.UpdateMoney(currentMoney); // UIを更新
    }
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"お金: {currentMoney}"; // お金を表示
        }
    }
}
