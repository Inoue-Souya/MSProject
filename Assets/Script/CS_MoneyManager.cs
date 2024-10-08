using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_MoneyManager : MonoBehaviour
{
    public static CS_MoneyManager Instance { get; private set; }

    public float currentMoney;
    public Text moneyText; // UI��Text�R���|�[�l���g

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����ׂ��ł������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // ���ɑ��݂���ꍇ�͔j��
        }

        currentMoney = 1000f; // �������z
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        CS_UIManager.Instance.UpdateMoney(currentMoney); // UI���X�V
    }
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"����: {currentMoney}"; // ������\��
        }
    }
}
