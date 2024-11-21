using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AddYo_kai : MonoBehaviour
{
    private bool chargeFlag;
    [Header("�����̃��X�g")]
    public List<CS_Room> Rooms;

    [Header("�d���`�F���W�}�l�[�W���[")]
    public CS_Yo_kaiChange Yo_KaiChange;

    [Header("�ǉ�����d���I�u�W�F�N�g")]
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
            // rooms���X�g�ɂ���isUnlocked
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
