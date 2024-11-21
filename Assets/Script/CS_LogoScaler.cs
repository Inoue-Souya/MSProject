using UnityEngine;

public class CS_LogoScaler : MonoBehaviour
{
    public float duration = 2.0f;       // フェードインと移動にかかる時間
    public float floatDistance = 50.0f; // フロートインの移動距離
    public FadeInButton fadeInButton1;  // 1つ目のボタン
    public FadeInButton fadeInButton2;  // 2つ目のボタン
    public SceneTransitionManager sceneTransitionManager; // フェードアウト管理

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;

    void Start()
    {
        // SpriteRenderer コンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.up * floatDistance; // 目標位置を設定

        // 最初の透明度を0に設定
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0f; // 初期の透明度を 0 に設定
            spriteRenderer.color = color;
        }

        // ボタンを最初は非表示にする
        fadeInButton1.gameObject.SetActive(false);
        fadeInButton2.gameObject.SetActive(false);
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration); // 経過時間に基づく進行度（0～1）

            // フロートインの位置
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            // フェードインのアルファ値
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = progress;  // 透明度を進行度に基づいて設定
                spriteRenderer.color = color;
            }
        }

        // タイトルロゴの出現が終わったらボタンを表示
        if (elapsedTime >= duration)
        {
            if (!fadeInButton1.gameObject.activeSelf)
            {
                fadeInButton1.gameObject.SetActive(true);  // 1つ目のボタンを表示
                fadeInButton1.FadeIn();  // 1つ目のボタンをフェードイン
            }

            if (!fadeInButton2.gameObject.activeSelf)
            {
                fadeInButton2.gameObject.SetActive(true);  // 2つ目のボタンを表示
                fadeInButton2.FadeIn();  // 2つ目のボタンをフェードイン
            }
        }
    }

    // ボタンに設定する関数: シーン名を引数として渡して遷移
    public void LoadScene(string sceneName)
    {
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.FadeToScene(sceneName);
        }
        else
        {
            Debug.LogError("SceneTransitionManager が設定されていません。");
        }
    }
}
