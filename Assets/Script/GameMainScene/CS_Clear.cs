using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CS_Clear : MonoBehaviour
{
    CS_Room room;

    // Start is called before the first frame update
    void Start()
    {
        room = gameObject.GetComponent<CS_Room>();
    }

    // Update is called once per frame
    void Update()
    {
        if(room.isUnlocked)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
