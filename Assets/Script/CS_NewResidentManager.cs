//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;
//using UnityEngine.UI;

//public class CS_NewResidentManager : MonoBehaviour
//{
//    private Resident[] residents;
//    private int currentIndex = 0;

//    public Text name;
//    public Text age;
//    public Text gender;
//    public Text personality;
//    public Image portraitImage;

//    public Button addButton; // ボタンをインスペクタから設定

//    void Start()
//    {
//        residents = new Resident[]
//        {
//            new Resident
//            {
//                name = "田中",
//                age = 25,
//                gender = "男",
//                personality = "善",
//                portrait = Resources.Load<Sprite>("Images/RImage01")
//            },
//            new Resident
//            {
//                name = "佐藤",
//                age = 28,
//                gender = "男",
//                personality = "悪",
//                portrait = Resources.Load<Sprite>("Images/RImage02")
//            },
//            new Resident
//            {
//                name = "山本",
//                age = 25,
//                gender = "女",
//                personality = "善",
//                portrait = Resources.Load<Sprite>("Images/RImage03")
//            },
//            new Resident
//            {
//                name = "鈴木",
//                age = 30,
//                gender = "女",
//                personality = "悪",
//                portrait = Resources.Load<Sprite>("Images/RImage04")
//            }

//        };



//        DisplayResidentInfo(currentIndex);
//    }

//    void ListResidents()
//    {
//        for (int i = 0; i < residents.Length; i++)
//        {
//            Resident resident = residents[i];
//            UnityEngine.Debug.Log($"Resident {i}: Name = {resident.name}, Age = {resident.age}, Gender = {resident.gender}, Personality = {resident.personality}");
//        }
//    }

//    public void OnButtonClick()
//    {
//        // 新しい居住者を追加
//        AddNewResident("加藤", 21, "男", "善", "Images/RImage04");

//        // Residentリストの情報を表示
//        ListResidents();

//        // 配列のサイズを確認
//        UnityEngine.Debug.Log($"現在の配列の長さ: {residents.Length}");

//        UnityEngine.Debug.Log("ボタンが押されました");
//    }

//    void AddNewResident(string residentName, int residentAge, string residentGender, string residentPersonality, string portraitPath)
//    {
//        Resident newResident = new Resident
//        {
//            name = residentName,
//            age = residentAge,
//            gender = residentGender,
//            personality = residentPersonality,
//            portrait = Resources.Load<Sprite>(portraitPath)
//        };

//        // residents配列に新しいResidentを追加
//        int oldLength = residents.Length; // 元の長さを保存
//        Array.Resize(ref residents, residents.Length + 1); // 配列のサイズを増加
//        residents[residents.Length - 1] = newResident; // 新しい居住者を最後に追加

//        // 配列のサイズが増加したことを確認
//        UnityEngine.Debug.Log($"配列の長さが {oldLength} から {residents.Length} に増加しました。");

//        // 新しい居住者の情報を確認
//        UnityEngine.Debug.Log($"追加された居住者: Name = {newResident.name}, Age = {newResident.age}, Gender = {newResident.gender}, Personality = {newResident.personality}");
//    }



//    public float EvaluateResident(Resident resident, ResidentRequest request)
//    {
//        float score = 0;

//        // 性格の一致
//        if (resident.personality == request.personality) score += 1;
//        // 年齢の範囲のチェック
//        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
//        // 性別の一致
//        if (resident.gender == request.gender) score += 1;

//        //// スコアに基づいてお金を増やす処理
//        //CS_MoneyManager.Instance.AddMoney(score * 10000); // スコアに応じてお金を増加（例: 1点ごとに10000円増える）

//        return score; // スコアを返す
//    }

//    public void NextResident()
//    {
//        currentIndex++;
//        if (currentIndex >= residents.Length)
//        {
//            currentIndex = 0; // 最初に戻る
//        }
//        UnityEngine.Debug.Log("リスト番号"+currentIndex);
//        UnityEngine.Debug.Log("リストの長さ"+residents.Length);
//        DisplayResidentInfo(currentIndex);
//    }

//    private void DisplayResidentInfo(int index)
//    {
//        Resident resident = residents[index];
//        name.text = resident.name;
//        age.text = resident.age.ToString();
//        gender.text = resident.gender;
//        personality.text = resident.personality;
//        portraitImage.sprite = resident.portrait;
//    }
//}

using System;
using System.Collections.Generic; // リストを使用するために必要
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class CS_NewResidentManager : MonoBehaviour
{
    private List<Resident> residents = new List<Resident>(); // 配列からリストに変更
    private int currentIndex = 0;

    public Text name;
    public Text age;
    public Text gender;
    public Text personality;
    public Image portraitImage;

    public Button addButton; // ボタンをインスペクタから設定
    public Button nextButton;
    private ResidentRequest[] requests;

    void Start()
    {
        // 初期の居住者をリストに追加
        residents.Add(new Resident
        {
            name = "田中",
            age = 25,
            gender = "男",
            personality = "善",
            portrait = Resources.Load<Sprite>("Images/RImage01")
        });

        residents.Add(new Resident
        {
            name = "佐藤",
            age = 28,
            gender = "男",
            personality = "悪",
            portrait = Resources.Load<Sprite>("Images/RImage02")
        });

        residents.Add(new Resident
        {
            name = "山本",
            age = 25,
            gender = "女",
            personality = "善",
            portrait = Resources.Load<Sprite>("Images/RImage03")
        });

        residents.Add(new Resident
        {
            name = "鈴木",
            age = 30,
            gender = "女",
            personality = "悪",
            portrait = Resources.Load<Sprite>("Images/RImage04")
        });

        DisplayResidentInfo(currentIndex);
    }

    // リスト内の全居住者を表示
    void ListResidents()
    {
        for (int i = 0; i < residents.Count; i++)
        {
            Resident resident = residents[i];
            UnityEngine.Debug.Log($"Resident {i}: Name = {resident.name}, Age = {resident.age}, Gender = {resident.gender}, Personality = {resident.personality}");
            UnityEngine.Debug.Log($"NextResident実行後 - リストの長さ: {residents.Count}");
        }
    }

    public void OnButtonClick()
    {
        // 新しい居住者を追加
        AddNewResident("山田", 21, "男", "善", "Images/RImage01");

        // Residentリストの情報を表示
        ListResidents();


        UnityEngine.Debug.Log("ボタンが押されました");
    }

    public void OnButtonClickNext()
    {
        NextResident();
    }

    public void RemoveCurrentResident()
    {
        if (residents.Count == 0) return; // 住民がいない場合は何もしない

        // 現在の住民を削除
        Resident selectedResident = residents[currentIndex];

        // 住民を削除
        RemoveResidentAt(currentIndex);

        // 次の住民を表示
        NextResident();

    }

    // 新しい居住者をリストに追加する関数
    void AddNewResident(string residentName, int residentAge, string residentGender, string residentPersonality, string portraitPath)
    {
        Resident newResident = new Resident
        {
            name = residentName,
            age = residentAge,
            gender = residentGender,
            personality = residentPersonality,
            portrait = Resources.Load<Sprite>(portraitPath)
        };

        // residentsリストに新しいResidentを追加
        residents.Add(newResident);

        // リストのサイズが増加したことを確認
        UnityEngine.Debug.Log($"AddNewResident実行後のリストの長さ: {residents.Count}");
    }

    // 居住者評価関数（リストの他の部分で使用する可能性あり）
    public float EvaluateResident(Resident resident, ResidentRequest request)
    {
        float score = 0;

        // 性格の一致
        if (resident.personality == request.personality) score += 1;
        // 年齢の範囲のチェック
        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
        // 性別の一致
        if (resident.gender == request.gender) score += 1;

        //// スコアに基づいてお金を増やす処理（必要に応じて）
        CS_MoneyManager.Instance.AddMoney(score * 10000);

        return score; // スコアを返す
    }

    // 次の居住者を表示
    public void NextResident()
    {

        currentIndex++;
        if (currentIndex >= residents.Count) // .Length から .Count に変更
        {
            currentIndex = 0; // 最初に戻る
        }

        // 現在のリスト番号と長さを表示
        UnityEngine.Debug.Log($"NextResident実行後 - リスト番号: {currentIndex}");
        UnityEngine.Debug.Log($"NextResident実行後 - リストの長さ: {residents.Count}");

        DisplayResidentInfo(currentIndex);
    }

    // 居住者の情報をUIに表示
    private void DisplayResidentInfo(int index)
    {
        Resident resident = residents[index];
        name.text = resident.name;
        age.text = resident.age.ToString();
        gender.text = resident.gender;
        personality.text = resident.personality;
        portraitImage.sprite = resident.portrait;
    }

    public void RemoveResidentAt(int index)
    {
        // 新しいリストを作成
        List<Resident> newResidents = new List<Resident>();

        // 住民を削除
        for (int i = 0; i < residents.Count; i++)
        {
            if (i != index)
            {
                newResidents.Add(residents[i]);
            }
        }

        // residentsを新しいリストに更新
        residents = newResidents;
        currentIndex = Mathf.Clamp(currentIndex, 0, residents.Count - 1); // インデックスを調整    }
    }

    public Resident GetCurrentResident()
    {
        return residents[currentIndex];
    }
}
