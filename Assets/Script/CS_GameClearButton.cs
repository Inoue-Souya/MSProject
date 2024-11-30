using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // �V�[���Ǘ��ɕK�v
using System.Collections; // �R���[�`�����g�����߂ɕK�v

public class CS_GameClearButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup; // CanvasGroup�̎Q�Ƃ�ǉ�
    private Button button; // Button�R���|�[�l���g�̎Q��
    [SerializeField] private Vector3 originalScale = new Vector3(1f, 1f, 1f); // �����T�C�Y���w��
    [SerializeField] private Vector3 hoverScale = new Vector3(0.8f, 0.8f, 0.8f); // �J�[�\������������̃T�C�Y
    [SerializeField] private float scaleDuration = 0.5f; // �k���ɂ����鎞��
    [SerializeField] private float fadeDuration = 1f; // �t�F�[�h�C���ɂ����鎞��
    [SerializeField] private string targetSceneName;   // �J�ڐ�̃V�[����

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup�R���|�[�l���g���擾
        button = GetComponent<Button>(); // Button�R���|�[�l���g���擾
        rectTransform.localScale = originalScale; // �Q�[���J�n���Ɏw�肳�ꂽ���̃T�C�Y�ɃZ�b�g
        canvasGroup.alpha = 0f; // �ŏ��͓����ɐݒ�
        button.interactable = false; // �t�F�[�h���̓N���b�N�𖳌���

        // �t�F�[�h�C������
        StartCoroutine(FadeIn(fadeDuration));
    }

    // �t�F�[�h�C���̃R���[�`��
    private IEnumerator FadeIn(float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / duration); // �����x��ω�������
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // �Ō�Ɋm���Ɋ��S�ɕ\��������
        button.interactable = true; // �t�F�[�h������ɃN���b�N��L����
    }

    // �J�[�\�����{�^���ɏ�����Ƃ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable) // �{�^�����L���ȏꍇ�̂ݓ���
        {
            StopAllCoroutines(); // ���݂̃R���[�`�����~
            // �J�[�\������������ɃX���[�Y�ɏk��
            StartCoroutine(SmoothScale(rectTransform.localScale, hoverScale, scaleDuration));
        }
    }

    // �J�[�\�����{�^������O�ꂽ�Ƃ�
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable) // �{�^�����L���ȏꍇ�̂ݓ���
        {
            StopAllCoroutines(); // ���݂̃R���[�`�����~
            // �J�[�\�����O�ꂽ���ɃX���[�Y�Ɍ��̃T�C�Y�ɖ߂�
            StartCoroutine(SmoothScale(rectTransform.localScale, originalScale, scaleDuration));
        }
    }

    // �{�^�����N���b�N���ꂽ�Ƃ�
    public void OnButtonClick()
    {
        if (!string.IsNullOrEmpty(targetSceneName)) // �V�[�������w�肳��Ă���ꍇ
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    // �X���[�Y�ɃX�P�[����ύX����R���[�`��
    private IEnumerator SmoothScale(Vector3 startScale, Vector3 endScale, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = endScale; // �Ō�Ɋm���ɖڕW�X�P�[���ɂ���
    }
}
