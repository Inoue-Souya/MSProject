using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // AudioSource�R���|�[�l���g
    private AudioSource audioSource;

    // BGM�Ƃ��čĐ�����AudioClip���w��
    public AudioClip bgmClip;

    void Start()
    {
        // AudioSource���擾
        audioSource = GetComponent<AudioSource>();

        // AudioClip���Z�b�g
        audioSource.clip = bgmClip;

        // ���[�v�Đ���L���ɂ���
        audioSource.loop = true;

        // BGM���Đ�
        if (audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log("BGM is playing.");
        }
        else
        {
            Debug.Log("AudioClip is not set.");
        }
    }

}