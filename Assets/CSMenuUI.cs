using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 必要な名前空間を追加

public class CSMenuUI : MonoBehaviour
{
    public GameObject contextMenu; // コンテキストメニュー用のUIパネル

    private RectTransform panelRect;

    private void Start()
    {
        contextMenu.SetActive(false);
    }

    private void Update()
    {
        // 右クリックを検知
        if (Input.GetMouseButtonDown(1))
        {
            // マウスの位置を取得
            Vector2 mousePosition = Input.mousePosition;

            // パネルを表示する
            contextMenu.SetActive(true);

            // パネルのサイズを取得
            RectTransform panelRect = contextMenu.GetComponent<RectTransform>();
            Vector2 panelSize = panelRect.sizeDelta;

            // 画面のサイズを取得
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // パネルの左上がマウス位置に一致するように設定
            float xPos = mousePosition.x;
            float yPos = mousePosition.y; 

            // 右にはみ出す場合は位置を調整
            if (xPos + panelSize.x > screenWidth)
            {
                xPos = screenWidth - panelSize.x; // パネルの幅を引いた位置に設定
            }

            // 下にはみ出す場合の調整
            if (yPos - panelSize.y < 0)
            {
                yPos = panelSize.y; // 下端を合わせる
            }

            // パネルの位置を設定（キャンバス内のローカル座標系に変換）
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect.parent as RectTransform, new Vector2(xPos, yPos), null, out localPoint);
            panelRect.anchoredPosition = localPoint; // 調整後の位置を設定

            // パネルの左上の位置を合わせるため、さらに調整
            panelRect.anchoredPosition += new Vector2(panelSize.x / 2, -panelSize.y / 2); // Y軸方向にパネルの高さを追加
        }

        // 左クリックでメニューを閉じる
        if (Input.GetMouseButtonDown(0))
        {
            // クリックしたオブジェクトがUI要素（ボタンなど）かどうかを確認
            if (!IsPointerOverUIElement())
            {
                // UI要素（ボタンなど）以外がクリックされた場合にメニューを閉じる
                contextMenu.SetActive(false);
            }
        }

        // ESCキーでメニューを閉じる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            contextMenu.SetActive(false);
        }
    }

    // クリックされた場所がUI要素かどうかを判定するメソッド
    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // クリックした場所にUI要素があるかどうかを判定
        return results.Count > 0;
    }
}
