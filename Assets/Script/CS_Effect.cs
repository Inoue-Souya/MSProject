using UnityEngine;

public class CS_Effect : MonoBehaviour
{
    public GameObject particlePrefab; // �p�[�e�B�N���̃v���n�u

    // �w��ʒu�Ƀp�[�e�B�N�����Đ����郁�\�b�h
    public void PlayPlacementEffect(Vector3 position)
    {
        if (particlePrefab != null)
        {
            GameObject particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
            Destroy(particleInstance, 2f); // �p�[�e�B�N����2�b��ɍ폜
        }
        else
        {
            Debug.LogWarning("Particle prefab is not assigned in CS_Effect.");
        }
    }
}
