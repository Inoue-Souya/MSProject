using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_NewResidentManager : MonoBehaviour
{
    private Resident[] residents;
    private int currentIndex = 0;

    public Text name;
    public Text age;
    public Text gender;
    public Text personality;
    public Image portraitImage;

    void Start()
    {
        residents = new Resident[]
        {
            new Resident
            {
                name = "田中",
                age = 25,
                gender = "男",
                personality = "善",
                portrait = Resources.Load<Sprite>("Images/RImage01")
            },
            new Resident
            {
                name = "佐藤",
                age = 28,
                gender = "男",
                personality = "悪",
                portrait = Resources.Load<Sprite>("Images/RImage02")
            },
            new Resident
            {
                name = "山本",
                age = 25,
                gender = "女",
                personality = "善",
                portrait = Resources.Load<Sprite>("Images/RImage03")
            },
            new Resident
            {
                name = "鈴木",
                age = 30,
                gender = "女",
                personality = "悪",
                portrait = Resources.Load<Sprite>("Images/RImage04")
            }
        };

        DisplayResidentInfo(currentIndex);
    }

    public void NextResident()
    {
        currentIndex++;
        if (currentIndex >= residents.Length)
        {
            currentIndex = 0; // 最初に戻る
        }
        DisplayResidentInfo(currentIndex);
    }

    private void DisplayResidentInfo(int index)
    {
        Resident resident = residents[index];
        name.text = resident.name;
        age.text = resident.age.ToString();
        gender.text = resident.gender;
        personality.text = resident.personality;
        portraitImage.sprite = resident.portrait;
    }
}