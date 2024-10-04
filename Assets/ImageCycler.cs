using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを使用

public class ImageCycler : MonoBehaviour
{
    public UnityEngine.UI.Image targetImage;  // 変更する対象のImageコンポーネント
    public Sprite[] sprites;                  // 画像を保持するスプライトの配列
    private int currentIndex = 0;             // 現在表示している画像のインデックス

    // ボタンが押されたときに呼ばれるメソッド
    public void CycleImage()
    {
        if (sprites.Length == 0 || targetImage == null) return;

        // 現在のインデックスを進めて次の画像に切り替え
        currentIndex = (currentIndex + 1) % sprites.Length;

        // 画像を変更
        targetImage.sprite = sprites[currentIndex];
    }
}

