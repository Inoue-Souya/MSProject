using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AlwaysOnScreen : MonoBehaviour
{
    public GameObject targetObject;  // 常に画面下部に表示したいオブジェクト
    public RectTransform uiElement;  // UI要素のRectTransform (Canvas内)
    public Vector3 ikonPosition;

    void Update()
    {
        // 画面の幅と高さを取得
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 画面下部のスクリーン座標 (y軸は最小値)
        Vector3 screenPos = new Vector3(screenWidth / 2, 0, 0);  // Xは画面中央、Yは下部

        // スクリーン座標をワールド座標に変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // Y軸は画面下部に固定し、X軸は現在の位置を保持
        worldPos.y = Camera.main.ViewportToWorldPoint(new Vector3(0,0.1f,0)).y;  // 画面の下部のY位置

        worldPos.x = ikonPosition.x;

        // Z軸の位置を保持（2DなのでZ軸は変えない）
        worldPos.z = targetObject.transform.position.z;

        // オブジェクトの位置を更新
        targetObject.transform.position = worldPos;

        // UI要素の場合、RectTransformを使って設定する
        if (uiElement != null)
        {
            Vector3 uiWorldPos = Camera.main.WorldToScreenPoint(worldPos); // ワールド座標をスクリーン座標に
            uiElement.position = uiWorldPos;
        }
    }
}
