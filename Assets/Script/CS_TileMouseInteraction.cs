using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CS_TileMouseInteraction : MonoBehaviour
{
    private Tilemap tilemap;
    public float maxDistance = 5f; // マウスカーソルとの距離の最大値

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            // マウスの位置をワールド座標に変換
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0; // Z座標を0に設定（2Dの場合）
            Vector3Int tilePosition = tilemap.WorldToCell(worldPosition);

            TileBase tile = tilemap.GetTile(tilePosition);
            if (tile != null)
            {
                // タイルのオブジェクトを取得
                Vector3 tileWorldPosition = tilemap.GetCellCenterWorld(tilePosition);
                float distance = Vector3.Distance(tileWorldPosition, worldPosition);

                // 距離に基づいて透明度を計算
                float alpha = Mathf.Clamp01(1 - (distance / maxDistance));

                // タイルの色を変更（タイルのスプライトの色を変更）
                if (tile is Tile myTile)
                {
                    Color color = myTile.color;
                    color.a = alpha;
                    myTile.color = color;
                }

                Debug.Log("Tile clicked: " + tilePosition);
                // ここでタイルがクリックされた時の処理を行います
            }
        }
    }
}