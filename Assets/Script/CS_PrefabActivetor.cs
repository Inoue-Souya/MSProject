using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PrefabActivetor : MonoBehaviour
{
    public GameObject prefab; // ヒエラルキー上のプレハブを参照

    // ボタンが押されたときに呼び出すメソッド
    public void ActivatePrefab()
    {
        if (prefab != null)
        {
            prefab.SetActive(true); // プレハブを有効にする
        }

    }

    public void NotActivatePrefab()
    {
       prefab.SetActive(false); // プレハブを無効にする
    }
}
