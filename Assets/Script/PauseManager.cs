using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public Button hoverButton;         // �z�o�[�ŉ摜��\������{�^��
    public Image hoverImage;          // �\������摜


    private Transform originalParent;  // ���̐e�I�u�W�F�N�g��ۑ�
    private int originalSiblingIndex; // ���̏��Ԃ�ۑ�

    public GameObject pauseMenu;
    public GameObject slidePanel;
    public GameObject blockPanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button returnToTitleButton;
    public Button nextSlideButton;
    public Button prevSlideButton;
 private Vector3 originalScale;      // �{�^���̌��̃X�P�[��
    public float hoverScale = 0.9f;     // �z�o�[���̃X�P�[���{��
    public float animationSpeed = 0.1f; // �A�j���[�V�������x

    // �A�j���[�V�����̑��x�𐧌䂷��ϐ�
    public float hoverAnimationSpeed = 5.0f;
    private Coroutine hoverCoroutine;

    [SerializeField]
    private List<SlideUIControl> slides;

    private int currentSlideIndex = 0;
    private bool isSliding = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
        slidePanel.SetActive(false);
        blockPanel.SetActive(false);

        pauseButton.onClick.AddListener(TogglePauseMenu);
        resumeButton.onClick.AddListener(ResumeGame);
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        nextSlideButton.onClick.AddListener(NextSlide);
        prevSlideButton.onClick.AddListener(PrevSlide);

        InitializeSlides();

        hoverImage.gameObject.SetActive(false); // ������ԂŔ�\��

        // �{�^���̃C�x���g�ݒ�
        EventTrigger trigger = hoverButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { ShowHoverImage(); });
        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { HideHoverImage(); });
        trigger.triggers.Add(pointerExit);
        // �摜�̐e����ۑ�
        originalParent = hoverImage.transform.parent;
        originalSiblingIndex = hoverImage.transform.GetSiblingIndex();
        // ���̃X�P�[�����L��
        originalScale = returnToTitleButton.transform.localScale;

        // �{�^���ɃC�x���g���X�i�[��ݒ�
        UnityEngine.EventSystems.EventTrigger trigger2 = returnToTitleButton.gameObject.AddComponent<EventTrigger>();

        // �z�o�[�J�n��
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => OnButtonHover(true));
        trigger2.triggers.Add(entryEnter);

        // �z�o�[�I����
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => OnButtonHover(false));
        trigger2.triggers.Add(entryExit);
    }

    private void InitializeSlides()
    {
        for (int i = 0; i < slides.Count; i++)
        {
            slides[i].state = (i == 0) ? 1 : 0; // �ŏ��̃X���C�h���������ɕ\��
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            slidePanel.SetActive(true);
            blockPanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        slidePanel.SetActive(false);
        blockPanel.SetActive(false);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Scene");
    }

    public void NextSlide()
    {
        if (isSliding || slides.Count <= 1) return;

        StartCoroutine(SlideToNext());
    }

    public void PrevSlide()
    {
        if (isSliding || slides.Count <= 1) return;

        StartCoroutine(SlideToPrevious());
    }
    private IEnumerator SlideToNext()
    {
        isSliding = true;

        // ���̃X���C�h�̃C���f�b�N�X���v�Z
        int nextSlideIndex = (currentSlideIndex + 1) % slides.Count;

        // ���݂̃X���C�h���E�ɃX���C�h�A�E�g
        slides[currentSlideIndex].state = 2;

        // ���̃X���C�h�������ʒu (��) �ɐݒ肵�A�����փX���C�h�C��
        slides[nextSlideIndex].state = 0;
        slides[nextSlideIndex].transform.localPosition = slides[nextSlideIndex].outPos01;
        slides[nextSlideIndex].state = 1;

        // �A�j���[�V���������܂őҋ@
        yield return new WaitForSecondsRealtime(1.5f);

        // �C���f�b�N�X�X�V�ƃX���C�h�I������
        currentSlideIndex = nextSlideIndex;
        isSliding = false;
    }
    private IEnumerator SlideToPrevious()
    {
        isSliding = true;

        // �O�̃X���C�h�̃C���f�b�N�X���v�Z
        int prevSlideIndex = (currentSlideIndex - 1 + slides.Count) % slides.Count;

        // ���݂̃X���C�h�����ɃX���C�h�A�E�g
        slides[currentSlideIndex].state = 2;

        // �O�̃X���C�h���E���璆���Ɉړ�������ݒ�
        slides[prevSlideIndex].state = 0; // �������
        slides[prevSlideIndex].transform.localPosition = slides[prevSlideIndex].outPos02; // �E�ɐݒ�
        slides[prevSlideIndex].state = 1; // �����ɃX���C�h�C��

        // �A�j���[�V���������܂őҋ@
        yield return new WaitForSecondsRealtime(1.5f);

        // �C���f�b�N�X���X�V���ăX���C�h����������
        currentSlideIndex = prevSlideIndex;
        isSliding = false;
    }
    private void OnButtonHover(bool isHovering)
    {
        if (isHovering)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(returnToTitleButton.transform, hoverScale));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(returnToTitleButton.transform, originalScale.x));
        }
    }

    private IEnumerator ScaleButton(Transform buttonTransform, float targetScale)
    {
        Vector3 target = originalScale * targetScale;

        while (Vector3.Distance(buttonTransform.localScale, target) > 0.01f)
        {
            // ���݂̃X�P�[�������̃X�P�[���𒴂��Ȃ��悤�ɐ���
            Vector3 newScale = Vector3.Lerp(buttonTransform.localScale, target, animationSpeed);
            newScale = Vector3.Min(newScale, originalScale); // ���̃X�P�[���𒴂��Ȃ��悤�ɂ���
            buttonTransform.localScale = newScale;

            yield return null;
        }

        buttonTransform.localScale = target;
    }

    private void ShowHoverImage()
    {
        hoverImage.gameObject.SetActive(true);

        // �s�{�b�g������ɐݒ�
        hoverImage.rectTransform.pivot = new Vector2(0, 1);
        hoverImage.rectTransform.localScale = Vector3.zero; // �����X�P�[�����[���ɐݒ�

        // �őO�ʂɈړ�
        hoverImage.transform.SetParent(hoverImage.canvas.transform, true);
        hoverImage.transform.SetAsLastSibling();

        // �A�j���[�V�����J�n
        if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
        hoverCoroutine = StartCoroutine(AnimateHoverImage(Vector3.one)); // �W���X�P�[�� (1, 1, 1)
    }


    private void HideHoverImage()
    {
        if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
        hoverCoroutine = StartCoroutine(AnimateHoverImage(Vector3.zero, () =>
        {
            // �A�j���[�V����������ɔ�\���ƌ��̐e�ɖ߂�
            hoverImage.gameObject.SetActive(false);
            hoverImage.transform.SetParent(originalParent, true);
            hoverImage.transform.SetSiblingIndex(originalSiblingIndex);
        }));
    }

    private IEnumerator AnimateHoverImage(Vector3 targetScale, System.Action onComplete = null)
    {
        RectTransform rt = hoverImage.rectTransform;
        Vector3 initialScale = rt.localScale;

        // �A�j���[�V�����̂��߂̎��Ԃ��w��
        float duration = 0.2f; // ��: 0.5�b�ŃA�j���[�V����
        float time = 0;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / duration; // �A�j���[�V�����̐i�s�x

            // �X�P�[�����Ԃ��Đݒ�
            rt.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // �ŏI�X�P�[�����m��
        rt.localScale = targetScale;
    }



}
