using UnityEngine;

public class CS_LogoScaler : MonoBehaviour
{
    public float duration = 2.0f;       // �t�F�[�h�C���ƈړ��ɂ����鎞��
    public float floatDistance = 50.0f; // �t���[�g�C���̈ړ�����
    public FadeInButton fadeInButton1;  // 1�ڂ̃{�^��
    public FadeInButton fadeInButton2;  // 2�ڂ̃{�^��
    public SceneTransitionManager sceneTransitionManager; // �t�F�[�h�A�E�g�Ǘ�

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;

    void Start()
    {
        // SpriteRenderer �R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.up * floatDistance; // �ڕW�ʒu��ݒ�

        // �ŏ��̓����x��0�ɐݒ�
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0f; // �����̓����x�� 0 �ɐݒ�
            spriteRenderer.color = color;
        }

        // �{�^�����ŏ��͔�\���ɂ���
        fadeInButton1.gameObject.SetActive(false);
        fadeInButton2.gameObject.SetActive(false);
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration); // �o�ߎ��ԂɊ�Â��i�s�x�i0�`1�j

            // �t���[�g�C���̈ʒu
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            // �t�F�[�h�C���̃A���t�@�l
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = progress;  // �����x��i�s�x�Ɋ�Â��Đݒ�
                spriteRenderer.color = color;
            }
        }

        // �^�C�g�����S�̏o�����I�������{�^����\��
        if (elapsedTime >= duration)
        {
            if (!fadeInButton1.gameObject.activeSelf)
            {
                fadeInButton1.gameObject.SetActive(true);  // 1�ڂ̃{�^����\��
                fadeInButton1.FadeIn();  // 1�ڂ̃{�^�����t�F�[�h�C��
            }

            if (!fadeInButton2.gameObject.activeSelf)
            {
                fadeInButton2.gameObject.SetActive(true);  // 2�ڂ̃{�^����\��
                fadeInButton2.FadeIn();  // 2�ڂ̃{�^�����t�F�[�h�C��
            }
        }
    }

    // �{�^���ɐݒ肷��֐�: �V�[�����������Ƃ��ēn���đJ��
    public void LoadScene(string sceneName)
    {
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.FadeToScene(sceneName);
        }
        else
        {
            Debug.LogError("SceneTransitionManager ���ݒ肳��Ă��܂���B");
        }
    }
}
