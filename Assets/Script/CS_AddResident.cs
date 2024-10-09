using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CS_AddResident : MonoBehaviour
{
    //public GameObject confirmationDialog; // 確認ダイアログ
    //private Vector3Int selectedPosition; // 選択した空き部屋の位置
    //private Resident selectedResident; // 選択された住民
    //public Tilemap tilemap; // タイルマップをインスペクターで指定


    //void Start()
    //{
    //    selectedResident = SelectedResidentData.Instance.selectedResident; // 選択された住民を取得
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector3Int tilePos = tilemap.WorldToCell(mousePos);

    //        if (IsVacantTile(tilePos)) // タイルが空いているか確認
    //        {
    //            selectedPosition = tilePos; // 空き部屋の位置を保存
    //            ShowConfirmationDialog(); // 確認ダイアログを表示
    //        }
    //    }
    //}

    //private bool IsVacantTile(Vector3Int position)
    //{
    //    TileBase tile = tilemap.GetTile(position);
    //    return tile == CS_ResidentsManager.Instance.vacantTile; // vacantTileを事前に設定
    //}

    //private void ShowConfirmationDialog()
    //{
    //    confirmationDialog.SetActive(true); // ダイアログを表示
    //}

    //public void OnConfirm()
    //{
    //    if (selectedResident != null)
    //    {
    //        CS_ResidentsManager.Instance.AddResident(selectedPosition, selectedResident); // 住民を追加
    //        confirmationDialog.SetActive(false); // ダイアログを閉じる
    //    }
    //}

    //public void OnCancel()
    //{
    //    confirmationDialog.SetActive(false); // ダイアログを閉じる
    //}
}
