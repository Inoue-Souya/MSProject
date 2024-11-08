using UnityEngine;

public class CS_Effect : MonoBehaviour
{
    public GameObject particlePrefab; // パーティクルのプレハブ

    // 指定位置にパーティクルを再生するメソッド
    public void PlayPlacementEffect(Vector3 position)
    {
        if (particlePrefab != null)
        {
            GameObject particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
            Destroy(particleInstance, 2f); // パーティクルを2秒後に削除
        }
        else
        {
            Debug.LogWarning("Particle prefab is not assigned in CS_Effect.");
        }
    }
}
