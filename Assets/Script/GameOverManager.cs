using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // タイトルシーンに戻る
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title Scene"); // TitleSceneの名前が違う場合は、正しい名前に変更してください
    }

    // ゲームメインシーンに戻る（再スタート）
    public void RetryGame()
    {
        SceneManager.LoadScene("Stage1"); // GameMainSceneの名前が違う場合は、正しい名前に変更してください
    }
}
