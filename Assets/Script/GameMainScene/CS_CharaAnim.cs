using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CharaAnim : MonoBehaviour
{
    // �X�v���C�g��ݒ肷��z��
    [Header("�A�j���[�V�����p�X�v���C�g")]
    public Sprite[] Animsprites;

    [Header("�������p�X�v���C�g")]
    public Sprite inRoomsprite;

    public CS_DragandDrop dragandDrop;// �h���b�O���A�������ł���t���O���擾���邽�߂̕ϐ�

    // �X�V�Ԋu
    [Header("�A�j���[�V�����̊Ԋu")]
    public float frameDuration = 0.5f;

    // �A�j���[�V�����p�̃J�E���^�[
    private float timer = 0f;
    private int currentFrame = 0;
    private int[] sequence = { 0, 1, 2, 1 }; // �X�v���C�g�C���f�b�N�X�̃V�[�P���X

    // SpriteRenderer�R���|�[�l���g�ւ̎Q��
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // SpriteRenderer���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �����X�v���C�g��ݒ�
        if (Animsprites.Length > 0)
        {
            spriteRenderer.sprite = Animsprites[sequence[0]];
        }
    }

    private void Update()
    {
        if (dragandDrop.GetisDragging())
        {// �h���b�O���̓L�������A�j���[�V��������
            spriteRenderer.enabled = true;
            // �o�ߎ��Ԃ��X�V
            timer += Time.deltaTime;

            // �t���[���Ԋu�𒴂�����X�v���C�g��ύX
            if (timer >= frameDuration)
            {
                timer -= frameDuration; // �c�莞�Ԃ�����

                // ���̃X�v���C�g�C���f�b�N�X�ɐi��
                currentFrame = (currentFrame + 1) % sequence.Length;

                // �X�v���C�g��ύX
                spriteRenderer.sprite = Animsprites[sequence[currentFrame]];
            }
        }
        else if(dragandDrop.GetinRoom())
        {// �������͋������p�̃X�v���C�g�ɂ���
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = inRoomsprite;
        }
        else
        {// �������ĂȂ����̓A�C�R���p�̃X�v���C�g�ɂ���
            spriteRenderer.enabled = false;
        }


    }
}
