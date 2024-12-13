using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PulsatingVignette : MonoBehaviour
{
    public Material vignetteMaterial;

    [Range(0, 1)] public float maxIntensity = 1.0f; // �ő勭�x
    [Range(0, 1)] public float minIntensity = 0.0f; // �ŏ����x
    public float pulseSpeed = 1.0f;                // �_�ł̑��x

    private void Update()
    {
        // �Q�[�������s���̂ݓ_�ŏ��������s
        if (Application.isPlaying && vignetteMaterial != null)
        {
            // �_�ŁiIntensity�̊��炩�ȑ����j
            float time = Time.time * pulseSpeed;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(time) + 1.0f) / 2.0f); // �����g�œ_��
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
