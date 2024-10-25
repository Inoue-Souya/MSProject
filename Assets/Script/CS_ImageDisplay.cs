using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ImageDisplay : MonoBehaviour
{
    public GameObject[] targetObjects;  // �����̃Q�[���I�u�W�F�N�g���w��
    public float proximityDistance = 50f;
    private List<UnityEngine.UI.Image> targetImages = new List<UnityEngine.UI.Image>(); // Image �R���|�[�l���g��ێ�
    private List<RectTransform> targetRectTransforms = new List<RectTransform>();

    void Start()
    {
        // �e�^�[�Q�b�g�I�u�W�F�N�g���� Image �R���|�[�l���g���擾
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                UnityEngine.UI.Image img = targetObject.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                {
                    targetImages.Add(img);
                    targetRectTransforms.Add(img.GetComponent<RectTransform>());
                    img.gameObject.SetActive(false); // ������ԂŔ�\���ɐݒ�
                }
                else
                {
                    UnityEngine.Debug.LogError($"�^�[�Q�b�g�I�u�W�F�N�g '{targetObject.name}' �� Image �R���|�[�l���g��������܂���ł����B");
                }
            }
            else
            {
                UnityEngine.Debug.LogError("�^�[�Q�b�g�I�u�W�F�N�g�� null �ł��B");
            }
        }
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        for (int i = 0; i < targetImages.Count; i++)
        {
            if (targetImages[i] == null) continue;

            Vector2 targetPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targetRectTransforms[i].position);
            float distance = Vector2.Distance(mousePosition, targetPosition);

            UnityEngine.Debug.Log($"Mouse Position: {mousePosition}, Target Position: {targetPosition}, Distance: {distance}");

            if (distance < proximityDistance)
            {
                targetImages[i].gameObject.SetActive(true);
                UnityEngine.Debug.Log($"�^�[�Q�b�g�摜 '{targetImages[i].name}' ���\������܂����B");
            }
            else
            {
                targetImages[i].gameObject.SetActive(false);
                UnityEngine.Debug.Log($"�^�[�Q�b�g�摜 '{targetImages[i].name}' ����\���ɂȂ�܂����B");
            }
        }
    }
}
