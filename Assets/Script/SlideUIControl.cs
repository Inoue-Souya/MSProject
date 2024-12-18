using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUIControl : MonoBehaviour
{
    public int state = 0; // 0: 非表示, 1: 表示, 2: 非表示に移動中
    public bool loop = false; // スライドのループ
    [Header("Positions")]
    public Vector3 outPos01; // 開始位置（画面外 左）
    public Vector3 inPos;    // 表示位置
    public Vector3 outPos02; // 終了位置（画面外 右）

    [HideInInspector]
    public bool slideOutToLeft = false; // 左方向にスライドアウトするか（デフォルトは右）

    void Update()
    {
        if (state == 0)
        {
            // 非表示位置に固定
            if (transform.localPosition != outPos01)
                transform.localPosition = outPos01;
        }
        else if (state == 1)
        {
            // 表示位置に移動
            if (Vector3.Distance(transform.localPosition, inPos) < 1.0f)
                transform.localPosition = inPos;
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, 2.0f * Time.unscaledDeltaTime);
        }
        else if (state == 2)
        {
            // 終了位置に移動（スライド方向を考慮）
            Vector3 targetPos = slideOutToLeft ? outPos01 : outPos02;

            if (Vector3.Distance(transform.localPosition, targetPos) < 1.0f)
                transform.localPosition = targetPos;
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 2.0f * Time.unscaledDeltaTime);

            if (transform.localPosition == targetPos && loop)
                state = 0;
        }
    }
}
