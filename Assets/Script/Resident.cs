using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resident
{
    public string name;
    public string personality; // 性格
    public int age;
    public string gender; // 性別
    public string info; // その他の情報
    public Sprite portrait; // 住民の画像
}

[System.Serializable]
public class ResidentRequest
{
    public string personality; // 性格
    public int age; // 年齢
    public string gender; // 性別
    public string info; // その他の情報
}