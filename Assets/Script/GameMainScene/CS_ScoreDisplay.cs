using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreDisplay : MonoBehaviour
{
    public Text displayTextPrefab; // テキストのプレハブ
    public Transform canvasTransform; // テキストを配置するCanvasのTransform
    public Vector3 offset = new Vector3(0, 50, 0); // 表示位置のオフセット（インスペクターで設定可能）
    public int fontSize = 24; // インスペクターで設定可能なフォントサイズ
    public float fadeDuration = 2f; // フェードアウトの持続時間（秒）
    public float riseDistance = 50f; // テキストが上昇する距離

    [Range(0, 1)] public float textSaturation = 1f; // 彩度（0 = 無彩色, 1 = 鮮やか）
    [Range(0, 1)] public float textBrightness = 1f; // 明るさ（0 = 暗い, 1 = 明るい）

    private List<GameObject> activeTexts = new List<GameObject>(); // 表示中のテキストを管理

    public void ShowText(int addedPoints,Vector3 position)
    {
        GameObject newTextObject = Instantiate(displayTextPrefab.gameObject, canvasTransform);
        Text newText = newTextObject.GetComponent<Text>();

        if (newText == null)
        {
            Debug.LogError("displayTextPrefabが正しく設定されていません。");
            return;
        }

        // フォントサイズを変更
        newText.fontSize = fontSize;

        // テキストの内容を設定
        newText.text = $"+{addedPoints}";

        Color goldColor = new Color(1.0f, 0.84f, 0.0f); // 金色のRGB値

        // スコアに応じて色を変更
        if (addedPoints >= 1000)
        {
            newText.color = Color.yellow; // 大きいスコアは赤
        }
        else if (addedPoints >= 100)
        {
            newText.color = Color.blue; // 中くらいのスコアは黄色
        }
        else
        {
            newText.color = Color.white; // 小さいスコアは緑
        }

        // HSVを使用して彩度と光度を変更
        Color baseColor = Color.yellow; // 基本色（任意で変更可能）
        float h, s, v; // 色相、彩度、明度を格納
        Color.RGBToHSV(baseColor, out h, out s, out v); // RGBをHSVに変換

        // 彩度と光度を調整
        s = textSaturation; // 彩度をインスペクターの値に変更
        v = textBrightness; // 光度をインスペクターの値に変更

        // HSVをRGBに戻してテキストの色を設定
        newText.color = Color.HSVToRGB(h, s, v);

        // 表示位置をオフセットに基づいて設定
        RectTransform rectTransform = newText.GetComponent<RectTransform>();
        //rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0) + offset;
        rectTransform.position = position + offset;

        // リストに追加
        activeTexts.Add(newTextObject);

        // フェードアウト処理を開始
        StartCoroutine(FadeOutAndDestroy(newTextObject, newText));
    }


    private IEnumerator<System.Collections.IEnumerator> FadeOutAndDestroy(GameObject textObject, Text text)
    {
        float elapsedTime = 0f;
        Color initialColor = text.color;

        RectTransform rectTransform = text.GetComponent<RectTransform>();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // テキストの透明度を減少させる
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // テキストを徐々に上昇させる
            rectTransform.position += new Vector3(0, riseDistance * (Time.deltaTime / fadeDuration), 0);

            yield return null;
        }

        // フェードアウト終了後、リストから削除してオブジェクトを破棄
        activeTexts.Remove(textObject);
        Destroy(textObject);
    }
}
