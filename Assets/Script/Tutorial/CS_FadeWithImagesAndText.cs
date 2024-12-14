using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.IO;
using UnityEngine.SceneManagement; // 追加

public class CS_FadeWithImagesAndText : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // フェード用のImage
    public float fadeDuration = 1.0f; // フェードにかかる時間
    private Vector3 initialPosition;  // カメラの初期位置

    public GameObject[] images; // 表示する画像の配列
    public UnityEngine.UI.Text[] texts; // 表示するテキストの配列
    public CS_DragandDrop drop;
    public CS_DragandDrop drop1;
    public CS_CameraZoom camera;
    public CS_AddYo_kai yokai;
    private Vector3 targetPosition;  // カメラが移動するターゲット位置

    private float currentAlpha = 0f; // 現在のアルファ値
    private bool isFading = false;
    private bool isShowingContent = false;
    private int i = 0;
    private int j = 0;
    private int clickcount = 0;
    private bool clickflag=false;
    private bool flag = false;
    private bool endflag = false;

    [SerializeField] private string targetSceneName;   // 遷移先のシーン名

    private void Start()
    {
        initialPosition = camera.transform.position;
        targetPosition= initialPosition;
        // フェード画像が設定されていなければエラーを表示
        if (fadeImage == null)
        {
            enabled = false;
            return;
        }

        // 初期状態でアルファ値を0にする
        SetAlpha(0f);

        // 表示する画像とテキストを非表示にしておく
        foreach (GameObject image in images)
        {
            if (image != null) image.SetActive(false);
        }

        foreach (UnityEngine.UI.Text text in texts)
        {
            if (text != null) text.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        
        switch(i)
        {
            case 0: if(camera.startPhase)
                {
                    clickflag = true;
                    StartFadeOut();
                    i++;
                }
                break;
            case 1:
                if (drop.GetisDragging())
                {
                    i++;
                }
                break;
            case 2:
                if (drop.GetinRoom())
                {
                    i++;
                }
                break;
            case 3:
                if (!drop.GetinRoom())
                {
                    i++;
                    StartFadeOut();
                }
                break;
            case 4:
                if(yokai.cutinflag)
                {
                    i++;
                    clickflag = true;
                    StartFadeOut();
                }
                break;
            case 5:
                if (drop1.GetinRoom())
                {
                    endflag = false;
                    clickflag = true;
                    StartFadeOut();
                    i++;
                }
                break;
            case 6:
                if(endflag)
                {
                    endflag = false;
                    StartFadeOut();
                    i++;
                }
                break;
            case 7:
                if (endflag)
                {
                    SceneManager.LoadScene(targetSceneName);
                }
                break;
        }


        // クリックでフェードイン開始
        if (Input.GetMouseButtonDown(0) && isShowingContent && isFading)
        {
            HideImagesAndText();
            if (clickflag)
            {
                clickcount++;
                // テキストを表示
                foreach (UnityEngine.UI.Text text in texts)
                {
                    UnityEngine.Debug.Log("" + j);
                    if (texts[j] != null) texts[j].gameObject.SetActive(true);
                    clickflag = false;
                    return;
                }
                
            }
            if (clickcount == 1)
            {
                clickflag = false;
                clickcount = 0;
                endflag = true;
            }
            isFading = false;
            endflag = true;
        }


    }

    public void StartFadeOut()
    {
        if (!isFading)
        {
           
             StartCoroutine(FadeOut());
           
        }
    }

   

    private IEnumerator FadeOut()
    {
        isFading = true;
        float elapsed = 0f;
        fadeImage.gameObject.SetActive(true);
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            currentAlpha = Mathf.Clamp01(elapsed / fadeDuration) * 0.5f;
            SetAlpha(currentAlpha);
            yield return null;
        }

        SetAlpha(0.5f); // フェードアウト完了
        ShowImagesAndText();

        
        isShowingContent = true;
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }

    private void ShowImagesAndText()
    {
        
        // 画像を表示
        foreach (GameObject image in images)
        {
            if (image != null) image.SetActive(true);
        }

        // テキストを表示
        foreach (UnityEngine.UI.Text text in texts)
        {
            UnityEngine.Debug.Log(""+j);
            if (texts[j] != null) texts[j].gameObject.SetActive(true);

        }
    }

    private void HideImagesAndText()
    {
        // 画像を非表示
        foreach (GameObject image in images)
        {
            if (image != null)
            {
                if (clickflag) 
                {
                    if (clickcount == 1)
                    {
                        image.SetActive(false);
                    }
                   
                }
                else
                {
                    image.SetActive(false);
                }
                
            }
        }

        // テキストを非表示
        foreach (UnityEngine.UI.Text text in texts)
        {
            if(texts[j] != null) texts[j].gameObject.SetActive(false);

        }
        if (clickflag)
        {
            if (clickcount == 1)
            {
                fadeImage.gameObject.SetActive(false);
                SetAlpha(0f); // フェードアウト完了
            }

        }
        else
        {
            fadeImage.gameObject.SetActive(false);
            SetAlpha(0f); // フェードアウト完了
        }

        j++;
    }

    void FixCameraPosition()
    {
        // カメラ位置を固定
        camera.transform.position = targetPosition;
    }

}
