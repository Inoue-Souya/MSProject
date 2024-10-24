using UnityEngine;

public class CS_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // ���C���J����
    public float zoomSpeed = 0.5f;  // �Y�[�����x
    public float minZoom = 2f;  // �ŏ��Y�[��
    public float maxZoom = 10f;  // �ő�Y�[��

    private Vector3 dragOrigin;  // �h���b�O�J�n�ʒu�i���[���h��ԁj
    private Vector3 initialPosition;  // �J�����̏����ʒu
    private float initialZoom;  // �J�����̏����Y�[��

    public Vector2 minBounds = new Vector2(-10f, -10f);  // �ŏ��ړ��͈�
    public Vector2 maxBounds = new Vector2(10f, 10f);  // �ő�ړ��͈�

    void Start()
    {
        initialPosition = mainCamera.transform.position;  // �����ʒu��ۑ�
        initialZoom = mainCamera.orthographicSize;  // �����Y�[����ۑ�
    }

    void Update()
    {
        HandleZoom();  // �Y�[������
        HandleDrag();  // �h���b�O����
        HandleReset();  // ���Z�b�g����
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
        if (Input.GetMouseButtonDown(0))  // �h���b�O�J�n���Ƀ��[���h���W���L�^
        {
            dragOrigin = GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0))  // �h���b�O��
        {
            Vector3 difference = dragOrigin - GetMouseWorldPosition();  // �������v�Z
            Vector3 newPosition = mainCamera.transform.position + difference;

            // �ړ��͈͂𐧌����Ă���ʒu��K�p
            mainCamera.transform.position = ClampPosition(newPosition);
        }
    }

    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))  // R�L�[�Ń��Z�b�g
        {
            mainCamera.transform.position = initialPosition;
            mainCamera.orthographicSize = initialZoom;
        }
    }

    // �}�E�X�̃��[���h���W���擾����w���p�[���\�b�h
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;  // �J������Z�����l��
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    // �J�����̈ʒu���w�肳�ꂽ�͈͓��ɐ�������
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
