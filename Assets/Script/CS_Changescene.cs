using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���Ǘ��p
using UnityEngine.UI; // UI�p

public class CS_Changescene : MonoBehaviour
{
    public Button transitionButton; // �{�^���̎Q��

    private void Start()
    {
        // �{�^���ɃN���b�N�C�x���g��ǉ�
        transitionButton.onClick.AddListener(OnTransitionButtonClick);
    }

    private void OnTransitionButtonClick()
    {
        string currentScene = SceneManager.GetActiveScene().name; // ���݂̃V�[�������擾

        switch (currentScene)
        {
            case "TitleScene":
                SceneManager.LoadScene("GameMainScene"); // TitleScene����GameScene��
                break;
            case "GameMainScene":
                SceneManager.LoadScene("ResultScene"); // GameScene����ApartmentScene��
                break;
            case "ResultScene":
                SceneManager.LoadScene("TitleScene"); // ResultScene����TitleScene��
                break;
            default:
                Debug.LogWarning("Unhandled scene: " + currentScene);
                break;
        }
    }
}
