using UnityEngine;

public class CS_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // メインカメラ
    public float zoomSpeed = 0.5f;  // ズーム速度
    public float minZoom = 2f;  // 最小ズーム
    private float initialZoom;  // カメラの初期ズーム（ズームアウトの上限）

    private Vector3 dragOrigin;  // ドラッグ開始位置（ワールド空間）
    private Vector3 initialPosition;  // カメラの初期位置

    private float minX, maxX;  // 初期の可視範囲 (X軸のみ)
    public float minY;  // Y軸の下限（調整可能）
    public float maxY;  // Y軸の上限（調整可能）

    void Start()
    {
        initialPosition = mainCamera.transform.position;  // 初期位置を保存
        initialZoom = mainCamera.orthographicSize;  // 初期ズームを基準とする

        // 初期位置でのカメラのX軸の可視範囲を計算
        CalculateBounds();
    }

    void Update()
    {
        HandleZoom();  // ズーム処理
        HandleDrag();  // ドラッグ処理
        HandleReset();  // リセット処理
    }

    // カメラの初期位置とズームを基準に、X軸の表示範囲を計算
    void CalculateBounds()
    {
        float cameraWidth = initialZoom * mainCamera.aspect;

        minX = initialPosition.x - cameraWidth;
        maxX = initialPosition.x + cameraWidth;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float currentZoom = mainCamera.orthographicSize;
            float newZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, initialZoom);

            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mouseWorldPos - mainCamera.transform.position;

            mainCamera.transform.position += direction * (currentZoom - newZoom) / currentZoom;
            mainCamera.orthographicSize = newZoom;

            // ズーム後の位置をX軸とY軸の範囲内に制限
            mainCamera.transform.position = ClampPosition(mainCamera.transform.position);
        }
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))  // ドラッグ開始時にワールド座標を記録
        {
            dragOrigin = GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0))  // ドラッグ中
        {
            Vector3 difference = dragOrigin - GetMouseWorldPosition();
            Vector3 newPosition = mainCamera.transform.position + new Vector3(difference.x, difference.y, 0);

            // X軸とY軸の移動範囲を制限
            mainCamera.transform.position = ClampPosition(newPosition);
        }
    }

    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))  // Rキーでリセット
        {
            mainCamera.transform.position = initialPosition;
            mainCamera.orthographicSize = initialZoom;
        }
    }

    // マウスのワールド座標を取得するヘルパーメソッド
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;  // カメラのZ軸を考慮
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    // X軸とY軸の移動範囲を設定し、初期表示範囲内に制限
    Vector3 ClampPosition(Vector3 targetPosition)
    {
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraHeight = mainCamera.orthographicSize;

        // X軸の移動範囲を制限
        float clampedX = Mathf.Clamp(targetPosition.x, minX + cameraWidth, maxX - cameraWidth);

        // ズームに応じてY軸の下限を調整
        float dynamicMinY = initialPosition.y - initialZoom + mainCamera.orthographicSize;
        float clampedY = Mathf.Clamp(targetPosition.y, dynamicMinY, maxY);

        return new Vector3(clampedX, clampedY, targetPosition.z);
    }
}
