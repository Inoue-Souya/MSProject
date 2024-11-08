using UnityEngine;

public class CS_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // ���C���J����
    public float zoomSpeed = 0.5f;  // �Y�[�����x
    public float minZoom = 2f;  // �ŏ��Y�[��
    private float initialZoom;  // �J�����̏����Y�[���i�Y�[���A�E�g�̏���j

    private Vector3 dragOrigin;  // �h���b�O�J�n�ʒu�i���[���h��ԁj
    private Vector3 initialPosition;  // �J�����̏����ʒu

    private float minX, maxX;  // �����̉��͈� (X���̂�)
    public float minY;  // Y���̉����i�����\�j
    public float maxY;  // Y���̏���i�����\�j

    void Start()
    {
        initialPosition = mainCamera.transform.position;  // �����ʒu��ۑ�
        initialZoom = mainCamera.orthographicSize;  // �����Y�[������Ƃ���

        // �����ʒu�ł̃J������X���̉��͈͂��v�Z
        CalculateBounds();
    }

    void Update()
    {
        HandleZoom();  // �Y�[������
        HandleDrag();  // �h���b�O����
        HandleReset();  // ���Z�b�g����
    }

    // �J�����̏����ʒu�ƃY�[������ɁAX���̕\���͈͂��v�Z
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

            // �Y�[����̈ʒu��X����Y���͈͓̔��ɐ���
            mainCamera.transform.position = ClampPosition(mainCamera.transform.position);
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
            Vector3 difference = dragOrigin - GetMouseWorldPosition();
            Vector3 newPosition = mainCamera.transform.position + new Vector3(difference.x, difference.y, 0);

            // X����Y���̈ړ��͈͂𐧌�
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

    // X����Y���̈ړ��͈͂�ݒ肵�A�����\���͈͓��ɐ���
    Vector3 ClampPosition(Vector3 targetPosition)
    {
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraHeight = mainCamera.orthographicSize;

        // X���̈ړ��͈͂𐧌�
        float clampedX = Mathf.Clamp(targetPosition.x, minX + cameraWidth, maxX - cameraWidth);

        // �Y�[���ɉ�����Y���̉����𒲐�
        float dynamicMinY = initialPosition.y - initialZoom + mainCamera.orthographicSize;
        float clampedY = Mathf.Clamp(targetPosition.y, dynamicMinY, maxY);

        return new Vector3(clampedX, clampedY, targetPosition.z);
    }
}
