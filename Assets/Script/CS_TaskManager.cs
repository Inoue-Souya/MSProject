using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_TaskManager : MonoBehaviour
{
    private int task;               // ���݂̃^�X�N��
    private int Max_task;           // �^�X�N�ʂ̍ő�l
    public CS_FadeOutIn FadeOutIn;   // �t�F�[�h�C���A�E�g�֐��Ăяo���p�ϐ�
    public Text taskCount;          // �^�X�N�ʕ\���e�L�X�g
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        // �^�X�N�ʏ����l
        task = 3;
        Max_task = task;

        // �^�X�N�ʕ\�������ݒ�
        taskCount.text = "�^�X�N�ʁF" + task;
    }

    // Update is called once per frame
    void Update()
    {
        // ���x���ɂ���ă^�X�N�ʂ���������Ȃ炱�̏������ׂ����ύX
        // if(���x�������ȏ�)
        // {
        //     Max_task += 1;
        // }

        if (task <= 0)
        {
            Panel.SetActive(false);

            // �t�F�[�h�C���A�E�g�J�n
            FadeOutIn.StartFadeOutIn();

            // �^�X�N�ʃ��Z�b�g
            taskReset();
        }
    }

    public void OnButtonClick()
    {
        task -= 1;
        Debug.Log("�^�X�N����" + task);
        taskCount.text = "�^�X�N�ʁF" + task;
    }

    private void taskReset()
    { 
        task = Max_task;
    }

    public int GetTask()
    {
        return task;
    }
}
