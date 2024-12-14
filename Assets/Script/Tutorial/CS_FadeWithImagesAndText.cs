using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.IO;
using UnityEngine.SceneManagement; // �ǉ�

public class CS_FadeWithImagesAndText : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // �t�F�[�h�p��Image
    public float fadeDuration = 1.0f; // �t�F�[�h�ɂ����鎞��
    private Vector3 initialPosition;  // �J�����̏����ʒu

    public GameObject[] images; // �\������摜�̔z��
    public UnityEngine.UI.Text[] texts; // �\������e�L�X�g�̔z��
    public CS_DragandDrop drop;
    public CS_DragandDrop drop1;
    public CS_CameraZoom camera;
    public CS_AddYo_kai yokai;
    private Vector3 targetPosition;  // �J�������ړ�����^�[�Q�b�g�ʒu

    private float currentAlpha = 0f; // ���݂̃A���t�@�l
    private bool isFading = false;
    private bool isShowingContent = false;
    private int i = 0;
    private int j = 0;
    private int clickcount = 0;
    private bool clickflag=false;
    private bool flag = false;
    private bool endflag = false;

    [SerializeField] private string targetSceneName;   // �J�ڐ�̃V�[����

    private void Start()
    {
        initialPosition = camera.transform.position;
        targetPosition= initialPosition;
        // �t�F�[�h�摜���ݒ肳��Ă��Ȃ���΃G���[��\��
        if (fadeImage == null)
        {
            enabled = false;
            return;
        }

        // ������ԂŃA���t�@�l��0�ɂ���
        SetAlpha(0f);

        // �\������摜�ƃe�L�X�g���\���ɂ��Ă���
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


        // �N���b�N�Ńt�F�[�h�C���J�n
        if (Input.GetMouseButtonDown(0) && isShowingContent && isFading)
        {
            HideImagesAndText();
            if (clickflag)
            {
                clickcount++;
                // �e�L�X�g��\��
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

        SetAlpha(0.5f); // �t�F�[�h�A�E�g����
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
        
        // �摜��\��
        foreach (GameObject image in images)
        {
            if (image != null) image.SetActive(true);
        }

        // �e�L�X�g��\��
        foreach (UnityEngine.UI.Text text in texts)
        {
            UnityEngine.Debug.Log(""+j);
            if (texts[j] != null) texts[j].gameObject.SetActive(true);

        }
    }

    private void HideImagesAndText()
    {
        // �摜���\��
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

        // �e�L�X�g���\��
        foreach (UnityEngine.UI.Text text in texts)
        {
            if(texts[j] != null) texts[j].gameObject.SetActive(false);

        }
        if (clickflag)
        {
            if (clickcount == 1)
            {
                fadeImage.gameObject.SetActive(false);
                SetAlpha(0f); // �t�F�[�h�A�E�g����
            }

        }
        else
        {
            fadeImage.gameObject.SetActive(false);
            SetAlpha(0f); // �t�F�[�h�A�E�g����
        }

        j++;
    }

    void FixCameraPosition()
    {
        // �J�����ʒu���Œ�
        camera.transform.position = targetPosition;
    }

}
