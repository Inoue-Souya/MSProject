using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CS_TileChanger : MonoBehaviour
{
    public Tilemap tilemap; // タイルマップをアサインする
    public Tile newTile; // 変更したいタイルをアサインする
    private Vector3Int lastClickedCell;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 右クリック
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
            lastClickedCell = cellPosition; // クリックしたタイルの位置を保存
        }
    }

    public void ChangeTile()
    {
        tilemap.SetTile(lastClickedCell, newTile); // 保存した位置に新しいタイルをセット
    }
}
