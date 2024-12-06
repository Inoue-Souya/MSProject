using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PulsatingVignette : MonoBehaviour
{
    public Material vignetteMaterial;

    [Range(0, 1)] public float maxIntensity = 1.0f; // 最大強度
    [Range(0, 1)] public float minIntensity = 0.0f; // 最小強度
    public float pulseSpeed = 1.0f;                // 点滅の速度

    private void Update()
    {
        // ゲームが実行中のみ点滅処理を実行
        if (Application.isPlaying && vignetteMaterial != null)
        {
            // 点滅（Intensityの滑らかな増減）
            float time = Time.time * pulseSpeed;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(time) + 1.0f) / 2.0f); // 正弦波で点滅
            vignetteMaterial.SetFloat("_Intensity", intensity);
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (vignetteMaterial != null)
        {
            Graphics.Blit(src, dest, vignetteMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
