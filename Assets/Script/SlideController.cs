using System.Collections;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    public GameObject[] slides; // �X���C�h�摜�̔z��
    public float transitionDuration = 1.0f; // �X���C�h�̐؂�ւ�����
    private int currentSlideIndex = 0; // ���݂̃X���C�h�̃C���f�b�N�X

    void Start()
    {
        // �����ݒ�: ���ׂẴX���C�h���\���ɂ���
        foreach (GameObject slide in slides)
        {
            slide.SetActive(false);
        }

        // �ŏ��̃X���C�h��\������
        slides[currentSlideIndex].SetActive(true);
    }

    public void NextSlide()
    {
        StartCoroutine(SlideTransition(true));
    }

    public void PrevSlide()
    {
        StartCoroutine(SlideTransition(false));
    }

    private IEnumerator SlideTransition(bool isNext)
    {
        // ���̃X���C�h�̃C���f�b�N�X���v�Z
        int nextSlideIndex = isNext ? (currentSlideIndex + 1) % slides.Length : (currentSlideIndex - 1 + slides.Length) % slides.Length;

        // ���݂̃X���C�h�Ǝ��̃X���C�h���擾
        GameObject currentSlide = slides[currentSlideIndex];
        GameObject nextSlide = slides[nextSlideIndex];

        // ���̃X���C�h��\�����A�����ʒu��ݒ�
        nextSlide.SetActive(true);
        RectTransform nextSlideRect = nextSlide.GetComponent<RectTransform>();
        RectTransform currentSlideRect = currentSlide.GetComponent<RectTransform>();

        // ���̃X���C�h�̈ʒu����ʊO����J�n
        Vector3 nextSlideStartPosition = isNext ? new Vector3(Screen.width, 0, 0) : new Vector3(-Screen.width, 0, 0);
        nextSlideRect.position = nextSlideStartPosition;

        // �A�j���[�V�����J�n
        float elapsedTime = 0f;
        Vector3 currentSlideEndPosition = isNext ? new Vector3(-Screen.width, 0, 0) : new Vector3(Screen.width, 0, 0);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / transitionDuration;

            // ���݂̃X���C�h����ʊO�ֈړ�
            currentSlideRect.position = Vector3.Lerp(currentSlideRect.position, currentSlideEndPosition, progress);

            // ���̃X���C�h����ʓ��ֈړ�
            nextSlideRect.position = Vector3.Lerp(nextSlideStartPosition, currentSlide.transform.position, progress);

            yield return null;
        }

        // �ŏI�I�Ȉʒu��ݒ�
        currentSlideRect.position = currentSlideEndPosition;
        nextSlideRect.position = currentSlide.transform.position;

        // ���݂̃X���C�h���\���ɂ���
        currentSlide.SetActive(false);

        // ���݂̃X���C�h�C���f�b�N�X���X�V
        currentSlideIndex = nextSlideIndex;
    }
}
