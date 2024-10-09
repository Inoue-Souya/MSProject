using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resident
{
    public string name;
    public string personality; // «Ši
    public int age;
    public string gender; // «•Ê
    public string info; // ‚»‚Ì‘¼‚Ìî•ñ
    public Sprite portrait; // Z–¯‚Ì‰æ‘œ
}

[System.Serializable]
public class ResidentRequest
{
    public string personality; // «Ši
    public int age; // ”N—î
    public string gender; // «•Ê

    public ResidentRequest(string personality, int age, string gender)
    {
        this.personality = personality;
        this.age = age;
        this.gender = gender;
    }
}