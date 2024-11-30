using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CharaAnim : MonoBehaviour
{
    // スプライトを設定する配列
    [Header("アニメーション用スプライト")]
    public Sprite[] Animsprites;

    [Header("脅かす用スプライト")]
    public Sprite inRoomsprite;

    public CS_DragandDrop dragandDrop;// ドラッグ中、入室中であるフラグを取得するための変数

    // 更新間隔
    [Header("アニメーションの間隔")]
    public float frameDuration = 0.5f;

    // アニメーション用のカウンター
    private float timer = 0f;
    private int currentFrame = 0;
    private int[] sequence = { 0, 1, 2, 1 }; // スプライトインデックスのシーケンス

    // SpriteRendererコンポーネントへの参照
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // SpriteRendererを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期スプライトを設定
        if (Animsprites.Length > 0)
        {
            spriteRenderer.sprite = Animsprites[sequence[0]];
        }
    }

    private void Update()
    {
        if (dragandDrop.GetisDragging())
        {// ドラッグ時はキャラをアニメーションする
            spriteRenderer.enabled = true;
            // 経過時間を更新
            timer += Time.deltaTime;

            // フレーム間隔を超えたらスプライトを変更
            if (timer >= frameDuration)
            {
                timer -= frameDuration; // 残り時間を引く

                // 次のスプライトインデックスに進む
                currentFrame = (currentFrame + 1) % sequence.Length;

                // スプライトを変更
                spriteRenderer.sprite = Animsprites[sequence[currentFrame]];
            }
        }
        else if(dragandDrop.GetinRoom())
        {// 入室時は脅かす用のスプライトにする
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = inRoomsprite;
        }
        else
        {// 何もしてない時はアイコン用のスプライトにする
            spriteRenderer.enabled = false;
        }


    }
}
