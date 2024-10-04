using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CS_TileChanger : MonoBehaviour
{
    public Tilemap tilemap; // �^�C���}�b�v���A�T�C������
    public Tile newTile; // �ύX�������^�C�����A�T�C������
    private Vector3Int lastClickedCell;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // �E�N���b�N
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
            lastClickedCell = cellPosition; // �N���b�N�����^�C���̈ʒu��ۑ�
        }
    }

    public void ChangeTile()
    {
        tilemap.SetTile(lastClickedCell, newTile); // �ۑ������ʒu�ɐV�����^�C�����Z�b�g
    }
}
