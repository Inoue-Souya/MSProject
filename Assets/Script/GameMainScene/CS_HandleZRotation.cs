using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_HandleZRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f; // ‰ñ“]‘¬“x
    [SerializeField] private float maxRotation = 25f; // Å‘åŠp“x

    private float currentRotation = 0f;
    private float direction = 1f;

    void Update()
    {
        // ‰ñ“]Šp“x‚ðXV
        currentRotation += direction * rotationSpeed * Time.deltaTime;

        // ‰ñ“]‚ªÅ‘å‚Ü‚½‚ÍÅ¬‚É’B‚µ‚½‚ç•ûŒü‚ð”½“]
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

        // ZŽ²‰ñ“]‚ð“K—p
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            currentRotation
        );
    }
}
