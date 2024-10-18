//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;
//using UnityEngine.UI;

//public class CS_NewResidentManager : MonoBehaviour
//{
//    private Resident[] residents;
//    private int currentIndex = 0;

//    public Text name;
//    public Text age;
//    public Text gender;
//    public Text personality;
//    public Image portraitImage;

//    public Button addButton; // �{�^�����C���X�y�N�^����ݒ�

//    void Start()
//    {
//        residents = new Resident[]
//        {
//            new Resident
//            {
//                name = "�c��",
//                age = 25,
//                gender = "�j",
//                personality = "�P",
//                portrait = Resources.Load<Sprite>("Images/RImage01")
//            },
//            new Resident
//            {
//                name = "����",
//                age = 28,
//                gender = "�j",
//                personality = "��",
//                portrait = Resources.Load<Sprite>("Images/RImage02")
//            },
//            new Resident
//            {
//                name = "�R�{",
//                age = 25,
//                gender = "��",
//                personality = "�P",
//                portrait = Resources.Load<Sprite>("Images/RImage03")
//            },
//            new Resident
//            {
//                name = "���",
//                age = 30,
//                gender = "��",
//                personality = "��",
//                portrait = Resources.Load<Sprite>("Images/RImage04")
//            }

//        };



//        DisplayResidentInfo(currentIndex);
//    }

//    void ListResidents()
//    {
//        for (int i = 0; i < residents.Length; i++)
//        {
//            Resident resident = residents[i];
//            UnityEngine.Debug.Log($"Resident {i}: Name = {resident.name}, Age = {resident.age}, Gender = {resident.gender}, Personality = {resident.personality}");
//        }
//    }

//    public void OnButtonClick()
//    {
//        // �V�������Z�҂�ǉ�
//        AddNewResident("����", 21, "�j", "�P", "Images/RImage04");

//        // Resident���X�g�̏���\��
//        ListResidents();

//        // �z��̃T�C�Y���m�F
//        UnityEngine.Debug.Log($"���݂̔z��̒���: {residents.Length}");

//        UnityEngine.Debug.Log("�{�^����������܂���");
//    }

//    void AddNewResident(string residentName, int residentAge, string residentGender, string residentPersonality, string portraitPath)
//    {
//        Resident newResident = new Resident
//        {
//            name = residentName,
//            age = residentAge,
//            gender = residentGender,
//            personality = residentPersonality,
//            portrait = Resources.Load<Sprite>(portraitPath)
//        };

//        // residents�z��ɐV����Resident��ǉ�
//        int oldLength = residents.Length; // ���̒�����ۑ�
//        Array.Resize(ref residents, residents.Length + 1); // �z��̃T�C�Y�𑝉�
//        residents[residents.Length - 1] = newResident; // �V�������Z�҂��Ō�ɒǉ�

//        // �z��̃T�C�Y�������������Ƃ��m�F
//        UnityEngine.Debug.Log($"�z��̒����� {oldLength} ���� {residents.Length} �ɑ������܂����B");

//        // �V�������Z�҂̏����m�F
//        UnityEngine.Debug.Log($"�ǉ����ꂽ���Z��: Name = {newResident.name}, Age = {newResident.age}, Gender = {newResident.gender}, Personality = {newResident.personality}");
//    }



//    public float EvaluateResident(Resident resident, ResidentRequest request)
//    {
//        float score = 0;

//        // ���i�̈�v
//        if (resident.personality == request.personality) score += 1;
//        // �N��͈̔͂̃`�F�b�N
//        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
//        // ���ʂ̈�v
//        if (resident.gender == request.gender) score += 1;

//        //// �X�R�A�Ɋ�Â��Ă����𑝂₷����
//        //CS_MoneyManager.Instance.AddMoney(score * 10000); // �X�R�A�ɉ����Ă����𑝉��i��: 1�_���Ƃ�10000�~������j

//        return score; // �X�R�A��Ԃ�
//    }

//    public void NextResident()
//    {
//        currentIndex++;
//        if (currentIndex >= residents.Length)
//        {
//            currentIndex = 0; // �ŏ��ɖ߂�
//        }
//        UnityEngine.Debug.Log("���X�g�ԍ�"+currentIndex);
//        UnityEngine.Debug.Log("���X�g�̒���"+residents.Length);
//        DisplayResidentInfo(currentIndex);
//    }

//    private void DisplayResidentInfo(int index)
//    {
//        Resident resident = residents[index];
//        name.text = resident.name;
//        age.text = resident.age.ToString();
//        gender.text = resident.gender;
//        personality.text = resident.personality;
//        portraitImage.sprite = resident.portrait;
//    }
//}

using System;
using System.Collections.Generic; // ���X�g���g�p���邽�߂ɕK�v
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class CS_NewResidentManager : MonoBehaviour
{
    private List<Resident> residents = new List<Resident>(); // �z�񂩂烊�X�g�ɕύX
    private int currentIndex = 0;

    public Text name;
    public Text age;
    public Text gender;
    public Text personality;
    public Image portraitImage;

    public Button addButton; // �{�^�����C���X�y�N�^����ݒ�
    public Button nextButton;
    private ResidentRequest[] requests;

    void Start()
    {
        // �����̋��Z�҂����X�g�ɒǉ�
        residents.Add(new Resident
        {
            name = "�c��",
            age = 25,
            gender = "�j",
            personality = "�P",
            portrait = Resources.Load<Sprite>("Images/RImage01")
        });

        residents.Add(new Resident
        {
            name = "����",
            age = 28,
            gender = "�j",
            personality = "��",
            portrait = Resources.Load<Sprite>("Images/RImage02")
        });

        residents.Add(new Resident
        {
            name = "�R�{",
            age = 25,
            gender = "��",
            personality = "�P",
            portrait = Resources.Load<Sprite>("Images/RImage03")
        });

        residents.Add(new Resident
        {
            name = "���",
            age = 30,
            gender = "��",
            personality = "��",
            portrait = Resources.Load<Sprite>("Images/RImage04")
        });

        DisplayResidentInfo(currentIndex);
    }

    // ���X�g���̑S���Z�҂�\��
    void ListResidents()
    {
        for (int i = 0; i < residents.Count; i++)
        {
            Resident resident = residents[i];
            UnityEngine.Debug.Log($"Resident {i}: Name = {resident.name}, Age = {resident.age}, Gender = {resident.gender}, Personality = {resident.personality}");
            UnityEngine.Debug.Log($"NextResident���s�� - ���X�g�̒���: {residents.Count}");
        }
    }

    public void OnButtonClick()
    {
        // �V�������Z�҂�ǉ�
        AddNewResident("�R�c", 21, "�j", "�P", "Images/RImage01");

        // Resident���X�g�̏���\��
        ListResidents();


        UnityEngine.Debug.Log("�{�^����������܂���");
    }

    public void OnButtonClickNext()
    {
        NextResident();
    }

    public void RemoveCurrentResident()
    {
        if (residents.Count == 0) return; // �Z�������Ȃ��ꍇ�͉������Ȃ�

        // ���݂̏Z�����폜
        Resident selectedResident = residents[currentIndex];

        // �Z�����폜
        RemoveResidentAt(currentIndex);

        // ���̏Z����\��
        NextResident();

    }

    // �V�������Z�҂����X�g�ɒǉ�����֐�
    void AddNewResident(string residentName, int residentAge, string residentGender, string residentPersonality, string portraitPath)
    {
        Resident newResident = new Resident
        {
            name = residentName,
            age = residentAge,
            gender = residentGender,
            personality = residentPersonality,
            portrait = Resources.Load<Sprite>(portraitPath)
        };

        // residents���X�g�ɐV����Resident��ǉ�
        residents.Add(newResident);

        // ���X�g�̃T�C�Y�������������Ƃ��m�F
        UnityEngine.Debug.Log($"AddNewResident���s��̃��X�g�̒���: {residents.Count}");
    }

    // ���Z�ҕ]���֐��i���X�g�̑��̕����Ŏg�p����\������j
    public float EvaluateResident(Resident resident, ResidentRequest request)
    {
        float score = 0;

        // ���i�̈�v
        if (resident.personality == request.personality) score += 1;
        // �N��͈̔͂̃`�F�b�N
        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
        // ���ʂ̈�v
        if (resident.gender == request.gender) score += 1;

        //// �X�R�A�Ɋ�Â��Ă����𑝂₷�����i�K�v�ɉ����āj
        CS_MoneyManager.Instance.AddMoney(score * 10000);

        return score; // �X�R�A��Ԃ�
    }

    // ���̋��Z�҂�\��
    public void NextResident()
    {

        currentIndex++;
        if (currentIndex >= residents.Count) // .Length ���� .Count �ɕύX
        {
            currentIndex = 0; // �ŏ��ɖ߂�
        }

        // ���݂̃��X�g�ԍ��ƒ�����\��
        UnityEngine.Debug.Log($"NextResident���s�� - ���X�g�ԍ�: {currentIndex}");
        UnityEngine.Debug.Log($"NextResident���s�� - ���X�g�̒���: {residents.Count}");

        DisplayResidentInfo(currentIndex);
    }

    // ���Z�҂̏���UI�ɕ\��
    private void DisplayResidentInfo(int index)
    {
        Resident resident = residents[index];
        name.text = resident.name;
        age.text = resident.age.ToString();
        gender.text = resident.gender;
        personality.text = resident.personality;
        portraitImage.sprite = resident.portrait;
    }

    public void RemoveResidentAt(int index)
    {
        // �V�������X�g���쐬
        List<Resident> newResidents = new List<Resident>();

        // �Z�����폜
        for (int i = 0; i < residents.Count; i++)
        {
            if (i != index)
            {
                newResidents.Add(residents[i]);
            }
        }

        // residents��V�������X�g�ɍX�V
        residents = newResidents;
        currentIndex = Mathf.Clamp(currentIndex, 0, residents.Count - 1); // �C���f�b�N�X�𒲐�    }
    }

    public Resident GetCurrentResident()
    {
        return residents[currentIndex];
    }
}
