using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CS_TileClickManager : MonoBehaviour
{
    //public Tilemap tilemap; // �^�C���}�b�v���C���X�y�N�^�[�Ŏw��
    //public GameObject dialog; // �_�C�A���OPanel���C���X�y�N�^�[�Ŏw��
    //public Text residentName; // ���O�\��
    //public Text residentInfo; // ���\��
    //public Image residentPortrait; // �Z���̉摜�\��
    //public CS_ResidentsManager residentManager; // �Z���Ǘ��p�}�l�[�W���[�̎Q��

    //private Vector3Int tilePos; // �Ō�ɃN���b�N�����^�C���̈ʒu
    //private bool isDialogOpen = false; // �_�C�A���O���J���Ă��邩�ǂ����̃t���O

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) && !isDialogOpen) // ���N���b�N���A�_�C�A���O���J���Ă��Ȃ��ꍇ
    //    {
    //        HandleTileClick();
    //    }
    //}
    //void HandleTileClick()
    //{
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    tilePos = tilemap.WorldToCell(mousePos);
    //    TileBase clickedTile = tilemap.GetTile(tilePos);

    //    if (clickedTile != null)
    //    {
    //        Resident resident = residentManager.GetResident(tilePos);
    //        if (resident != null)
    //        {
    //            ShowDialog(resident);
    //        }

    //        // �������^�C�����N���b�N�����ꍇ
    //        if (clickedTile == residentManager.vacantTile)
    //        {
    //            residentManager.AddResidentToTile(tilePos);
    //        }
    //    }
    //}

    //void ShowDialog(Resident resident)
    //{
    //    dialog.SetActive(true);
    //    isDialogOpen = true; // �_�C�A���O���J������t���O���X�V
    //    residentName.text = resident.name;
    //    residentPortrait.sprite = resident.portrait; // �摜��ݒ�
    //}

    //public void CloseDialog()
    //{
    //    dialog.SetActive(false); // �_�C�A���O���\��
    //    isDialogOpen = false; // �_�C�A���O�������t���O�����Z�b�g
    //}

    //public void OnChooseResidentButtonClick()
    //{
    //    // �Z���v�]�𐶐�
    //    ResidentRequest request = new ResidentRequest
    //    {
    //        personality = "�P", // �Z��1�ƈ�v
    //        age = 25, // �Z��1�ƈ�v
    //        gender = "�j" // �Z��1�̐��ʂ́u�j�v
    //    };

    //    // �Z����I��
    //    residentManager.ChooseResident(tilePos, request);
    //    CloseDialog(); // �_�C�A���O�����
    //}
}
