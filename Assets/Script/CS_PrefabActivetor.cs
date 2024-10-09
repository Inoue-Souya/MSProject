using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PrefabActivetor : MonoBehaviour
{
    public GameObject prefab; // �q�G�����L�[��̃v���n�u���Q��

    // �{�^���������ꂽ�Ƃ��ɌĂяo�����\�b�h
    public void ActivatePrefab()
    {
        if (prefab != null)
        {
            prefab.SetActive(true); // �v���n�u��L���ɂ���
        }

    }

    public void NotActivatePrefab()
    {
       prefab.SetActive(false); // �v���n�u�𖳌��ɂ���
    }
}
