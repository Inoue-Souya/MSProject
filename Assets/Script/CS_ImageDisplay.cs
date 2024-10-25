using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ImageDisplay : MonoBehaviour
{
    public GameObject[] targetObjects;  // 複数のゲームオブジェクトを指定
    public float proximityDistance = 50f;
    private List<UnityEngine.UI.Image> targetImages = new List<UnityEngine.UI.Image>(); // Image コンポーネントを保持
    private List<RectTransform> targetRectTransforms = new List<RectTransform>();

    void Start()
    {
        // 各ターゲットオブジェクトから Image コンポーネントを取得
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                UnityEngine.UI.Image img = targetObject.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                {
                    targetImages.Add(img);
                    targetRectTransforms.Add(img.GetComponent<RectTransform>());
                    img.gameObject.SetActive(false); // 初期状態で非表示に設定
                }
                else
                {
                    UnityEngine.Debug.LogError($"ターゲットオブジェクト '{targetObject.name}' に Image コンポーネントが見つかりませんでした。");
                }
            }
            else
            {
                UnityEngine.Debug.LogError("ターゲットオブジェクトが null です。");
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
                UnityEngine.Debug.Log($"ターゲット画像 '{targetImages[i].name}' が表示されました。");
            }
            else
            {
                targetImages[i].gameObject.SetActive(false);
                UnityEngine.Debug.Log($"ターゲット画像 '{targetImages[i].name}' が非表示になりました。");
            }
        }
    }
}
