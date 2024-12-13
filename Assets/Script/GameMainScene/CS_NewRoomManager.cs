using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms;               // �V�[�����̕����̔z��
    public int inResident;                // �Q�[���I�[�o�[�p�ϐ�
    public int openRoom;                  // ����ς݂̕�����
    public PulsatingVignette vignette;    // �r�l�b�g����X�N���v�g
    public AudioSource audioSource;       // SE�Đ��p��AudioSource
    public AudioClip warningSE;           // �x�����i���[�v�p�j

    private bool isVignetteActive = false; // �r�l�b�g�̏�Ԃ�ǐ�

    void Start()
    {
        //InitializeRooms();
        inResident = 0;
        openRoom = 5;

        if (vignette != null)
        {
            vignette.enabled = false; // �r�l�b�g���ŏ��͖�����
        }

        if (audioSource != null)
        {
            audioSource.loop = true; // ���[�v�Đ���L����
            audioSource.clip = warningSE; // �Đ����鉹��ݒ�
        }
    }

    private void InitializeRooms()
    {
        // ����ς݂̕�����ݒ肷��
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i < 5) // �ŏ���5����������ς݂ɂ���
            {
                rooms[i].InitializeRoom(true);
            }
            else
            {
                rooms[i].InitializeRoom(false);
            }
        }
    }

    private void Update()
    {
        gameObject.SetActive(true);

        // �Q�[���I�[�o�[���O�̏���
        if (openRoom - inResident <= 3 && inResident > 5)
        {
            ActivateVignetteAndSE();
        }
        else
        {
            DeactivateVignetteAndSE();
        }

        // �Q�[���I�[�o�[����
        if (inResident >= openRoom && openRoom >= 6)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void ActivateVignetteAndSE()
    {
        if (!isVignetteActive && vignette != null)
        {
            vignette.enabled = true; // �r�l�b�g��L����
            isVignetteActive = true;

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play(); // SE���Đ�
            }
        }
    }

    private void DeactivateVignetteAndSE()
    {
        if (isVignetteActive && vignette != null)
        {
            vignette.enabled = false; // �r�l�b�g�𖳌���
            isVignetteActive = false;

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop(); // SE���~
            }
        }
    }
}
