using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YoKai
{
    public CS_DragandDrop gameObject;  // �d���I�u�W�F�N�g
    public Vector3 initialPosition;  // �����ʒu

    public YoKai(CS_DragandDrop obj, Vector3 position)
    {
        gameObject = obj;
        initialPosition = position;
    }
}
public class CS_Yo_kaiChange : MonoBehaviour
{
    [Header("�d���̃��X�g")]
    public List<CS_DragandDrop> yo_kaies;  // �ړ��Ώۂ̃��X�g

    [Header("�ړ��Ԋu")]
    public float spacing = 2.0f;  // �I�u�W�F�N�g�Ԃ̃X�y�[�X

    [Header("�ړ��J�n�ʒu")]
    public Vector3 startPosition = Vector3.zero;  // �ړ��J�n�ʒu

    private List<YoKai> movedObjects = new List<YoKai>();  // �ړ��ς݃I�u�W�F�N�g�̃��X�g
    private List<CS_DragandDrop> otherObjects;// �d�����X�g�ȊO�̃I�u�W�F�N�g
    private CS_DragandDrop randomOtherObject;

    [Header("�������̃G�t�F�N�g")]
    public CS_Effect effectController;//�p�[�e�B�N���p

    [Header("���̗d�����o���X�v���C�g")]
    public Image NextYo_kaiImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < yo_kaies.Count; i++)
        {
            yo_kaies[i].GetComponent<CS_DragandDrop>();
        }

        // �ŏ��̗d�����Z�b�g
        MoveYoKaies();

        // ���̗d����p��
        NextYo_kai();
    }

    void MoveYoKaies()
    {
        // ���X�g�̗v�f���ő�5�܂ňړ�
        int count = Mathf.Min(yo_kaies.Count, 5);

        for (int i = 0; i < count; i++)
        {
            if (yo_kaies[i] != null)  // �I�u�W�F�N�g�����݂��邩�m�F
            {
                // �V�����ʒu���v�Z
                Vector3 newPosition = startPosition + new Vector3(i * spacing, 0, 0);

                // �I�u�W�F�N�g��V�����ʒu�Ɉړ�
                yo_kaies[i].GetComponent<CS_DragandDrop>();
                yo_kaies[i].transform.position = newPosition;
                yo_kaies[i].SetPosition(yo_kaies[i].transform.position);
                

                // �ړ������I�u�W�F�N�g�����X�g�ɒǉ�
                movedObjects.Add(new YoKai(yo_kaies[i], newPosition));
            }
        }
    }

    public void SwapRandomObject(string objectName)
    {
        // ���͂��ꂽ���O�����I�u�W�F�N�g�̎Q�Ƃ��擾
        YoKai specifiedObject = movedObjects.Find(obj => obj.gameObject.name == objectName);

        // ���͂��ꂽ���O�����I�u�W�F�N�g�̃C���f�b�N�X���擾
        int specifiedIndex = movedObjects.FindIndex(obj => obj.gameObject.name == objectName);

        // �w�肳�ꂽ�I�u�W�F�N�g��������Ȃ��ꍇ�͏I��
        if (specifiedObject == null)
        {
            Debug.LogWarning("�w�肳�ꂽ���O�̃I�u�W�F�N�g��������܂���B");
            return;
        }

        // ���̑��̃I�u�W�F�N�g���Ȃ��ꍇ��specifiedObjec(�u�����d��)�����̈ʒu�ɖ߂�
        if (otherObjects.Count == 0)
        {
            specifiedObject.gameObject.transform.position = specifiedObject.initialPosition;
            //Debug.LogWarning("�����\�ȃI�u�W�F�N�g���s�����Ă��܂��B");
            return;
        }

        // �ʒu������
        Vector3 tempPosition = randomOtherObject.transform.position;  // randomOtherObject �̌��̈ʒu���ꎞ�ۑ�
        randomOtherObject.transform.position = specifiedObject.initialPosition;  // randomOtherObject �� specifiedObject �̌��̈ʒu�Ɉړ�
        specifiedObject.gameObject.transform.position = tempPosition;  // specifiedObject �� randomOtherObject �̌��̈ʒu�Ɉړ�

        // �ʒu���X�V
        randomOtherObject.SetPosition(randomOtherObject.transform.position);
        specifiedObject.initialPosition = specifiedObject.gameObject.transform.position;  // �w��I�u�W�F�N�g�̐V�����ʒu�� initialPosition �ɐݒ�

        // randomOtherObject �̐V�����ʒu�������� YoKai �C���X�^���X���쐬
        YoKai randomOtherYoKai = new YoKai(randomOtherObject, randomOtherObject.transform.position);

        // movedObjects���X�g���X�V
        movedObjects.Remove(specifiedObject);       // specifiedObject�����X�g����폜
        movedObjects.Add(randomOtherYoKai);         // �V���ɑI�����ꂽ�I�u�W�F�N�g��ǉ�

        // �V�����d���̈ʒu�̃G�t�F�N�g���o��
        PlaceSmallImage(randomOtherObject.transform.position);

        // ���̗d�����Z�b�g
        NextYo_kai();

        Debug.Log("�������܂����F" + randomOtherObject.name + " �� " + specifiedObject.gameObject.name);
    }

    private void NextYo_kai()
    {
        // �d���̏������Z�b�g
        otherObjects = new List<CS_DragandDrop>();
        randomOtherObject = null;

        // YoKai���X�g���̃I�u�W�F�N�g�����������X�g���쐬
        foreach (var obj in yo_kaies)
        {
            if (!movedObjects.Exists(movedObj => movedObj.gameObject == obj))
            {
                otherObjects.Add(obj);  // �ړ����Ă��Ȃ��I�u�W�F�N�g��ǉ�
            }
        }

        if (otherObjects.Count == 0)
        {

        }
        else
        {
            // ���̃I�u�W�F�N�g���烉���_����1�I��
            randomOtherObject = otherObjects[Random.Range(0, otherObjects.Count)];
            Debug.Log("�Z�b�g���܂����F" + randomOtherObject.name);

            SpriteRenderer spriteRenderer = randomOtherObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Sprite sprite = spriteRenderer.sprite;
                Debug.Log("�擾�����X�v���C�g: " + sprite.name);

                // �K�v�ɉ����ăX�v���C�g���g�p
                // ��: UI�摜�ɐݒ肷��
                if (NextYo_kaiImage != null)
                {
                    NextYo_kaiImage.sprite = sprite;
                }
            }
            else
            {
                Debug.LogWarning("�w�肳�ꂽ�I�u�W�F�N�g��SpriteRenderer������܂���: " + randomOtherObject.name);
            }
        }
    }

    private void PlaceSmallImage(Vector3 position)
    {
        Vector3 offsetPos = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.transform.position = position + offsetPos;

        // �G�t�F�N�g�Đ�
        if (effectController != null)
        {
            effectController.PlayPlacementEffect(gameObject.transform.position);
        }
        else
        {
            Debug.LogWarning("Effect controller is not assigned in CS_DragandDrop.");
        }
    }

    public void AddYo_kai(CS_DragandDrop Yo_kai)
    {
        if (yo_kaies.Exists(obj => obj.name == Yo_kai.gameObject.name))
        {
            Debug.Log("���łɒǉ�����Ă��܂��B");
            return;
        }

        // �d�������X�g�ɒǉ�
        yo_kaies.Add(Yo_kai);

        // randomOtherObject��null�̏ꍇ��NextYo_kai()���Ăяo��
        if (randomOtherObject == null || string.IsNullOrEmpty(randomOtherObject.name))
        {
            NextYo_kai();
        }
    }
}
