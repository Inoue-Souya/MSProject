using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ParticleMover : MonoBehaviour
{
    public ParticleSystem particleSystem; // �p�[�e�B�N���V�X�e�����A�T�C������
    public Vector2 targetPosition; // �p�[�e�B�N�����ړ�������ʒu

    private void Start()
    {
        // �p�[�e�B�N���V�X�e�����w�肵���ʒu�Ɉړ�
        if (particleSystem != null)
        {
            MoveParticleSystemToPosition(targetPosition);
        }
    }

    private void MoveParticleSystemToPosition(Vector2 position)
    {
        particleSystem.transform.position = position; // �w�肵���ʒu�Ɉړ�
        particleSystem.Play(); // �p�[�e�B�N�����Đ�
    }
}
