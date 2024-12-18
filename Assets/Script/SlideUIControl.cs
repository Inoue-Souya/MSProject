using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUIControl : MonoBehaviour
{
    public int state = 0; // 0: ��\��, 1: �\��, 2: ��\���Ɉړ���
    public bool loop = false; // �X���C�h�̃��[�v
    [Header("Positions")]
    public Vector3 outPos01; // �J�n�ʒu�i��ʊO ���j
    public Vector3 inPos;    // �\���ʒu
    public Vector3 outPos02; // �I���ʒu�i��ʊO �E�j

    [HideInInspector]
    public bool slideOutToLeft = false; // �������ɃX���C�h�A�E�g���邩�i�f�t�H���g�͉E�j

    void Update()
    {
        if (state == 0)
        {
            // ��\���ʒu�ɌŒ�
            if (transform.localPosition != outPos01)
                transform.localPosition = outPos01;
        }
        else if (state == 1)
        {
            // �\���ʒu�Ɉړ�
            if (Vector3.Distance(transform.localPosition, inPos) < 1.0f)
                transform.localPosition = inPos;
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, 2.0f * Time.unscaledDeltaTime);
        }
        else if (state == 2)
        {
            // �I���ʒu�Ɉړ��i�X���C�h�������l���j
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
