using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EventSceneController : MonoBehaviour
{
    public GameObject myPrefab; // �\������Prefab
    private GameObject currentInstance; // ���ݕ\������Prefab

    // �u�\���v�{�^���������ꂽ�Ƃ��̏���
    public void OnShowButtonClicked()
    {
        if (currentInstance == null)
        {
            //// Prefab���C���X�^���X��
            //currentInstance = Instantiate(myPrefab);
            //CS_EventHandler eventHandler = currentInstance.GetComponent<CS_EventHandler>();

            //// �C�x���g���g���K�[
            //eventHandler.TriggerEvent();

            //// �p�����[�^���擾
            //int result = eventHandler.GetParameter();
            //Debug.Log("Returned Parameter: " + result);
        }
    }

    // �u�߂�v�{�^���������ꂽ�Ƃ��̏���
    public void OnBackButtonClicked()
    {
        if (currentInstance != null)
        {
            // �\������Prefab���폜
            Destroy(currentInstance);
            currentInstance = null;
        }
    }
}