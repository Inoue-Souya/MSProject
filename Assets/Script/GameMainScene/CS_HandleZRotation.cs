using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_HandleZRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f; // ��]���x
    [SerializeField] private float maxRotation = 25f; // �ő�p�x

    private float currentRotation = 0f;
    private float direction = 1f;

    void Update()
    {
        // ��]�p�x���X�V
        currentRotation += direction * rotationSpeed * Time.deltaTime;

        // ��]���ő�܂��͍ŏ��ɒB����������𔽓]
        if (currentRotation >= maxRotation)
        {
            currentRotation = maxRotation;
            direction = -1f;
        }
        else if (currentRotation <= -maxRotation)
        {
            currentRotation = -maxRotation;
            direction = 1f;
        }

        // Z����]��K�p
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            currentRotation
        );
    }
}
