using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EventHandler : MonoBehaviour
{
    private int myParameter;

    public void TriggerEvent()
    {
        // �����ŃC�x���g�������s���A�p�����[�^��ύX����
        myParameter += 10; // ��: �p�����[�^��10���₷
    }

    public int GetParameter()
    {
        return myParameter;
    }
}
