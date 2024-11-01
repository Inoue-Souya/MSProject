using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AutoMoveCharactor : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度
    public BoxCollider2D roomCollider; // 部屋のボックスコライダー

    private Rigidbody2D rb;
    private Vector2 minBound;
    private Vector2 maxBound;
    private bool movingRight = true; // 移動方向のフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 部屋のボックスコライダーの範囲を取得
        if (roomCollider != null)
        {
            minBound = roomCollider.bounds.min;
            maxBound = roomCollider.bounds.max;
        }
        else
        {
            Debug.LogError("Room collider is not set!");
        }
    }

    void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // 現在の位置を取得
        Vector2 currentPosition = rb.position;

        // 移動ベクトルを計算
        Vector2 movement = new Vector2((movingRight ? 1 : -1) * moveSpeed * Time.deltaTime, 0);

        // 現在の位置を更新
        rb.MovePosition(currentPosition + movement);

        // ボックスコライダーの範囲内に制限
        if (currentPosition.x >= maxBound.x || currentPosition.x <= minBound.x)
        {
            movingRight = !movingRight; // 方向を反転
        }
    }
}
