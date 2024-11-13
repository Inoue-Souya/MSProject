using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public Button optionButton;  // �I�v�V�����{�^��
    public CanvasGroup fadeGroup;  // �t�F�[�h�p��CanvasGroup

    public float fadeDuration = 1.0f;  // �t�F�[�h����
    private bool isFading = false;

    void Start()
    {
        // �I�v�V�����{�^���Ƀ��X�i�[��ǉ�
        optionButton.onClick.AddListener(OnOptionButtonClicked);

        // �t�F�[�h�p��CanvasGroup��������
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 0;  // ��ʂ͍ŏ����疾�邢��Ԃ���X�^�[�g
        }
    }

    void Update()
    {
        // Enter�L�[�܂��͉E�N���b�N�ŃV�[���J��
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(1)) && !isFading)
        {
            StartCoroutine(FadeOutAndLoadScene("StorySwitch"));  // �t�F�[�h�A�E�g�ƃV�[���J��
        }
    }

    // �I�v�V�����{�^�����N���b�N���ꂽ�Ƃ��̏���
    void OnOptionButtonClicked()
    {
        Debug.Log("Option button clicked!");
    }

    // �t�F�[�h�A�E�g���ăV�[�������[�h�i���邢��Ԃ���Â��Ȃ�j
    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isFading = true;
        float timer = 0f;

        // �t�F�[�h�A�E�g�i��ʂ��Â��Ȃ�j
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);  // 0����1�ɐ��`��ԂŃt�F�[�h�A�E�g
            yield return null;
        }

        // �V�[�������[�h
        SceneManager.LoadScene(sceneName);
    }
}
