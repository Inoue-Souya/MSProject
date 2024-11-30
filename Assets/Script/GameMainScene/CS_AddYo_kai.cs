using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_AddYo_kai : MonoBehaviour
{
    private bool chargeFlag;
    [Header("部屋のリスト")]
    public List<CS_Room> Rooms;

    [Header("妖怪チェンジマネージャー")]
    public CS_Yo_kaiChange Yo_KaiChange;

    [Header("追加する妖怪オブジェクト")]
    public CS_DragandDrop NewYo_kai;

    [Header("カットインアニメーション関連の情報")]
    public Animator CutInAnimator; // Animatorコンポーネントをアタッチ
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        chargeFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!chargeFlag)
        {
            // roomsリストにあるisUnlocked
            for(int i=0;i<Rooms.Count;i++)
            {
                if(Rooms[i].isUnlocked)
                {
                    SpriteRenderer sprite = NewYo_kai.GetComponent<SpriteRenderer>();
                    image.sprite = sprite.sprite;

                    // アニメーションを再生
                    PlayCutInAnimation();

                    Yo_KaiChange.AddYo_kai(NewYo_kai);
                    chargeFlag = true;
                    return;
                }
            }
        }
    }

    // カットインアニメーションを再生する関数
    private void PlayCutInAnimation()
    {
        if (CutInAnimator != null)
        {
            CutInAnimator.SetBool("PlayCutIn",true); // AnimatorのTriggerを呼び出す
        }
        else
        {
            Debug.LogWarning("CutInAnimatorが設定されていません");
        }
    }
}
