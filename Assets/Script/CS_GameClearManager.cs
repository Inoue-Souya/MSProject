using UnityEngine;
using System.Collections;  // IEnumeratorを使うために追加
using System.Diagnostics;

public class CS_GameClearManager : MonoBehaviour
{
    public CS_FadeIn fadeInScript;  // CS_FadeInスクリプト
    public CS_GameClear gameClearScript;  // CS_GameClearスクリプト

    private void Start()
    {
        if (fadeInScript == null || gameClearScript == null)
        {
           
            return;
        }

        StartCoroutine(WaitForFadeIn());
    }

    private IEnumerator WaitForFadeIn()
    {
        // フェードインの完了を待つ
        while (!fadeInScript.fadeFinish)
        {
            yield return null;
        }

        // フェードイン完了後にCS_GameClearの処理を開始
        gameClearScript.enabled = true;
    }
}
