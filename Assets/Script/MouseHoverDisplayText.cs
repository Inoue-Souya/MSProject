using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MouseHoverDisplayText : MonoBehaviour
{
    public Text displayText; // テキストを表示するためのUI Textコンポーネント
    private CS_Room room; // CS_Room スクリプトの参照
    public Vector3 textOffset = new Vector3(0, 1f, 0); // y軸方向に1.0のオフセット

    private void Start()
    {
        // CS_Roomコンポーネントを取得
        room = GetComponent<CS_Room>();

        // 初めはテキストを非表示にする
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // マウスの位置を取得してワールド座標に変換
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // マウス位置がBox Collider2Dに重なっているかをチェック
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);
        if (collider != null && collider == GetComponent<BoxCollider2D>())
        {
            // マウスがコライダーに当たっている場合にテキストを表示
            if (displayText != null)
            {
                // attributesの内容を表示用テキストに変換
                displayText.text = GetRoomAttributesText();
                displayText.gameObject.SetActive(true);

                Vector3 displayPosition = transform.position + textOffset;
                displayText.transform.position = Camera.main.WorldToScreenPoint(displayPosition);
            }
        }
        else
        {
            // 重なっていない場合、メッセージを非表示
            if (displayText != null)
            {
                displayText.gameObject.SetActive(false);
            }
        }
    }


    private string GetRoomAttributesText()
    {
        // `attributes` リストが null の場合に空の文字列を返す
        if (room.attributes == null)
        {
            return "";
        }

        string attributesText = ""; // 空の文字列で初期化

        // 各属性を一行ずつテキストに追加
        foreach (var attribute in room.attributes)
        {
            attributesText += $"{attribute.attributeName}: {attribute.matchScore}\n";
        }

        return attributesText.TrimEnd(); // 最後の改行を削除してから返す
    }

}


