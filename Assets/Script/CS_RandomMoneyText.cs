using UnityEngine;
using UnityEngine.UI; // Unity��UI Text���g�p

public class CS_RandomMoneyText : MonoBehaviour
{

    public CS_MoneyManager MoneyManager;
    public void  OnButtonClick()
    {
        // 1000, 5000, 10000�̂����ꂩ�������_���ɑI��
        int randomMoney = GetRandomMoney();

        MoneyManager.AddMoney(randomMoney);
    }

    // 1000, 5000, 10000�̂����ꂩ�������_���ɕԂ����\�b�h
    private int GetRandomMoney()
    {
        int[] moneyValues = { 1000, 5000, 10000 };
        // UnityEngine.Random.Range�𖾎��I�Ɏg�p����
        int randomIndex = UnityEngine.Random.Range(0, moneyValues.Length);
        return moneyValues[randomIndex];
    }

}
