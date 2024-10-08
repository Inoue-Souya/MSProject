using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectedResidentData
{
    public Resident selectedResident;
    public static SelectedResidentData Instance { get; private set; } = new SelectedResidentData();
}