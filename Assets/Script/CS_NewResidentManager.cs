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
                name = "�c��",
                age = 25,
                gender = "�j",
                personality = "�P",
                portrait = Resources.Load<Sprite>("Images/RImage01")
            },
            new Resident
            {
                name = "����",
                age = 28,
                gender = "�j",
                personality = "��",
                portrait = Resources.Load<Sprite>("Images/RImage02")
            },
            new Resident
            {
                name = "�R�{",
                age = 25,
                gender = "��",
                personality = "�P",
                portrait = Resources.Load<Sprite>("Images/RImage03")
            },
            new Resident
            {
                name = "���",
                age = 30,
                gender = "��",
                personality = "��",
                portrait = Resources.Load<Sprite>("Images/RImage04")
            }
        };

        DisplayResidentInfo(currentIndex);
    }

    public float EvaluateResident(Resident resident, ResidentRequest request)
    {
        float score = 0;

        // ���i�̈�v
        if (resident.personality == request.personality) score += 1;
        // �N��͈̔͂̃`�F�b�N
        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
        // ���ʂ̈�v
        if (resident.gender == request.gender) score += 1;

        //// �X�R�A�Ɋ�Â��Ă����𑝂₷����
        //CS_MoneyManager.Instance.AddMoney(score * 10000); // �X�R�A�ɉ����Ă����𑝉��i��: 1�_���Ƃ�10000�~������j

        return score; // �X�R�A��Ԃ�
    }

    public void NextResident()
    {
        currentIndex++;
        if (currentIndex >= residents.Length)
        {
            currentIndex = 0; // �ŏ��ɖ߂�
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