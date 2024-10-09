using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_MoneyManager : MonoBehaviour
{
    public static CS_MoneyManager Instance { get; private set; }

    private float currentMoney;
    public Text moneyText; // UIのTextコンポーネント

    private void Start()
    {
        // 1000, 5000, 10000のいずれかをランダムに選ぶ
        int randomMoney = GetRandomMoney();

        // 最初の所持金として支給
        currentMoney = randomMoney;

        // Textに選ばれた金額を表示
        moneyText.text = $"お金: {currentMoney}";
    }

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

        //currentMoney = 1000f; // 初期金額
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        moneyText.text = $"お金: {currentMoney}";
        //CS_UIManager.Instance.UpdateMoney(currentMoney); // UIを更新
    }

    public void DecreaseMoney(float amount)
    {
        currentMoney -= amount;
        moneyText.text = $"お金: {currentMoney}";
        //CS_UIManager.Instance.UpdateMoney(currentMoney); // UIを更新
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"お金: {currentMoney}"; // お金を表示
        }
    }

    public float GetMoney()
    {
        return currentMoney;
    }
    

    // 1000, 5000, 10000のいずれかをランダムに返すメソッド
    private int GetRandomMoney()
    {
        int[] moneyValues = { 1000, 5000, 10000 };
        // UnityEngine.Random.Rangeを明示的に使用する
        int randomIndex = UnityEngine.Random.Range(0, moneyValues.Length);
        return moneyValues[randomIndex];
    }

}
