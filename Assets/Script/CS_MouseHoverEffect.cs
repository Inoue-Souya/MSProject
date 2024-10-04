using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MouseHoverEffect : MonoBehaviour
{
    public float maxDistance = 5f; // マウスカーソルとの距離の最大値
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // マウスの位置をワールド座標に変換
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z座標を0に設定（2Dの場合）

        // オブジェクトの位置とマウスの距離を計算
        float distance = Vector3.Distance(transform.position, mousePosition);

        // 距離に基づいて透明度を計算
        float alpha = Mathf.Clamp01(1 - (distance / maxDistance));

        // スプライトの色を更新
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
