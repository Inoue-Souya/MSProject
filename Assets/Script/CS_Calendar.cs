using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Calendar : MonoBehaviour
{
    public GameObject dayPrefab; // プレハブとして設定されたテキストUI要素
    public Transform gridLayout; // グリッドレイアウト
    public Text DayText;// 日数テキスト
    private Image[] cells; // セルのImageコンポーネントを保持する配列

    private string[] daysOfWeek = { "月", "火", "水", "木", "金", "土", "日" };

    private int currentYellowCellIndex = -1; // 現在の黄色のセルのインデックスを保持
    private int currentRedCellIndex = -1; // 現在の赤色のセルのインデックスを保持

    void Start()
    {
        CreateCalendar();
    }

    public void NextDay()
    {
        // 現在の黄色のセルを白色に戻し、次のセルを黄色にする
        if (currentYellowCellIndex != -1 && currentYellowCellIndex < cells.Length)
        {
            cells[currentYellowCellIndex].color = Color.white; // 白色に戻す
        }

        // 次のセルを黄色にするロジック
        currentYellowCellIndex = (currentYellowCellIndex + 1); // 7セルは曜日名用なので、それ以外をループ
        cells[currentYellowCellIndex].color = Color.yellow; // 次のセルを黄色に設定

        DayText.text="あと"+(currentRedCellIndex - currentYellowCellIndex)+"日";
        //% (cells.Length - 7)
    }

    void CreateCalendar()
    {
        if (dayPrefab == null)
        {
            Debug.LogError("dayPrefab is not assigned!");
            return;
        }

        if (gridLayout == null)
        {
            Debug.LogError("gridLayout is not assigned!");
            return;
        }

        int rows = 6; // 行数
        int columns = 7; // 列数
        int totalCells = rows * columns; // 総セル数
        cells = new Image[totalCells]; // Imageコンポーネントの配列を初期化

        for (int i = 0; i < totalCells; i++)
        {
            GameObject day = Instantiate(dayPrefab, gridLayout);

            // 子オブジェクトからTextコンポーネントを取得
            Text dayText = day.GetComponentInChildren<Text>();
            if (dayText == null)
            {
                Debug.LogError("Text component not found in dayPrefab!");
                continue; // エラーメッセージを表示し、次のループへ
            }

            // Imageコンポーネントを取得
            Image dayImage = day.GetComponent<Image>();
            cells[i] = dayImage; // 取得したImageを配列に保存

            // 最初の7セルには曜日名を割り当てる
            if (i < columns)
            {
                dayText.text = daysOfWeek[i]; // 曜日の配列を使用
            }
            else
            {
                dayText.text = ""; // その他は空白
            }
        }

        // セルの色をランダムに設定
        SetRandomColorCells();
    }

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