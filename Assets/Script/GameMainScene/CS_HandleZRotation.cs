using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_HandleZRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f; // 回転速度
    [SerializeField] private float maxRotation = 25f; // 最大角度

    private float currentRotation = 0f;
    private float direction = 1f;

    void Update()
    {
        // 回転角度を更新
        currentRotation += direction * rotationSpeed * Time.deltaTime;

        // 回転が最大または最小に達したら方向を反転
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

        // Z軸回転を適用
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            currentRotation
        );
    }
}
