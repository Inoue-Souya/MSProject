using System.Collections;
using UnityEngine;

public class CS_GameClear : MonoBehaviour
{
    public RectTransform targetImage;
    public Vector2 startSize = new Vector2(200, 200);
    public Vector2 endSize = new Vector2(50, 50);
    public float duration = 2f;
    public float rotationSpeed = 360f;

    private void Awake()
    {
        this.enabled = false;  // ç≈èâÇÕñ≥å¯âª
    }

    private void Start()
    {
        if (targetImage != null)
        {
            targetImage.sizeDelta = startSize;
            StartCoroutine(ShrinkAndRotateImage());
        }
    }

    private IEnumerator ShrinkAndRotateImage()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            targetImage.sizeDelta = Vector2.Lerp(startSize, endSize, t);

            float rotationAngle = rotationSpeed * Time.deltaTime;
            targetImage.localRotation *= Quaternion.Euler(0f, 0f, rotationAngle);

            yield return null;
        }

        targetImage.sizeDelta = endSize;
        targetImage.localRotation = Quaternion.Euler(0f, 0f, 6f);
    }
}
