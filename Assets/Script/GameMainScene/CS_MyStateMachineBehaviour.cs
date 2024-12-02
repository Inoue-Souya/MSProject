using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MyStateMachineBehaviour : StateMachineBehaviour
{
    public string audioSourceObjectName = "";
    public AudioClip soundEffect; // 効果音のAudioClip(ボーナスあり)
    private AudioSource audioSource; // AudioSourceコンポーネント

    // アニメーションステートが開始する時
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("アニメーション開始時に呼ばれる関数");

        // AnimatorのGameObjectからAudioSourceObjectを探す
        if (audioSource == null)
        {
            GameObject audioSourceObject = GameObject.Find(audioSourceObjectName);
            if (audioSourceObject != null)
            {
                audioSource = audioSourceObject.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogWarning("AudioSourceオブジェクトが見つかりません: " + audioSourceObjectName);
            }
        }

        // 効果音を再生
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }

        if (stateInfo.IsName("CutIn"))
        {
            animator.SetBool("PlayCutIn", false);  // CutInが始まったらPlayCutInをfalseに設定
        }
    }

    // アニメーションステートが終了する時
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("アニメーション終了時に呼ばれる関数");

        animator.SetBool("PlayCutIn", false);

        // デバッグ用
        Debug.Log("アニメーション終了時にbool変数をfalseに設定");
    }

    // アニメーションステートが遷移する時
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("ステートマシン開始時に呼ばれる関数");
    }
}
