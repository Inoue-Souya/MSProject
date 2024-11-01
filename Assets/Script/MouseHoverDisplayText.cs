using UnityEngine;
using UnityEngine.UI;

public class MouseHoverDisplayText : MonoBehaviour
{
    // 表示したい文字列
    public string message = "Hello!";
    // テキストを表示するためのUI Textコンポーネント
    public Text displayText;

    private void Start()
    {
        // 初めは文字を非表示にする
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
            // マウスがコライダーに当たっている場合
            if (displayText != null)
            {
                // マウスがクリックまたはホールドされている場合、テキストを非表示にする
                if (Input.GetMouseButton(0)) // 左クリックが押されているか
                {
                    displayText.gameObject.SetActive(false);
                }
                else
                {
                    // マウスが当たっているが、クリックされていない場合のみ表示
                    displayText.text = message;
                    displayText.gameObject.SetActive(true);
                }
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
}
