using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ScaleDown : MonoBehaviour
{
    public Transform targetImage; // スケールを変更したいUIのRectTransform
    public float scaleFactor = 0.8f; // スケールの比率
    public float spaceHeight = 100f; // 下に空けるスペースの高さ

    private bool flgPose = false;

    public void OnButtonClick()
    {
        if(!flgPose)
        {
            flgPose = true;
            ScaleDown();
        }
        else
        {
            flgPose = false;

            // スケールを小さくする
            targetImage.localScale /= scaleFactor;
            // 元に戻す処理
            Vector3 newPosition = targetImage.localPosition;
            newPosition.y -= spaceHeight; // 上に移動
            targetImage.localPosition = newPosition;
        }
    }

    private void ScaleDown()
    {
        // スケールを小さくする
        targetImage.localScale *= scaleFactor;

        // 下にスペースを空けるために位置を変更
        Vector3 newPosition = targetImage.localPosition;
        newPosition.y += spaceHeight; // 上に移動
        targetImage.localPosition = newPosition;
    }
}
