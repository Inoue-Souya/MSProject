using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_MoneyManager : MonoBehaviour
{
    public static CS_MoneyManager Instance { get; private set; }

    private float currentMoney;
    public Text moneyText; // UI��Text�R���|�[�l���g

    private void Start()
    {
        // 1000, 5000, 10000�̂����ꂩ�������_���ɑI��
        int randomMoney = GetRandomMoney();

        // �ŏ��̏������Ƃ��Ďx��
        currentMoney = randomMoney;

        // Text�ɑI�΂ꂽ���z��\��
        moneyText.text = $"����: {currentMoney}";
    }

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

        //currentMoney = 1000f; // �������z
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        moneyText.text = $"����: {currentMoney}";
        //CS_UIManager.Instance.UpdateMoney(currentMoney); // UI���X�V
    }

    public void DecreaseMoney(float amount)
    {
        currentMoney -= amount;
        moneyText.text = $"����: {currentMoney}";
        //CS_UIManager.Instance.UpdateMoney(currentMoney); // UI���X�V
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"����: {currentMoney}"; // ������\��
        }
    }

    public float GetMoney()
    {
        return currentMoney;
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
