using UnityEngine;
using UnityEngine.UI; // UI 名前空間を使用

public class CS_StorySwitch : MonoBehaviour
{
    public UnityEngine.UI.Image[] images; // 明示的に UnityEngine.UI.Image を指定
    public float slideSpeed = 5f; // スライド速度
    public AudioSource audioSource; // サウンドを再生する AudioSource コンポーネント
    public AudioClip audioclip;
    private int currentIndex = 0; // 現在の画像のインデックス
    private bool isSliding = false; // スライド中かどうかのフラグ

    void Start()
    {
        // AudioSource が設定されていない場合は自動で追加
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        // 左クリックが押されたとき
        if (Input.GetMouseButtonDown(0) && currentIndex < images.Length && !isSliding)
        {
            // サウンドを再生
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play(); // サウンドを再生
            }
            else
            {
                UnityEngine.Debug.LogWarning("AudioSource or AudioClip is missing!");
            }

            // 一番上の画像を左にスライドさせる
            StartSliding();
        }

        // 画像がスライドしている場合の処理
        if (isSliding)
        {
            SlideCurrentImage();
        }
    }

    void StartSliding()
    {
        // スライドする画像が現在のインデックスにあるか確認
        if (currentIndex < images.Length)
        {
            images[currentIndex].transform.localPosition = Vector3.zero; // 位置を初期化
            isSliding = true; // スライドフラグをオン
        }
    }

    void SlideCurrentImage()
    {
        UnityEngine.UI.Image currentImage = images[currentIndex]; // 明示的に UnityEngine.UI.Image を指定

        // 画像が画面の左側を超えるまでスライド
        if (currentImage.transform.localPosition.x > -Screen.width)
        {
            currentImage.transform.localPosition += Vector3.left * slideSpeed * Time.deltaTime;
        }
        else
        {
            // スライドが完了したら次の画像に移動
            currentIndex++;
            isSliding = false; // スライドフラグをオフ
        }
    }
}
