using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CS_ReleaseRoom : MonoBehaviour
{
    [Header("キャンバス関連")]
    public Canvas canvas; // パネルの属するキャンバス
    public GameObject panel; // 表示させるパネル
    public Button panelButton; // パネル内のボタン（選択肢はい）
    public Text yesText;
    [Header("メインカメラ")]
    public Camera mainCamera; // 2Dカメラ
    [Header("スコアマネージャー")]
    public CS_ScoreManager scoreManager;

    private int releaseCost = 0; // 部屋開放コスト
    private CS_Room selectedRoom; // ヒットしたRoomオブジェクトの参照
    public Material defaultMaterial; // デフォルトのマテリアル

    private void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        // 右クリックが押されたとき
        if (Input.GetMouseButtonDown(1))
        {
            // UI上でクリックされた場合は無視
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // マウスのワールド座標を取得し、レイキャストを飛ばす
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // もしオブジェクトにヒットした場合
            if (hit.collider != null)
            {
                // オブジェクトのタグが"Room"かどうか確認
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Room"))
                {
                    // パネルを表示
                    panel.SetActive(true);

                    // パネルをクリック位置に移動（CanvasのRenderModeに応じて変換）
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvas.transform as RectTransform,
                        Input.mousePosition,
                        canvas.worldCamera,
                        out Vector2 localPoint);

                    panel.transform.localPosition = localPoint;

                    // 右クリックした部屋の情報を取得
                    clickedObject.TryGetComponent<CS_Room>(out selectedRoom);

                    // 解放コストを保存
                    releaseCost = selectedRoom.unlockCost * 100;

                    // 解放コストを選択肢（はい）に表示
                    yesText.text = "はい(" + releaseCost + "怨)";

                    // スコアが解放コスト以上でボタンを有効化
                    panelButton.interactable = scoreManager.currentScore >= releaseCost;
                }
                else
                {
                    // タグが"Room"でない場合はパネルを非表示にする
                    panel.SetActive(false);
                }
            }
            else
            {
                // ヒットしなかった場合はパネルを非表示にする
                panel.SetActive(false);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
           // panel.SetActive(false);
        }
    }

    // ボタンがクリックされたときの処理
    public void OnbuttonClick()
    {
        // スコアを消費
        scoreManager.SpendScore(releaseCost);

        // 選択された部屋のSpriteのMaterialをデフォルトに戻す
        if (selectedRoom != null)
        {
            SpriteRenderer spriteRenderer = selectedRoom.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.material = defaultMaterial;
            }
        }

        selectedRoom.InitializeRoom(true);

        // パネルを非表示にする
        panel.SetActive(false);
    }
}
