using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AutoMoveCharactor : MonoBehaviour
{
    public float moveSpeed = 3f; // �ړ����x
    public BoxCollider2D roomCollider; // �����̃{�b�N�X�R���C�_�[

    private Rigidbody2D rb;
    private Vector2 minBound;
    private Vector2 maxBound;
    private bool movingRight = true; // �ړ������̃t���O

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �����̃{�b�N�X�R���C�_�[�͈̔͂��擾
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
        // ���݂̈ʒu���擾
        Vector2 currentPosition = rb.position;

        // �ړ��x�N�g�����v�Z
        Vector2 movement = new Vector2((movingRight ? 1 : -1) * moveSpeed * Time.deltaTime, 0);

        // ���݂̈ʒu���X�V
        rb.MovePosition(currentPosition + movement);

        // �{�b�N�X�R���C�_�[�͈͓̔��ɐ���
        if (currentPosition.x >= maxBound.x || currentPosition.x <= minBound.x)
        {
            movingRight = !movingRight; // �����𔽓]
        }
    }
}
