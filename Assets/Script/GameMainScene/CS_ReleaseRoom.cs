using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CS_ReleaseRoom : MonoBehaviour
{
    [Header("�L�����o�X�֘A")]
    public Canvas canvas; // �p�l���̑�����L�����o�X
    public GameObject panel; // �\��������p�l��
    public Button panelButton; // �p�l�����̃{�^���i�I�����͂��j
    public Text yesText;
    [Header("���C���J����")]
    public Camera mainCamera; // 2D�J����
    [Header("�X�R�A�}�l�[�W���[")]
    public CS_ScoreManager scoreManager;

    private int releaseCost = 0; // �����J���R�X�g
    private CS_Room selectedRoom; // �q�b�g����Room�I�u�W�F�N�g�̎Q��
    public Material defaultMaterial; // �f�t�H���g�̃}�e���A��

    private void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        // �E�N���b�N�������ꂽ�Ƃ�
        if (Input.GetMouseButtonDown(1))
        {
            // UI��ŃN���b�N���ꂽ�ꍇ�͖���
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // �}�E�X�̃��[���h���W���擾���A���C�L���X�g���΂�
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // �����I�u�W�F�N�g�Ƀq�b�g�����ꍇ
            if (hit.collider != null)
            {
                // �I�u�W�F�N�g�̃^�O��"Room"���ǂ����m�F
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Room"))
                {
                    // �p�l����\��
                    panel.SetActive(true);

                    // �p�l�����N���b�N�ʒu�Ɉړ��iCanvas��RenderMode�ɉ����ĕϊ��j
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvas.transform as RectTransform,
                        Input.mousePosition,
                        canvas.worldCamera,
                        out Vector2 localPoint);

                    panel.transform.localPosition = localPoint;

                    // �E�N���b�N���������̏����擾
                    clickedObject.TryGetComponent<CS_Room>(out selectedRoom);

                    // ����R�X�g��ۑ�
                    releaseCost = selectedRoom.unlockCost * 100;

                    // ����R�X�g��I�����i�͂��j�ɕ\��
                    yesText.text = "�͂�(" + releaseCost + "��)";

                    // �X�R�A������R�X�g�ȏ�Ń{�^����L����
                    panelButton.interactable = scoreManager.currentScore >= releaseCost;
                }
                else
                {
                    // �^�O��"Room"�łȂ��ꍇ�̓p�l�����\���ɂ���
                    panel.SetActive(false);
                }
            }
            else
            {
                // �q�b�g���Ȃ������ꍇ�̓p�l�����\���ɂ���
                panel.SetActive(false);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
           // panel.SetActive(false);
        }
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��̏���
    public void OnbuttonClick()
    {
        // �X�R�A������
        scoreManager.SpendScore(releaseCost);

        // �I�����ꂽ������Sprite��Material���f�t�H���g�ɖ߂�
        if (selectedRoom != null)
        {
            SpriteRenderer spriteRenderer = selectedRoom.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.material = defaultMaterial;
            }
        }

        selectedRoom.InitializeRoom(true);

        // �p�l�����\���ɂ���
        panel.SetActive(false);
    }
}
