using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CS_AddResident : MonoBehaviour
{
    //public GameObject confirmationDialog; // �m�F�_�C�A���O
    //private Vector3Int selectedPosition; // �I�������󂫕����̈ʒu
    //private Resident selectedResident; // �I�����ꂽ�Z��
    //public Tilemap tilemap; // �^�C���}�b�v���C���X�y�N�^�[�Ŏw��


    //void Start()
    //{
    //    selectedResident = SelectedResidentData.Instance.selectedResident; // �I�����ꂽ�Z�����擾
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector3Int tilePos = tilemap.WorldToCell(mousePos);

    //        if (IsVacantTile(tilePos)) // �^�C�����󂢂Ă��邩�m�F
    //        {
    //            selectedPosition = tilePos; // �󂫕����̈ʒu��ۑ�
    //            ShowConfirmationDialog(); // �m�F�_�C�A���O��\��
    //        }
    //    }
    //}

    //private bool IsVacantTile(Vector3Int position)
    //{
    //    TileBase tile = tilemap.GetTile(position);
    //    return tile == CS_ResidentsManager.Instance.vacantTile; // vacantTile�����O�ɐݒ�
    //}

    //private void ShowConfirmationDialog()
    //{
    //    confirmationDialog.SetActive(true); // �_�C�A���O��\��
    //}

    //public void OnConfirm()
    //{
    //    if (selectedResident != null)
    //    {
    //        CS_ResidentsManager.Instance.AddResident(selectedPosition, selectedResident); // �Z����ǉ�
    //        confirmationDialog.SetActive(false); // �_�C�A���O�����
    //    }
    //}

    //public void OnCancel()
    //{
    //    confirmationDialog.SetActive(false); // �_�C�A���O�����
    //}
}
