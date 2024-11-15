using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < yo_kaies.Count; i++)
        {
            yo_kaies[i].GetComponent<CS_DragandDrop>();
        }

        MoveYoKaies();
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

        // �d�����X�g�ȊO�̃I�u�W�F�N�g�𒊑I�Ώۂɂ���
        List<CS_DragandDrop> otherObjects = new List<CS_DragandDrop>();

        // YoKai���X�g���̃I�u�W�F�N�g�����������X�g���쐬
        foreach (var obj in yo_kaies)
        {
            if (!movedObjects.Exists(movedObj => movedObj.gameObject == obj))
            {
                otherObjects.Add(obj);  // �ړ����Ă��Ȃ��I�u�W�F�N�g��ǉ�
            }
        }

        // ���̑��̃I�u�W�F�N�g���Ȃ��ꍇ��specifiedObjec(�u�����d��)�����̈ʒu�ɖ߂�
        if (otherObjects.Count == 0)
        {
            specifiedObject.gameObject.transform.position = specifiedObject.initialPosition;
            //Debug.LogWarning("�����\�ȃI�u�W�F�N�g���s�����Ă��܂��B");
            return;
        }

        // ���̃I�u�W�F�N�g���烉���_����1�I��
        CS_DragandDrop randomOtherObject = otherObjects[Random.Range(0, otherObjects.Count)];

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

        Debug.Log("�������܂����F" + randomOtherObject.name + " �� " + specifiedObject.gameObject.name);
    }
}
