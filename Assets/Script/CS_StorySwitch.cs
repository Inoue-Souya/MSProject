using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �ǉ�
using UnityEngine.EventSystems; // EventTrigger�p

public class CS_StorySwitch : MonoBehaviour
{
    public Button fadeOutButton; // �t�F�[�h�A�E�g�J�n�p�̃{�^��
    public UnityEngine.UI.Image[] images;
    public float slideSpeed = 5f;
    public AudioClip clickClip;
    public AudioClip bgmClip;
    public float bgmVolume = 0.5f;
    public float clickVolume = 1.0f;
    private AudioSource bgmSource;
    private AudioSource clickSource;
    private int currentIndex = 0;
    private bool isSliding = false; // �X���C�h�����ǂ������Ǘ�����t���O
    public float fadeDuration = 1f;
    private bool isFading = false;
    public string nextSceneName = "NextScene"; // ���̃V�[����
    private bool isButtonClicked = false; // �{�^���������ꂽ���ǂ������Ǘ�����t���O
    private bool isPointerOverButton = false; // �|�C���^���{�^����ɂ��邩�ǂ���

    public CS_FadeIn fade;

    private Coroutine blinkCoroutine; // �_�ŗp��Coroutine

    void Start()
    {
        // �{�^�����ŏ��ɔ�\���ɂ���
        if (fadeOutButton != null)
        {
            fadeOutButton.gameObject.SetActive(false); // ��\���ɐݒ�
        }

        // BGM�p��AudioSource���쐬���A�����ݒ�
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();

        // �N���b�N���p��AudioSource���쐬
        clickSource = gameObject.AddComponent<AudioSource>();
        clickSource.clip = clickClip;
        clickSource.volume = clickVolume;

        // �{�^���̃N���b�N�C�x���g��StartFadeOut��o�^
        if (fadeOutButton != null)
        {
            fadeOutButton.onClick.AddListener(() =>
            {
                if (!isSliding) // �X���C�h���łȂ��ꍇ�̂݃{�^����������
                {
                    isButtonClicked = true;   // �t���O��ݒ�
                    PlayClickSound();         // �N���b�N�����Đ�
                    StartFadeOut();           // �t�F�[�h�A�E�g���J�n
                }
            });

            // �{�^����EventTrigger��ǉ����A�J�[�\������ɏ�������Ƃ����o
            EventTrigger trigger = fadeOutButton.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            pointerEnterEntry.callback.AddListener((data) => OnPointerEnter());
            trigger.triggers.Add(pointerEnterEntry);

            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            pointerExitEntry.callback.AddListener((data) => OnPointerExit());
            trigger.triggers.Add(pointerExitEntry);
        }
    }

    void Update()
    {
        if (fade.fadeFinish)
        {
            // �X���C�h���łȂ��A�J�[�\�����{�^����ɂȂ��ꍇ�̂݃X���C�h���������s
            if (Input.GetMouseButtonDown(0) && currentIndex < images.Length && !isSliding && !isFading && !isButtonClicked && !isPointerOverButton)
            {
                PlayClickSound();
                if (currentIndex == images.Length - 1)
                {
                    StartFadeOut();
                }
                else
                {
                    StartSliding();
                }
            }

            if (isSliding)
            {
                SlideCurrentImage();
            }

            if (isFading)
            {
                FadeOutCurrentImage();
            }

            // �ŏ��̃t�F�[�h�C��������������Ƀ{�^����\������
            if (fade.fadeFinish && fadeOutButton != null && !fadeOutButton.gameObject.activeSelf)
            {
                // Coroutine�Œx����ǉ����ă{�^����\��
                StartCoroutine(ShowButtonWithDelay(0f)); // 1�b�x��
            }
        }
    }

    IEnumerator ShowButtonWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���҂�
        fadeOutButton.gameObject.SetActive(true); // �{�^����\��
    }

    void PlayClickSound()
    {
        if (clickSource != null && clickSource.clip != null)
        {
            clickSource.Play(); // �N���b�N�����Đ�
        }
    }

    void StartSliding()
    {
        if (currentIndex < images.Length)
        {
            images[currentIndex].transform.localPosition = Vector3.zero;
            isSliding = true; // �X���C�f�B���O���J�n
            fadeOutButton.interactable = false; // �X���C�h���̓{�^���𖳌���
        }
    }

    void SlideCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex];
        float imageWidth = currentImage.rectTransform.rect.width; // �摜�̕����擾

        if (currentIndex < images.Length - 1)
        {
            if (currentImage.transform.localPosition.x > -imageWidth)
            {
                currentImage.transform.localPosition += Vector3.left * slideSpeed * Time.deltaTime;
            }
            else
            {
                currentIndex++;
                isSliding = false; // �X���C�f�B���O�I��
                fadeOutButton.interactable = true; // �X���C�h�I����A�{�^�����ēx�L����
            }
        }
        else
        {
            currentImage.transform.localPosition = Vector3.zero;
        }
    }

    void StartFadeOut()
    {
        if (!isFading) // ���łɃt�F�[�h���łȂ��ꍇ�̂ݎ��s
        {
            isFading = true;
            fadeOutButton.interactable = false; // �t�F�[�h�A�E�g���̓{�^���𖳌���
            Color currentColor = images[currentIndex].color;
            images[currentIndex].color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        }
    }

    void FadeOutCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex];
        Color currentColor = currentImage.color;
        float darkenAmount = Time.deltaTime / fadeDuration;
        currentImage.color = new Color(
            Mathf.Lerp(currentColor.r, 0f, darkenAmount),
            Mathf.Lerp(currentColor.g, 0f, darkenAmount),
            Mathf.Lerp(currentColor.b, 0f, darkenAmount),
            currentColor.a
        );

        // �t�F�[�h�i�s�󋵂����O�Ŋm�F
        UnityEngine.Debug.Log("�t�F�[�h�i�s��: " + currentImage.color);
        if (currentImage.color.r <= 0.1f && currentImage.color.g <= 0.1f && currentImage.color.b <= 0.1f)
        {
            isFading = false;
            UnityEngine.Debug.Log("�t�F�[�h���������܂����B���̃V�[���Ɉړ����܂��B");
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        UnityEngine.Debug.Log("�V�[���J��: " + nextSceneName);
        // ���̃V�[�������[�h
        SceneManager.LoadScene("Stage1");
    }

    // �{�^����ɃJ�[�\����������Ƃ��ɃX���C�h�𖳌������A�{�^����_�ł�����
    void OnPointerEnter()
    {
        isPointerOverButton = true; // �{�^����ɃJ�[�\���������
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // �����̓_�ł��~
        }
        blinkCoroutine = StartCoroutine(BlinkButton()); // �_�ł��ĊJ
    }

    // �{�^������J�[�\�������ꂽ�Ƃ��ɓ_�ł��~���A�{�^�������̐F�ɖ߂�
    void OnPointerExit()
    {
        isPointerOverButton = false; // �{�^������J�[�\�������ꂽ
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // �_�ł��~
            blinkCoroutine = null; // �Q�Ƃ��N���A
            ChangeButtonOpacity(1f); // �{�^���̓����x�����ɖ߂�
        }
    }

    // �{�^���̐F��ύX���郁�\�b�h
    void ChangeButtonOpacity(float alpha)
    {
        if (fadeOutButton != null)
        {
            Color color = fadeOutButton.image.color;
            color.a = alpha;
            fadeOutButton.image.color = color;
        }
    }

    // �{�^����_�ł�����R���[�`��
    IEnumerator BlinkButton()
    {
        while (isPointerOverButton)
        {
            ChangeButtonOpacity(0f); // �����ɂ���
            yield return new WaitForSeconds(0.5f); // 0.5�b�҂�
            ChangeButtonOpacity(1f); // ���̕\���ɖ߂�
            yield return new WaitForSeconds(0.5f); // 0.5�b�҂�
        }
    }
}
