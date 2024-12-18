using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class CS_NextSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ���̃V�[���̖��O���w��
    [SerializeField] private string nextSceneName;

    // �_�ł̐ݒ�
    [SerializeField] private UnityEngine.UI.Image targetImage; // �_�ł�����{�^���̔w�i
    [SerializeField] private float blinkSpeed = 1.0f; // �_�ł̑���

    private Color originalColor; // ���̐F
    private bool isBlinking = false; // �_�Œ����ǂ���

    private void Start()
    {
        // �{�^����Image�R���|�[�l���g���擾
        if (targetImage == null)
        {
            targetImage = GetComponent<UnityEngine.UI.Image>();
        }

        if (targetImage != null)
        {
            originalColor = targetImage.color; // ���̐F���L��
        }
      
    }

    // ���̃V�[�������[�h����
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
       
    }

    // �}�E�X�J�[�\�����{�^����ɓ�������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            isBlinking = true;
            StartCoroutine(BlinkTransparency());
        }
    }

    // �}�E�X�J�[�\�����{�^�����痣�ꂽ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            isBlinking = false;
            targetImage.color = originalColor; // ���̐F�ɖ߂�
        }
    }

    // �����ƕ\���̓_�ŏ���
    private System.Collections.IEnumerator BlinkTransparency()
    {
        while (isBlinking)
        {
            // ���S�ɓ����ɂ���
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            yield return new WaitForSeconds(1.0f / blinkSpeed);

            // ���̓����x�ɖ߂�
            targetImage.color = originalColor;
            yield return new WaitForSeconds(1.0f / blinkSpeed);
        }
    }
}
