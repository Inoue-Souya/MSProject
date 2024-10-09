using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_ResidentRequestManager : MonoBehaviour
{
    public Text requestText; // ダイアログに表示するテキスト
    public Button nextButton; // 次の要望ボタン
    private ResidentRequest[] requests;
    private int currentIndex = 0;

    void Start()
    {
        // サンプルの要望を作成
        requests = new ResidentRequest[]
        {
            new ResidentRequest("善", 25, "男"),
            new ResidentRequest("悪", 28, "男"),
            new ResidentRequest("善", 30, "女")
        };

        // 最初の要望を表示
        DisplayRequest(currentIndex);

        // ボタンのクリックイベントを設定
        nextButton.onClick.AddListener(NextRequest);
    }

    private void DisplayRequest(int index)
    {
        ResidentRequest request = requests[index];
        requestText.text = $"性格: {request.personality}\n年齢: {request.age}\n性別: {request.gender}";
    }

    private void NextRequest()
    {
        currentIndex++;
        if (currentIndex >= requests.Length)
        {
            currentIndex = 0; // 最初に戻る
        }
        DisplayRequest(currentIndex);
    }
}
