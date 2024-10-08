using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EventHandler : MonoBehaviour
{
    private int myParameter;

    public void TriggerEvent()
    {
        // ここでイベント処理を行い、パラメータを変更する
        myParameter += 10; // 例: パラメータを10増やす
    }

    public int GetParameter()
    {
        return myParameter;
    }
}
