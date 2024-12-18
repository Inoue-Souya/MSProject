using UnityEngine;
using TMPro;

public class CS_TextBlinker : MonoBehaviour
{
    public TMP_Text textMeshPro;    // TextMesh Pro�̎Q��
    public float fadeDuration = 1f; // �t�F�[�h�C���E�t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j

    private float alpha = 0f;       // ���݂̃A���t�@�l
    private bool isFadingIn = true; // �t�F�[�h�C�������ǂ���

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }
    }

    private void Update()
    {
        // �A���t�@�l�����Ԍo�߂ɉ����Ē���
        float fadeStep = Time.deltaTime / fadeDuration;
        alpha += isFadingIn ? fadeStep : -fadeStep;

        // �A���t�@�l��0�`1�͈̔͂𒴂���������𔽓]
        if (alpha >= 1f)
        {
            alpha = 1f;
            isFadingIn = false;
        }
        else if (alpha <= 0f)
        {
            alpha = 0f;
            isFadingIn = true;
        }

        // TextMesh Pro�̃A���t�@�l��ݒ�
        SetTextAlpha(alpha);
    }

    private void SetTextAlpha(float alphaValue)
    {
        if (textMeshPro != null)
        {
            Color color = textMeshPro.color;
            color.a = alphaValue; // �A���t�@�l��ύX
            textMeshPro.color = color;
        }
    }
}
