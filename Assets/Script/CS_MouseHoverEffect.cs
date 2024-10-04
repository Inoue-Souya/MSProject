using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MouseHoverEffect : MonoBehaviour
{
    public float maxDistance = 5f; // �}�E�X�J�[�\���Ƃ̋����̍ő�l
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z���W��0�ɐݒ�i2D�̏ꍇ�j

        // �I�u�W�F�N�g�̈ʒu�ƃ}�E�X�̋������v�Z
        float distance = Vector3.Distance(transform.position, mousePosition);

        // �����Ɋ�Â��ē����x���v�Z
        float alpha = Mathf.Clamp01(1 - (distance / maxDistance));

        // �X�v���C�g�̐F���X�V
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
