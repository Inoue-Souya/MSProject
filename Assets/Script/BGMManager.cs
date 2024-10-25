using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // AudioSourceコンポーネント
    private AudioSource audioSource;

    // BGMとして再生するAudioClipを指定
    public AudioClip bgmClip;

    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();

        // AudioClipをセット
        audioSource.clip = bgmClip;

        // ループ再生を有効にする
        audioSource.loop = true;

        // BGMを再生
        if (audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log("BGM is playing.");
        }
        else
        {
            Debug.Log("AudioClip is not set.");
        }
    }

}