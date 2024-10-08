using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_AddInfoResident : MonoBehaviour
{
    public void OnResidentSelected(Resident resident)
    {
        SelectedResidentData.Instance.selectedResident = resident;
        SceneManager.LoadScene("ApartmentScene");
    }
}
