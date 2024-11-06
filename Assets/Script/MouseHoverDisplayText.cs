using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MouseHoverDisplayText : MonoBehaviour
{
    public Text displayText; // �e�L�X�g��\�����邽�߂�UI Text�R���|�[�l���g
    private CS_Room room; // CS_Room �X�N���v�g�̎Q��
    public Vector3 textOffset = new Vector3(0, 1f, 0); // y��������1.0�̃I�t�Z�b�g

    private void Start()
    {
        // CS_Room�R���|�[�l���g���擾
        room = GetComponent<CS_Room>();

        // ���߂̓e�L�X�g���\���ɂ���
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // �}�E�X�̈ʒu���擾���ă��[���h���W�ɕϊ�
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �}�E�X�ʒu��Box Collider2D�ɏd�Ȃ��Ă��邩���`�F�b�N
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);
        if (collider != null && collider == GetComponent<BoxCollider2D>())
        {
            // �}�E�X���R���C�_�[�ɓ������Ă���ꍇ�Ƀe�L�X�g��\��
            if (displayText != null)
            {
                // attributes�̓��e��\���p�e�L�X�g�ɕϊ�
                displayText.text = GetRoomAttributesText();
                displayText.gameObject.SetActive(true);

                Vector3 displayPosition = transform.position + textOffset;
                displayText.transform.position = Camera.main.WorldToScreenPoint(displayPosition);
            }
        }
        else
        {
            // �d�Ȃ��Ă��Ȃ��ꍇ�A���b�Z�[�W���\��
            if (displayText != null)
            {
                displayText.gameObject.SetActive(false);
            }
        }
    }


    private string GetRoomAttributesText()
    {
        // `attributes` ���X�g�� null �̏ꍇ�ɋ�̕������Ԃ�
        if (room.attributes == null)
        {
            return "";
        }

        string attributesText = ""; // ��̕�����ŏ�����

        // �e��������s���e�L�X�g�ɒǉ�
        foreach (var attribute in room.attributes)
        {
            attributesText += $"{attribute.attributeName}: {attribute.matchScore}\n";
        }

        return attributesText.TrimEnd(); // �Ō�̉��s���폜���Ă���Ԃ�
    }

}


