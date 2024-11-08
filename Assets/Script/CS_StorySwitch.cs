using UnityEngine;
using UnityEngine.UI; // UI ���O��Ԃ��g�p

public class CS_StorySwitch : MonoBehaviour
{
    public UnityEngine.UI.Image[] images; // �����I�� UnityEngine.UI.Image ���w��
    public float slideSpeed = 5f; // �X���C�h���x
    public AudioSource audioSource; // �T�E���h���Đ����� AudioSource �R���|�[�l���g
    public AudioClip audioclip;
    private int currentIndex = 0; // ���݂̉摜�̃C���f�b�N�X
    private bool isSliding = false; // �X���C�h�����ǂ����̃t���O

    void Start()
    {
        // AudioSource ���ݒ肳��Ă��Ȃ��ꍇ�͎����Œǉ�
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        // ���N���b�N�������ꂽ�Ƃ�
        if (Input.GetMouseButtonDown(0) && currentIndex < images.Length && !isSliding)
        {
            // �T�E���h���Đ�
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play(); // �T�E���h���Đ�
            }
            else
            {
                UnityEngine.Debug.LogWarning("AudioSource or AudioClip is missing!");
            }

            // ��ԏ�̉摜�����ɃX���C�h������
            StartSliding();
        }

        // �摜���X���C�h���Ă���ꍇ�̏���
        if (isSliding)
        {
            SlideCurrentImage();
        }
    }

    void StartSliding()
    {
        // �X���C�h����摜�����݂̃C���f�b�N�X�ɂ��邩�m�F
        if (currentIndex < images.Length)
        {
            images[currentIndex].transform.localPosition = Vector3.zero; // �ʒu��������
            isSliding = true; // �X���C�h�t���O���I��
        }
    }

    void SlideCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex]; // �����I�� UnityEngine.UI.Image ���w��

        // �摜����ʂ̍����𒴂���܂ŃX���C�h
        if (currentImage.transform.localPosition.x > -Screen.width)
        {
            currentImage.transform.localPosition += Vector3.left * slideSpeed * Time.deltaTime;
        }
        else
        {
            // �X���C�h�����������玟�̉摜�Ɉړ�
            currentIndex++;
            isSliding = false; // �X���C�h�t���O���I�t
        }
    }
}
