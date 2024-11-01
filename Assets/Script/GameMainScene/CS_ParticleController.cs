using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem; // �p�[�e�B�N���V�X�e�����A�T�C������
    public float duration = 0.5f; // �p�[�e�B�N����\�����鎞��

    private void Update()
    {
        // �������`�F�b�N�i��F�X�y�[�X�L�[�������ꂽ��j
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowParticles();
        }
    }

    private void ShowParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Play(); // �p�[�e�B�N�����Đ�
            StartCoroutine(StopParticlesAfterDuration());
        }
    }

    private System.Collections.IEnumerator StopParticlesAfterDuration()
    {
        yield return new WaitForSeconds(duration); // �w�肵�����ԑ҂�
        particleSystem.Stop(); // �p�[�e�B�N�����~
    }
}
