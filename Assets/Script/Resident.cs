using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resident
{
    public string name;
    public string personality; // ���i
    public int age;
    public string gender; // ����
    public string info; // ���̑��̏��
    public Sprite portrait; // �Z���̉摜
}

[System.Serializable]
public class ResidentRequest
{
    public string personality; // ���i
    public int age; // �N��
    public string gender; // ����

    public ResidentRequest(string personality, int age, string gender)
    {
        this.personality = personality;
        this.age = age;
        this.gender = gender;
    }
}