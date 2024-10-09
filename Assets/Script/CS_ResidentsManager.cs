using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Tilemaps;

public class CS_ResidentsManager : MonoBehaviour
{
    public static CS_ResidentsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも消えないようにする
        }
        else
        {
            Destroy(gameObject); // 既に存在する場合は破棄
        }
    }

    //public CS_CreateResident[] resident; // スクリプタブルオブジェクトの配列
    //public GameObject residentPrefab; // UIプレハブ

    // タイル座標をキー、住民データを値にする辞書
    private Dictionary<Vector3Int, Resident> residents = new Dictionary<Vector3Int, Resident>();

    // タイルマップのインスタンスを取得
    public Tilemap tilemap; // タイルマップの参照
    public TileBase vacantTile; // 未入居用タイルの参照
    public TileBase occupiedTile; // 入居済みタイル

    void Start()
    {
        //PopulateResidents();

        // 住民データ
        Resident resident1 = new Resident
        {
            name = "田中",
            age = 25,
            gender = "男",
            personality = "善",
            info = "101の住民",
            portrait = Resources.Load<Sprite>("Images/RImage01") // 画像を設定（Resourcesフォルダ内に配置）
        };
        Resident resident2 = new Resident
        {
            name = "佐藤",
            age = 28,
            gender = "男",
            personality = "悪",
            info = "102の住民",
            portrait = Resources.Load<Sprite>("Images/RImage02") // 画像を設定（Resourcesフォルダ内に配置）
        };
        Resident resident3 = new Resident
        {
            name = "山本",
            age = 25,
            gender = "女",
            personality = "善",
            info = "103の住民",
            portrait = Resources.Load<Sprite>("Images/RImage03") // 画像を設定（Resourcesフォルダ内に配置）
        };


        residents.Add(new Vector3Int(-7, -2, 0), resident1);
        residents.Add(new Vector3Int(-4, -2, 0), resident2);
        residents.Add(new Vector3Int(-1, -2, 0), resident3);

    }

    // 住民を追加するメソッド
    public void AddResident(Vector3Int position, Resident resident)
    {
        if (residents.ContainsKey(position))
        {
            Debug.LogWarning("その位置には既に住民がいます。");
            return;
        }

        residents[position] = resident; // 住民を追加
        tilemap.SetTile(position, occupiedTile); // タイルを入居済みに変更
        Debug.Log($"{resident.name}が{position}に追加されました。");
    }

    public Resident GetResident(Vector3Int position)
    {
        residents.TryGetValue(position, out Resident resident);
        return resident;
    }

    public void RemoveResident(Vector3Int position)
    {
        if (residents.ContainsKey(position))
        {
            residents.Remove(position);
        }
    }

    // 住民要望に基づく評価を計算
    public float EvaluateResident(Resident resident, ResidentRequest request)
    {
        float score = 0;

        // 性格の一致
        if (resident.personality == request.personality) score += 1;
        // 年齢の範囲のチェック
        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
        // 性別の一致
        if (resident.gender == request.gender) score += 1;

        //// スコアに基づいてお金を増やす処理
        //CS_MoneyManager.Instance.AddMoney(score * 10000); // スコアに応じてお金を増加（例: 1点ごとに10000円増える）

        return score; // スコアを返す
    }

    public void ChooseResident(Vector3Int position, ResidentRequest request)
    {
        Resident resident = GetResident(position);
        if (resident != null)
        {
            float score = EvaluateResident(resident, request);
            Debug.Log($"Evaluation Score: {score}");

            // 住民データを削除
            RemoveResident(position); // ここが正しく呼ばれるか確認

            // タイルを未入居用タイルに切り替える処理
            SwitchTileToVacant(position);
        }
        else
        {
            Debug.LogWarning("住民が見つかりませんでした。削除できません。");
        }
    }

    public void AddResidentToTile(Vector3Int tilePos)
    {
        Resident resident = SelectedResidentData.Instance.selectedResident;

        if (resident != null)
        {
            // タイルを入居済みに変更
            tilemap.SetTile(tilePos, occupiedTile);
            // 住民情報を辞書などに保存（例: residents.Add(tilePos, resident);）
            Debug.Log($"{resident.name} が入居しました。");
        }
    }

    public void SwitchTileToVacant(Vector3Int position)
    {
        tilemap.SetTile(position, vacantTile); // 指定された位置のタイルを変更
    }

    // 空いている部屋をチェックするメソッド
    public bool IsRoomAvailable(Vector3Int position)
    {
        return !residents.ContainsKey(position);
    }

    // 追加で、住民情報を表示するメソッド
    public void DisplayResidentInfo(Vector3Int position)
    {
        if (residents.TryGetValue(position, out Resident resident))
        {
            // UIに住民情報を表示するロジック
            Debug.Log($"住民名: {resident.name}, 年齢: {resident.age}, 性別: {resident.gender}");
        }
    }

    //    void PopulateResidents()
    //    {
    //        foreach (CS_CreateResident resident in residents)
    //        {
    //            GameObject residentGO = Instantiate(residentPrefab, transform);
    //            CS_ResidentUI residentUI = residentGO.GetComponent<CS_ResidentUI>();

    //            // UIに住民情報を設定
    //            residentUI.SetResidentData(resident);
    //        }
    //    }
}