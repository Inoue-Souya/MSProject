using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CS_TileMouseInteraction : MonoBehaviour
{
    private Tilemap tilemap;
    public float maxDistance = 5f; // �}�E�X�J�[�\���Ƃ̋����̍ő�l

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0; // Z���W��0�ɐݒ�i2D�̏ꍇ�j
            Vector3Int tilePosition = tilemap.WorldToCell(worldPosition);

            TileBase tile = tilemap.GetTile(tilePosition);
            if (tile != null)
            {
                // �^�C���̃I�u�W�F�N�g���擾
                Vector3 tileWorldPosition = tilemap.GetCellCenterWorld(tilePosition);
                float distance = Vector3.Distance(tileWorldPosition, worldPosition);

                // �����Ɋ�Â��ē����x���v�Z
                float alpha = Mathf.Clamp01(1 - (distance / maxDistance));

                // �^�C���̐F��ύX�i�^�C���̃X�v���C�g�̐F��ύX�j
                if (tile is Tile myTile)
                {
                    Color color = myTile.color;
                    color.a = alpha;
                    myTile.color = color;
                }

                Debug.Log("Tile clicked: " + tilePosition);
                // �����Ń^�C�����N���b�N���ꂽ���̏������s���܂�
            }
        }
    }
}