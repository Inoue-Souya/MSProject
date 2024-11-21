using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AddYo_kai : MonoBehaviour
{
    private bool chargeFlag;
    [Header("部屋のリスト")]
    public List<CS_Room> Rooms;

    [Header("妖怪チェンジマネージャー")]
    public CS_Yo_kaiChange Yo_KaiChange;

    [Header("追加する妖怪オブジェクト")]
    public CS_DragandDrop NewYo_kai;

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
                    Yo_KaiChange.AddYo_kai(NewYo_kai);
                    chargeFlag = true;
                    return;
                }
            }
        }
        
    }
}
