using UnityEngine;
using UnityEngine.UI;

public class MouseHoverDisplayText : MonoBehaviour
{
    // �\��������������
    public string message = "Hello!";
    // �e�L�X�g��\�����邽�߂�UI Text�R���|�[�l���g
    public Text displayText;

    private void Start()
    {
        // ���߂͕������\���ɂ���
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
            // �}�E�X���R���C�_�[�ɓ������Ă���ꍇ
            if (displayText != null)
            {
                // �}�E�X���N���b�N�܂��̓z�[���h����Ă���ꍇ�A�e�L�X�g���\���ɂ���
                if (Input.GetMouseButton(0)) // ���N���b�N��������Ă��邩
                {
                    displayText.gameObject.SetActive(false);
                }
                else
                {
                    // �}�E�X���������Ă��邪�A�N���b�N����Ă��Ȃ��ꍇ�̂ݕ\��
                    displayText.text = message;
                    displayText.gameObject.SetActive(true);
                }
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
}
