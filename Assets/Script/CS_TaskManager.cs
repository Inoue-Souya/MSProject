using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_TaskManager : MonoBehaviour
{
    private int task;               // 現在のタスク量
    private int Max_task;           // タスク量の最大値
    public CS_FadeOutIn FadeOutIn;   // フェードインアウト関数呼び出し用変数
    public CS_Calendar Calendar;    // カレンダー関数呼び出し用変数
    public Text taskCount;          // タスク量表示テキスト
    public GameObject Panel;        // タスク終了時に表示をオフにするパネル
    // Start is called before the first frame update
    void Start()
    {
        // タスク量初期値
        task = 3;
        Max_task = task;

        // タスク量表示初期設定
        taskCount.text = "タスク量：" + task + " / " + Max_task;
    }

    // Update is called once per frame
    void Update()
    {
        // レベルによってタスク量が増減するならこの処理を細かく変更
        // if(レベルが一定以上)
        // {
        //     Max_task += 1;
        // }

        if (task <= 0)
        {
            Panel.SetActive(false);
            taskCount.gameObject.SetActive(false);

            // フェードインアウト開始～終了
            StartCoroutine(StartFade());

            Calendar.NextDay();

            taskReset();
        }
    }

    public void OnButtonClick()
    {
        task -= 1;
        Debug.Log("タスクあと" + task);
        taskCount.text = "タスク量：" + task + " / " + Max_task;
    }

    private IEnumerator StartFade()
    {
        // フェードインアウト開始
        FadeOutIn.StartFadeOutIn();

        yield return new WaitUntil(() => FadeOutIn.gameObject.activeSelf == false);

        taskCount.text = "タスク量：" + task + " / " + Max_task;
        taskCount.gameObject.SetActive(true);
    }
    private void taskReset()
    {
        task = Max_task;
    }

    public int GetTask()
    {
        return task;
    }
}
