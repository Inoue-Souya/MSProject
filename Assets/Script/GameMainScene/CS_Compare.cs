using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Compare : MonoBehaviour
{

    private float scoreType;

    public float CompareCharactor(int nameNumber,string name)
    {
        switch (nameNumber)
        {
            case 0:
                if(name == "�}�X�R�b�g"�@|| name == "���")
                {
                    scoreType = 0;
                }
                else if(name == "���ǂ�������" || name == "�ł�����ڂ���"|| name == "�V��" )
                {
                    scoreType = 1;
                }
                else 
                {
                    scoreType = 2;
                }
                break;
            case 1:
                if (name == "���ǂ�������" || name == "�e�q������")
                {
                    scoreType = 0;
                }
                else if (name == "�}�X�R�b�g" || name == "��ڏ��m")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 2:
                if (name == "�ł�����ڂ���" || name == "�Ꮧ")
                {
                    scoreType = 0;
                }
                else if (name == "�}�X�R�b�g" || name == "���" || name == "���e"
                    || name == "�ʂ��Ђ��" || name == "�e�q������")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 3:
                if (name == "��ڏ��m" || name == "�ʂ��Ђ��")
                {
                    scoreType = 0;
                }
                else if (name == "�}�X�R�b�g" || name == "���ǂ�������" || name == "�ł�����ڂ���"
                    || name == "�V��" || name == "���e" || name == "�e�q������")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 4:
                if (name == "���" || name == "���e")
                {
                    scoreType = 0;
                }
                else if (name == "�Ꮧ" || name == "�e�q������")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 5:
                if (name == "���ǂ�������" || name == "�ł�����ڂ���" || name == "�Ꮧ")
                {
                    scoreType = 0;
                }
                else if (name == "��ڏ��m" || name == "�ʂ��Ђ��")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 6:
                if (name == "�}�X�R�b�g" || name == "�V��")
                {
                    scoreType = 0;
                }
                else if (name == "��ڏ��m" || name == "���" 
                    || name == "���e"|| name == "�ʂ��Ђ��")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 7:
                if (name == "���e" )
                {
                    scoreType = 0;
                }
                else if (name == "���ǂ�������" || name == "�Ꮧ" || name == "�e�q������")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 8:
                if (name == "��ڏ��m" || name == "�ʂ��Ђ��")
                {
                    scoreType = 0;
                }
                else if (name == "�ł�����ڂ���" || name == "���"
                    || name == "�Ꮧ" || name == "�V��")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
            case 9:
                if (name == "�V��" || name == "�e�q������")
                {
                    scoreType = 0;
                }
                else if (name == "���")
                {
                    scoreType = 1;
                }
                else
                {
                    scoreType = 2;
                }
                break;
        }

        return scoreType;
    }
}
