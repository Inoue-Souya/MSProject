using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EventSceneController : MonoBehaviour
{
    public GameObject myPrefab; // 表示するPrefab
    private GameObject currentInstance; // 現在表示中のPrefab

    // 「表示」ボタンが押されたときの処理
    public void OnShowButtonClicked()
    {
        if (currentInstance == null)
        {
            //// Prefabをインスタンス化
            //currentInstance = Instantiate(myPrefab);
            //CS_EventHandler eventHandler = currentInstance.GetComponent<CS_EventHandler>();

            //// イベントをトリガー
            //eventHandler.TriggerEvent();

            //// パラメータを取得
            //int result = eventHandler.GetParameter();
            //Debug.Log("Returned Parameter: " + result);
        }
    }

    // 「戻る」ボタンが押されたときの処理
    public void OnBackButtonClicked()
    {
        if (currentInstance != null)
        {
            // 表示中のPrefabを削除
            Destroy(currentInstance);
            currentInstance = null;
        }
    }
}