using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SelectResidentDate : MonoBehaviour
{
    public static CS_SelectResidentDate Instance { get; private set; }

    public Resident selectedResident;

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
}
