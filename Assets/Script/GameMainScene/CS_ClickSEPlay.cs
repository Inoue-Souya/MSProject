using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ClickSEPlay : MonoBehaviour
{
    public AudioClip soundEffect;  // 効果音のAudioClip
    public GameObject audioSourceObject;  // SEを再生するためのAudioSourceがアタッチされたゲームオブジェクト

    private AudioSource audioSource;  // AudioSourceコンポーネント

    void Start()
    {
        // 指定されたゲームオブジェクトからAudioSourceコンポーネントを取得
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

        // AudioSourceが正しく取得できていない場合は警告を出す
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component is not attached to the specified object.");
        }

        // AudioSourceのPlay On Awakeを無効にする
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    // クリックイベントを処理
    void OnMouseDown()
    {
        // 音を再生
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
        else
        {
            Debug.LogWarning("AudioSource or soundEffect is missing!");
        }
    }
}