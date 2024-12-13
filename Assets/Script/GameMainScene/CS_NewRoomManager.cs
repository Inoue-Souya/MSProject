using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_NewRoomManager : MonoBehaviour
{
    public CS_Room[] rooms;               // シーン内の部屋の配列
    public int inResident;                // ゲームオーバー用変数
    public int openRoom;                  // 解放済みの部屋数
    public PulsatingVignette vignette;    // ビネット制御スクリプト
    public AudioSource audioSource;       // SE再生用のAudioSource
    public AudioClip warningSE;           // 警告音（ループ用）

    private bool isVignetteActive = false; // ビネットの状態を追跡

    void Start()
    {
        //InitializeRooms();
        inResident = 0;
        openRoom = 5;

        if (vignette != null)
        {
            vignette.enabled = false; // ビネットを最初は無効化
        }

        if (audioSource != null)
        {
            audioSource.loop = true; // ループ再生を有効化
            audioSource.clip = warningSE; // 再生する音を設定
        }
    }

    private void InitializeRooms()
    {
        // 解放済みの部屋を設定する
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i < 5) // 最初の5部屋を解放済みにする
            {
                rooms[i].InitializeRoom(true);
            }
            else
            {
                rooms[i].InitializeRoom(false);
            }
        }
    }

    private void Update()
    {
        gameObject.SetActive(true);

        // ゲームオーバー直前の条件
        if (openRoom - inResident <= 3 && inResident > 5)
        {
            ActivateVignetteAndSE();
        }
        else
        {
            DeactivateVignetteAndSE();
        }

        // ゲームオーバー条件
        if (inResident >= openRoom && openRoom >= 6)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void ActivateVignetteAndSE()
    {
        if (!isVignetteActive && vignette != null)
        {
            vignette.enabled = true; // ビネットを有効化
            isVignetteActive = true;

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play(); // SEを再生
            }
        }
    }

    private void DeactivateVignetteAndSE()
    {
        if (isVignetteActive && vignette != null)
        {
            vignette.enabled = false; // ビネットを無効化
            isVignetteActive = false;

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop(); // SEを停止
            }
        }
    }
}
