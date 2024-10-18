using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Calendar : MonoBehaviour
{
    //public GameObject dayPrefab; // プレハブとして設定されたテキストUI要素
    public Transform gridLayout; // グリッドレイアウト
    public Text DayText;// 日数テキスト
    private Image[] cells; // セルのImageコンポーネントを保持する配列

    private string[] daysOfWeek = { "月", "火", "水", "木", "金", "土", "日" };

    private int currentYellowCellIndex = -1; // 現在の黄色のセルのインデックスを保持
    private int currentRedCellIndex = -1; // 現在の赤色のセルのインデックスを保持

    //void Start()
    //{
    //    CreateCalendar();
    //}

    public void NextDay()
    {
        // 現在の黄色のセルを白色に戻し、次のセルを黄色にする
        if (currentYellowCellIndex != -1 && currentYellowCellIndex < cells.Length)
        {
            cells[currentYellowCellIndex].color = Color.white; // 白色に戻す
        }

        // 次のセルを黄色にするロジック
        currentYellowCellIndex = (currentYellowCellIndex + 1) % cells.Length; // セルをループさせる

        // 黄色セルと赤色セルが重なった場合に新しいセルを再度設定
        if (currentYellowCellIndex == currentRedCellIndex)
        {
            // 赤色セルを新しい場所に移動または再生成
            SetNewRedCell();
        }

        // 次のセルを黄色に設定
        cells[currentYellowCellIndex].color = Color.yellow;

        // 日数テキストを更新
        DayText.text = "あと" + (currentRedCellIndex - currentYellowCellIndex) + "日";
    }

    void SetNewRedCell()
    {
        // 赤色セルを現在の黄色セルの7つ後に設定
        int newRedCellIndex = currentYellowCellIndex + 7;

        // セル数を超えた場合
        if (newRedCellIndex >= cells.Length)
        {
            // 現在の赤色セルを白に戻す
            if (currentRedCellIndex != -1 && currentRedCellIndex < cells.Length)
            {
                cells[currentRedCellIndex].color = Color.white;
            }

            // ランダムに黄色と赤色セルを再設定
            SetRandomColorCells();
        }
        else
        {
            // 新しい赤色セルを設定
            currentRedCellIndex = newRedCellIndex;
            cells[currentRedCellIndex].color = Color.red;
        }
    }

    //void CreateCalendar()
    //{
    //    //// セルの色をランダムに設定
    //    //SetRandomColorCells();

    //    if (dayPrefab == null)
    //    {
    //        Debug.LogError("dayPrefab is not assigned!");
    //        return;
    //    }

    //    if (gridLayout == null)
    //    {
    //        Debug.LogError("gridLayout is not assigned!");
    //        return;
    //    }

    //    int rows = 6; // 行数
    //    int columns = 7; // 列数
    //    int totalCells = rows * columns; // 総セル数

    //    // 既存のプレハブ数を考慮して調整
    //    int existingChildren = gridLayout.childCount;

    //    // 必要なセル数 - 既存のセル数分だけ新たに生成
    //    int cellsToGenerate = totalCells - existingChildren;
    //    cells = new Image[totalCells]; // Imageコンポーネントの配列を初期化

    //    for (int i = 0; i < totalCells; i++)
    //    {
    //        GameObject day;

    //        // 既存のプレハブを再利用するか、新たに生成する
    //        if (i < existingChildren)
    //        {
    //            day = gridLayout.GetChild(i).gameObject; // 既存のセルを再利用
    //        }
    //        else
    //        {
    //            day = Instantiate(dayPrefab, gridLayout); // 新たにセルを生成
    //        }

    //        // 子オブジェクトからTextコンポーネントを取得
    //        Text dayText = day.GetComponentInChildren<Text>();
    //        if (dayText == null)
    //        {
    //            Debug.LogError("Text component not found in dayPrefab!");
    //            continue; // エラーメッセージを表示し、次のループへ
    //        }

    //        // Imageコンポーネントを取得
    //        Image dayImage = day.GetComponent<Image>();
    //        cells[i] = dayImage; // 取得したImageを配列に保存

    //        // 最初の7セルには曜日名を割り当てる
    //        if (i < columns)
    //        {
    //            dayText.text = daysOfWeek[i]; // 曜日の配列を使用
    //        }
    //        else
    //        {
    //            dayText.text = ""; // その他は空白
    //        }
    //    }

    //    // セルの色をランダムに設定
    //    SetRandomColorCells();
    //}

    void SetRandomColorCells()
    {
        // 曜日が入っているセルの次のセルを取得
        int startIndex = 7; // 曜日名が入っているセルの次のセルのインデックス

        // 7つの中からランダムで1つ選ぶ
        int randomOffset = Random.Range(1, 7); // 1〜6のオフセット
        int yellowCellIndex = startIndex + randomOffset; // 選ばれたセルのインデックス
        currentYellowCellIndex = yellowCellIndex;// 最初に選ばれたセルのインデックスを保持

        if (yellowCellIndex < cells.Length)
        {
            // 選ばれたセルを黄色にする
            cells[yellowCellIndex].color = Color.yellow;
            // 1週間後のセルを赤色にする
            int redCellIndex = yellowCellIndex + 7; // 7つ後のセル
            currentRedCellIndex = redCellIndex;// ７つ後のセルのインデックスを保持

            if (redCellIndex < cells.Length)
            {
                cells[redCellIndex].color = Color.red;
            }
        }
    }
}