using System.Collections;
using System.Collections.Generic;
using UnityEngine;




////////////////////////////////////// ////////////////
//・機能説明・
//マウスホイールでのズームイン・アウト
//左クリックドラッグでのカメラ移動
//Rキーでカメラの位置とズームを初期状態に戻す機能
/// ///////////////////////////////////////////////////
public class CS_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // メインカメラ
    public float zoomSpeed = 1f;  // ズーム速度
    public float minZoom = 2f;  // 最小ズーム
    public float maxZoom = 5f;  // 最大ズーム

    private Vector3 dragOrigin;  // ドラッグ開始位置
    private Vector3 initialPosition;  // カメラの初期位置
    private float initialZoom;  // カメラの初期ズーム

    void Start()
    {
        // 初期位置とズームを保存
        initialPosition = mainCamera.transform.position;
        initialZoom = mainCamera.orthographicSize;
    }

    void Update()
    {
        HandleZoom();  // ズーム処理
        HandleDrag();  // ドラッグ処理
        HandleReset();  // カメラのリセット処理
    }

    // マウスホイールによるズーム処理
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float currentZoom = mainCamera.orthographicSize;
            float newZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);

            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mouseWorldPos - mainCamera.transform.position;

            mainCamera.transform.position += direction * (currentZoom - newZoom) / currentZoom;
            mainCamera.orthographicSize = newZoom;
        }
    }

    // 左クリックでのドラッグ移動処理
    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))  // 左クリックを押したとき
        {
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))  // 左クリックをホールドしている間
        {
            Vector3 difference = dragOrigin - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mainCamera.transform.position += difference;
        }
    }

    // Rキーでカメラを初期位置に戻す処理
    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))  // Rキーが押されたら
        {
            mainCamera.transform.position = initialPosition;
            mainCamera.orthographicSize = initialZoom;
        }
    }
}
