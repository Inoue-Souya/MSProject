using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CS_TileClickManager : MonoBehaviour
{
    //public Tilemap tilemap; // タイルマップをインスペクターで指定
    //public GameObject dialog; // ダイアログPanelをインスペクターで指定
    //public Text residentName; // 名前表示
    //public Text residentInfo; // 情報表示
    //public Image residentPortrait; // 住民の画像表示
    //public CS_ResidentsManager residentManager; // 住民管理用マネージャーの参照

    //private Vector3Int tilePos; // 最後にクリックしたタイルの位置
    //private bool isDialogOpen = false; // ダイアログが開いているかどうかのフラグ

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) && !isDialogOpen) // 左クリックし、ダイアログが開いていない場合
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

    //        // 未入居タイルをクリックした場合
    //        if (clickedTile == residentManager.vacantTile)
    //        {
    //            residentManager.AddResidentToTile(tilePos);
    //        }
    //    }
    //}

    //void ShowDialog(Resident resident)
    //{
    //    dialog.SetActive(true);
    //    isDialogOpen = true; // ダイアログを開いたらフラグを更新
    //    residentName.text = resident.name;
    //    residentPortrait.sprite = resident.portrait; // 画像を設定
    //}

    //public void CloseDialog()
    //{
    //    dialog.SetActive(false); // ダイアログを非表示
    //    isDialogOpen = false; // ダイアログを閉じたらフラグをリセット
    //}

    //public void OnChooseResidentButtonClick()
    //{
    //    // 住民要望を生成
    //    ResidentRequest request = new ResidentRequest
    //    {
    //        personality = "善", // 住民1と一致
    //        age = 25, // 住民1と一致
    //        gender = "男" // 住民1の性別は「男」
    //    };

    //    // 住民を選択
    //    residentManager.ChooseResident(tilePos, request);
    //    CloseDialog(); // ダイアログを閉じる
    //}
}
