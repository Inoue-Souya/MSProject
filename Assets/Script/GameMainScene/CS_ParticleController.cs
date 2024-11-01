using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem; // パーティクルシステムをアサインする
    public float duration = 0.5f; // パーティクルを表示する時間

    private void Update()
    {
        // 条件をチェック（例：スペースキーが押されたら）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowParticles();
        }
    }

    private void ShowParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Play(); // パーティクルを再生
            StartCoroutine(StopParticlesAfterDuration());
        }
    }

    private System.Collections.IEnumerator StopParticlesAfterDuration()
    {
        yield return new WaitForSeconds(duration); // 指定した時間待つ
        particleSystem.Stop(); // パーティクルを停止
    }
}
