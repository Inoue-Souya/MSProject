using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ScoreDisplay : MonoBehaviour
{
    public Text displayTextPrefab; // �e�L�X�g�̃v���n�u
    public Transform canvasTransform; // �e�L�X�g��z�u����Canvas��Transform
    public Vector3 offset = new Vector3(0, 50, 0); // �\���ʒu�̃I�t�Z�b�g�i�C���X�y�N�^�[�Őݒ�\�j
    public int fontSize = 24; // �C���X�y�N�^�[�Őݒ�\�ȃt�H���g�T�C�Y
    public float fadeDuration = 2f; // �t�F�[�h�A�E�g�̎������ԁi�b�j
    public float riseDistance = 50f; // �e�L�X�g���㏸���鋗��

    [Range(0, 1)] public float textSaturation = 1f; // �ʓx�i0 = ���ʐF, 1 = �N�₩�j
    [Range(0, 1)] public float textBrightness = 1f; // ���邳�i0 = �Â�, 1 = ���邢�j

    private List<GameObject> activeTexts = new List<GameObject>(); // �\�����̃e�L�X�g���Ǘ�

    public void ShowText(int addedPoints,Vector3 position)
    {
        GameObject newTextObject = Instantiate(displayTextPrefab.gameObject, canvasTransform);
        Text newText = newTextObject.GetComponent<Text>();

        if (newText == null)
        {
            Debug.LogError("displayTextPrefab���������ݒ肳��Ă��܂���B");
            return;
        }

        // �t�H���g�T�C�Y��ύX
        newText.fontSize = fontSize;

        // �e�L�X�g�̓��e��ݒ�
        newText.text = $"+{addedPoints}";

        Color goldColor = new Color(1.0f, 0.84f, 0.0f); // ���F��RGB�l

        // �X�R�A�ɉ����ĐF��ύX
        if (addedPoints >= 1000)
        {
            newText.color = Color.yellow; // �傫���X�R�A�͐�
        }
        else if (addedPoints >= 100)
        {
            newText.color = Color.blue; // �����炢�̃X�R�A�͉��F
        }
        else
        {
            newText.color = Color.white; // �������X�R�A�͗�
        }

        // HSV���g�p���čʓx�ƌ��x��ύX
        Color baseColor = Color.yellow; // ��{�F�i�C�ӂŕύX�\�j
        float h, s, v; // �F���A�ʓx�A���x���i�[
        Color.RGBToHSV(baseColor, out h, out s, out v); // RGB��HSV�ɕϊ�

        // �ʓx�ƌ��x�𒲐�
        s = textSaturation; // �ʓx���C���X�y�N�^�[�̒l�ɕύX
        v = textBrightness; // ���x���C���X�y�N�^�[�̒l�ɕύX

        // HSV��RGB�ɖ߂��ăe�L�X�g�̐F��ݒ�
        newText.color = Color.HSVToRGB(h, s, v);

        // �\���ʒu���I�t�Z�b�g�Ɋ�Â��Đݒ�
        RectTransform rectTransform = newText.GetComponent<RectTransform>();
        //rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0) + offset;
        rectTransform.position = position + offset;

        // ���X�g�ɒǉ�
        activeTexts.Add(newTextObject);

        // �t�F�[�h�A�E�g�������J�n
        StartCoroutine(FadeOutAndDestroy(newTextObject, newText));
    }


    private IEnumerator<System.Collections.IEnumerator> FadeOutAndDestroy(GameObject textObject, Text text)
    {
        float elapsedTime = 0f;
        Color initialColor = text.color;

        RectTransform rectTransform = text.GetComponent<RectTransform>();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // �e�L�X�g�̓����x������������
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // �e�L�X�g�����X�ɏ㏸������
            rectTransform.position += new Vector3(0, riseDistance * (Time.deltaTime / fadeDuration), 0);

            yield return null;
        }

        // �t�F�[�h�A�E�g�I����A���X�g����폜���ăI�u�W�F�N�g��j��
        activeTexts.Remove(textObject);
        Destroy(textObject);
    }
}
