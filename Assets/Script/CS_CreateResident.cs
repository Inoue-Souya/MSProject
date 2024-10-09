using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resident", menuName = "Residents/New Resident")]
public class CS_CreateResident : ScriptableObject
{
    public string name;
    public int age;
    public string gender;
    public string personality;
    public string info;
    public Sprite portrait;
}