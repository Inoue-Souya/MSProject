using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_UIManager : MonoBehaviour
{
    public static CS_UIManager Instance { get; private set; }
    public Text moneyText; // ������\������Text�R���|�[�l���g

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMoney(float amount)
    {
        moneyText.text = $"����: {amount}";
    }
}
