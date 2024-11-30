using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MyStateMachineBehaviour : StateMachineBehaviour
{
    // �A�j���[�V�����X�e�[�g���J�n���鎞
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("�A�j���[�V�����J�n���ɌĂ΂��֐�");

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
