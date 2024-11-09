using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ParticleMover : MonoBehaviour
{
    public ParticleSystem particleSystem; // パーティクルシステムをアサインする
    public Vector2 targetPosition; // パーティクルを移動させる位置

    private void Start()
    {
        // パーティクルシステムを指定した位置に移動
        if (particleSystem != null)
        {
            MoveParticleSystemToPosition(targetPosition);
        }
    }

    private void MoveParticleSystemToPosition(Vector2 position)
    {
        particleSystem.transform.position = position; // 指定した位置に移動
        particleSystem.Play(); // パーティクルを再生
    }
}
