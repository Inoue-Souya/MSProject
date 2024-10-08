using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理用
using UnityEngine.UI; // UI用

public class CS_Changescene : MonoBehaviour
{
    public Button transitionButton; // ボタンの参照

    private void Start()
    {
        // ボタンにクリックイベントを追加
        transitionButton.onClick.AddListener(OnTransitionButtonClick);
    }

    private void OnTransitionButtonClick()
    {
        string currentScene = SceneManager.GetActiveScene().name; // 現在のシーン名を取得

        switch (currentScene)
        {
            //case "TitleScene":
            //    SceneManager.LoadScene("GameScene"); // TitleSceneからGameSceneへ
            //     break;
            case "GameScene":
                SceneManager.LoadScene("ApartmentScene"); // GameSceneからApartmentSceneへ
                break;
            case "ApartmentScene":
                SceneManager.LoadScene("GameScene"); // ApartmentSceneからGameSceneへ
                break;
            //case "ResultScene":
            //    SceneManager.LoadScene("TitleScene"); // ResultSceneからTitleSceneへ
            //    break;
            default:
                Debug.LogWarning("Unhandled scene: " + currentScene);
                break;
        }
    }
}
