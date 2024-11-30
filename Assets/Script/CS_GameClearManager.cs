using UnityEngine;
using System.Collections;  // IEnumerator���g�����߂ɒǉ�
using System.Diagnostics;

public class CS_GameClearManager : MonoBehaviour
{
    public CS_FadeIn fadeInScript;  // CS_FadeIn�X�N���v�g
    public CS_GameClear gameClearScript;  // CS_GameClear�X�N���v�g

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
        // �t�F�[�h�C���̊�����҂�
        while (!fadeInScript.fadeFinish)
        {
            yield return null;
        }

        // �t�F�[�h�C���������CS_GameClear�̏������J�n
        gameClearScript.enabled = true;
    }
}
