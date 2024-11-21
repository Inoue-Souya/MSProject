using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage; // ��ʑS�̂��Â����邽�߂� Image �R���|�[�l���g
    public float fadeDuration = 1.0f; // �t�F�[�h�A�E�g�ɂ����鎞��

    private bool isTransitioning = false;

    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 0f); // �ŏ��͓���
        }
        else
        {
            Debug.LogError("FadeImage ���ݒ肳��Ă��܂���I");
        }
    }

    // �t�F�[�h�A�E�g���Ă���V�[���J��
    public void FadeToScene(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeOutAndLoadScene(sceneName)); // �t�F�[�h�A�E�g�ƃV�[���J�ڂ��R���[�`���Ŏ��s
        }
    }

    private System.Collections.IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        float elapsedTime = 0f;

        // �t�F�[�h�A�E�g�����i��ʂ��Â�����j
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // �����x�𑝂₵�ĉ�ʂ��Â�����
            fadeImage.color = new Color(0f, 0f, 0f, alpha); // ���Ńt�F�[�h�A�E�g
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 1f); // ���S�ɈÂ�����
        Debug.Log("�t�F�[�h�A�E�g����");

        // �V�[���J��
        SceneManager.LoadScene(sceneName);
    }
}
