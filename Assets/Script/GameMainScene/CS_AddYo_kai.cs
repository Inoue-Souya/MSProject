using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_AddYo_kai : MonoBehaviour
{
    private bool chargeFlag;
    [Header("�����̃��X�g")]
    public List<CS_Room> Rooms;

    [Header("�d���`�F���W�}�l�[�W���[")]
    public CS_Yo_kaiChange Yo_KaiChange;

    [Header("�ǉ�����d���I�u�W�F�N�g")]
    public CS_DragandDrop NewYo_kai;

    [Header("�J�b�g�C���A�j���[�V�����֘A�̏��")]
    public Animator CutInAnimator; // Animator�R���|�[�l���g���A�^�b�`
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
            // rooms���X�g�ɂ���isUnlocked
            for(int i=0;i<Rooms.Count;i++)
            {
                if(Rooms[i].isUnlocked)
                {
                    SpriteRenderer sprite = NewYo_kai.GetComponent<SpriteRenderer>();
                    image.sprite = sprite.sprite;

                    // �A�j���[�V�������Đ�
                    PlayCutInAnimation();

                    Yo_KaiChange.AddYo_kai(NewYo_kai);
                    chargeFlag = true;
                    return;
                }
            }
        }
    }

    // �J�b�g�C���A�j���[�V�������Đ�����֐�
    private void PlayCutInAnimation()
    {
        if (CutInAnimator != null)
        {
            CutInAnimator.SetBool("PlayCutIn",true); // Animator��Trigger���Ăяo��
        }
        else
        {
            Debug.LogWarning("CutInAnimator���ݒ肳��Ă��܂���");
        }
    }
}
