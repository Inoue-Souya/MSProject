using System.Collections;
using System.Collections.Generic;
using UnityEngine;




////////////////////////////////////// ////////////////
//�E�@�\�����E
//�}�E�X�z�C�[���ł̃Y�[���C���E�A�E�g
//���N���b�N�h���b�O�ł̃J�����ړ�
//R�L�[�ŃJ�����̈ʒu�ƃY�[����������Ԃɖ߂��@�\
/// ///////////////////////////////////////////////////
public class CS_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // ���C���J����
    public float zoomSpeed = 1f;  // �Y�[�����x
    public float minZoom = 2f;  // �ŏ��Y�[��
    public float maxZoom = 5f;  // �ő�Y�[��

    private Vector3 dragOrigin;  // �h���b�O�J�n�ʒu
    private Vector3 initialPosition;  // �J�����̏����ʒu
    private float initialZoom;  // �J�����̏����Y�[��

    void Start()
    {
        // �����ʒu�ƃY�[����ۑ�
        initialPosition = mainCamera.transform.position;
        initialZoom = mainCamera.orthographicSize;
    }

    void Update()
    {
        HandleZoom();  // �Y�[������
        HandleDrag();  // �h���b�O����
        HandleReset();  // �J�����̃��Z�b�g����
    }

    // �}�E�X�z�C�[���ɂ��Y�[������
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

    // ���N���b�N�ł̃h���b�O�ړ�����
    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))  // ���N���b�N���������Ƃ�
        {
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))  // ���N���b�N���z�[���h���Ă����
        {
            Vector3 difference = dragOrigin - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mainCamera.transform.position += difference;
        }
    }

    // R�L�[�ŃJ�����������ʒu�ɖ߂�����
    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))  // R�L�[�������ꂽ��
        {
            mainCamera.transform.position = initialPosition;
            mainCamera.orthographicSize = initialZoom;
        }
    }
}
