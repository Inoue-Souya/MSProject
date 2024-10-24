using UnityEngine;

public class CS_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // メインカメラ
    public float zoomSpeed = 0.5f;  // ズーム速度
    public float minZoom = 2f;  // 最小ズーム
    public float maxZoom = 10f;  // 最大ズーム

    private Vector3 dragOrigin;  // ドラッグ開始位置（ワールド空間）
    private Vector3 initialPosition;  // カメラの初期位置
    private float initialZoom;  // カメラの初期ズーム

    public Vector2 minBounds = new Vector2(-10f, -10f);  // 最小移動範囲
    public Vector2 maxBounds = new Vector2(10f, 10f);  // 最大移動範囲

    void Start()
    {
        initialPosition = mainCamera.transform.position;  // 初期位置を保存
        initialZoom = mainCamera.orthographicSize;  // 初期ズームを保存
    }

    void Update()
    {
        HandleZoom();  // ズーム処理
        HandleDrag();  // ドラッグ処理
        HandleReset();  // リセット処理
    }

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

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))  // ドラッグ開始時にワールド座標を記録
        {
            dragOrigin = GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0))  // ドラッグ中
        {
            Vector3 difference = dragOrigin - GetMouseWorldPosition();  // 差分を計算
            Vector3 newPosition = mainCamera.transform.position + difference;

            // 移動範囲を制限してから位置を適用
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

    // カメラの位置を指定された範囲内に制限する
    Vector3 ClampPosition(Vector3 targetPosition)
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float clampedX = Mathf.Clamp(targetPosition.x,
                                     minBounds.x + cameraWidth, maxBounds.x - cameraWidth);
        float clampedY = Mathf.Clamp(targetPosition.y,
                                     minBounds.y + cameraHeight, maxBounds.y - cameraHeight);

        return new Vector3(clampedX, clampedY, targetPosition.z);
    }
}
