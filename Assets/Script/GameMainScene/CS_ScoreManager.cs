using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreManager : MonoBehaviour
{
    public Sprite[] numberSprites; // 数字のスプライトを入れる配列
    public int currentScore;
    public Image[] scoreImages; // スコアを表示するImageの配列
    public GameObject audioSourceObject;  // SEを再生するためのAudioSourceがアタッチされたゲームオブジェクト
    public AudioClip soundEffect1;  // 効果音のAudioClip(ボーナスなし)
    public AudioClip soundEffect2;  // 効果音のAudioClip(ボーナスあり)
    private AudioSource audioSource;  // AudioSourceコンポーネント

    public void Init()
    {
        // 初期スコア設定
        UpdateScoreDisplay();

        // 指定されたゲームオブジェクトからAudioSourceコンポーネントを取得
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

    }
    public void AddScore(int score, bool soundflag)
    {
        currentScore += score;
        UpdateScoreDisplay();

        // サウンドを流す
        if(soundflag)
        {// ボーナスがあるときのサウンド
            if (audioSource != null && soundEffect2 != null)
            {
                audioSource.PlayOneShot(soundEffect2);
            }
        }
        else
        {// ボーナスがないときのサウンド
            if (audioSource != null && soundEffect1 != null)
            {
                audioSource.PlayOneShot(soundEffect1);
            }
        }
    }

    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;
            UpdateScoreDisplay();
            return true;
        }
        return false; // スコアが足りない場合
    }

    public void UpdateScoreDisplay()
    {
        // 現在のスコアを数字の画像に変換する
        string scoreString = currentScore.ToString();
        for (int i = 0; i < scoreImages.Length; i++)
        {
            if (i < scoreString.Length)
            {
                // 数字に応じたスプライトを表示
                int number = int.Parse(scoreString[i].ToString());
                scoreImages[i].sprite = numberSprites[number];
                scoreImages[i].gameObject.SetActive(true);
            }
            else
            {
                scoreImages[i].gameObject.SetActive(false); // スコアの桁が少ない場合は非表示
            }
        }
    }
}
