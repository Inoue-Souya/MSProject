using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_NewResidentManager : MonoBehaviour
{
    private Resident[] residents;
    private int currentIndex = 0;

    public Text name;
    //public Text age;
    //public Text gender;
    //public Text personality;
    //public Text info;
    public Image portraitImage;

    void Start()
    {
        residents = new Resident[]
        {
            new Resident
            {
                name = "“c’†",
                age = 25,
                gender = "’j",
                personality = "‘P",
                info = "101‚ÌZ–¯",
                portrait = Resources.Load<Sprite>("Images/RImage01")
            },
            new Resident
            {
                name = "²“¡",
                age = 28,
                gender = "’j",
                personality = "ˆ«",
                info = "102‚ÌZ–¯",
                portrait = Resources.Load<Sprite>("Images/RImage02")
            },
            new Resident
            {
                name = "R–{",
                age = 25,
                gender = "—",
                personality = "‘P",
                info = "103‚ÌZ–¯",
                portrait = Resources.Load<Sprite>("Images/RImage03")
            },
            new Resident
            {
                name = "R–{",
                age = 25,
                gender = "—",
                personality = "‘P",
                info = "103‚ÌZ–¯",
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
            currentIndex = 0; // Å‰‚É–ß‚é
        }
        DisplayResidentInfo(currentIndex);
    }

    private void DisplayResidentInfo(int index)
    {
        Resident resident = residents[index];
        name.text = resident.name;
        //age.text = resident.age.ToString();
        //gender.text = resident.gender;
        //personality.text = resident.personality;
        //info.text = resident.info;
        portraitImage.sprite = resident.portrait;
    }
}