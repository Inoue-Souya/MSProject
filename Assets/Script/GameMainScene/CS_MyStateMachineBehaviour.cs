using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MyStateMachineBehaviour : StateMachineBehaviour
{
    public string audioSourceObjectName = "";
    public AudioClip soundEffect; // ���ʉ���AudioClip(�{�[�i�X����)
    private AudioSource audioSource; // AudioSource�R���|�[�l���g

    // �A�j���[�V�����X�e�[�g���J�n���鎞
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("�A�j���[�V�����J�n���ɌĂ΂��֐�");

        // Animator��GameObject����AudioSourceObject��T��
        if (audioSource == null)
        {
            GameObject audioSourceObject = GameObject.Find(audioSourceObjectName);
            if (audioSourceObject != null)
            {
                audioSource = audioSourceObject.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogWarning("AudioSource�I�u�W�F�N�g��������܂���: " + audioSourceObjectName);
            }
        }

        // ���ʉ����Đ�
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }

        if (stateInfo.IsName("CutIn"))
        {
            animator.SetBool("PlayCutIn", false);  // CutIn���n�܂�����PlayCutIn��false�ɐݒ�
        }
    }

    // �A�j���[�V�����X�e�[�g���I�����鎞
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("�A�j���[�V�����I�����ɌĂ΂��֐�");

        animator.SetBool("PlayCutIn", false);

        // �f�o�b�O�p
        Debug.Log("�A�j���[�V�����I������bool�ϐ���false�ɐݒ�");
    }

    // �A�j���[�V�����X�e�[�g���J�ڂ��鎞
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("�X�e�[�g�}�V���J�n���ɌĂ΂��֐�");
    }
}
