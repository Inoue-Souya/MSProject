using UnityEngine;
using UnityEngine.UI; // UnityのUI Textを使用

public class CS_RandomMoneyText : MonoBehaviour
{

    public CS_MoneyManager MoneyManager;
    public void  OnButtonClick()
    {
        // 1000, 5000, 10000のいずれかをランダムに選ぶ
        int randomMoney = GetRandomMoney();

        MoneyManager.AddMoney(randomMoney);
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
