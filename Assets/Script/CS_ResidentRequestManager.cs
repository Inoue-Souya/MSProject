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
        //requests = new ResidentRequest[]
        //{
        //    new ResidentRequest("善", 25, "男"),
        //    new ResidentRequest("悪", 28, "男"),
        //    new ResidentRequest("善", 30, "女")
        //};

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

    public void NextRequest()
    {
        currentIndex++;
        if (currentIndex >= requests.Length)
        {
            currentIndex = 0; // 最初に戻る
        }
        DisplayRequest(currentIndex);
    }

    private int CalculateScore(Resident resident, ResidentRequest request)
    {
        int score = 0;

        // 性格の照合
        if (resident.personality == request.personality)
        {
            score += 1; // 性格が合致
        }

        // 年齢の照合
        if (resident.age == request.age)
        {
            score += 1; // 年齢が合致
        }

        // 性別の照合
        if (resident.gender == request.gender)
        {
            score += 1; // 性別が合致
        }

        return score; // 合致した分のスコアを返す
    }
}
