using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YoKai
{
    public CS_DragandDrop gameObject;   // �d���I�u�W�F�N�g
    //public Vector3 initialPosition;     // �����ʒu
    public GameObject Ikonobject;       // �A�C�R���p�I�u�W�F�N�g

    public YoKai(CS_DragandDrop obj, GameObject gameobj)
    {
        gameObject = obj;
        //initialPosition = position;
        Ikonobject = gameobj;

    }
}
public class CS_Yo_kaiChange : MonoBehaviour
{
    [Header("�d���̃��X�g")]
    public List<CS_DragandDrop> yo_kaies;  // �ړ��Ώۂ̃��X�g

    private int count;
    private int MaxReSource;

    [Header("�d���A�C�R���̃��X�g")]
    public List<GameObject> yo_kaiesIkon;

    [Header("�ړ��Ԋu")]
    public float spacing = 2.0f;  // �I�u�W�F�N�g�Ԃ̃X�y�[�X

    [Header("�ړ��J�n�ʒu")]
    public Vector3 startPosition = Vector3.zero;  // �ړ��J�n�ʒu

    private List<YoKai> movedObjects = new List<YoKai>();  // �ړ��ς݃I�u�W�F�N�g�̃��X�g
    private List<CS_DragandDrop> otherObjects;// �d�����X�g�ȊO�̃I�u�W�F�N�g
    private CS_DragandDrop randomOtherObject;
    

    [Header("���̗d�����o���C���[�WUI")]
    public Image NextYo_kaiImage;

    [Header("�g�p�ςݗd���X�v���C�g")]
    public Sprite usedYo_kaiImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < yo_kaies.Count; i++)
        {
            if(yo_kaies[i] != null)
            {
                yo_kaies[i].GetComponent<CS_DragandDrop>();
            }
        }

        // �����d���̐��̋L�^
        count = yo_kaies.Count;
        // �莝���̍ő吔��ۑ�
        MaxReSource = 5;

        Debug.Log(count);

        // �����X�V 
        Init();
    }

    private void Init()
    {
        // �ŏ��̗d�����Z�b�g
        MoveYoKaies();

        // ���̗d����ݒ�
        NextYo_kai();

        // �ŏ�����randomOtherObject�̃X�v���C�g�𒼐ړ����
        if(randomOtherObject != null)
        {
            SpriteRenderer sprite = randomOtherObject.GetComponent<SpriteRenderer>();
            NextYo_kaiImage.sprite = sprite.sprite;
        }
    }

    private void Update()
    {
        for (int i = 0; i < movedObjects.Count; i++)
        {
            if(!yo_kaies[i].GetinRoom())
            {
                yo_kaies[i].SetPosition(yo_kaiesIkon[i].transform.position);
            }
        }
    }

    private void MoveYoKaies()
    {
        for (int i = 0; i < MaxReSource; i++)
        {
            // �V�����ʒu���v�Z
            Vector3 newPosition = startPosition + new Vector3(i * spacing, 0, 0);

            // �ŏ��̗d���A�C�R���̏����ݒ�
            // �A�C�R���̈ʒu
            yo_kaiesIkon[i].transform.position = newPosition;
            // �A�C�R���摜
            SpriteRenderer IkonSprite = yo_kaiesIkon[i].gameObject.GetComponent<SpriteRenderer>();
            if (i < yo_kaies.Count)
                IkonSprite.sprite = yo_kaies[i].IkonSprite;
            else
                IkonSprite.sprite = usedYo_kaiImage;

            if (i < yo_kaies.Count)  // �I�u�W�F�N�g�����݂��邩�m�F
            {
                // �I�u�W�F�N�g��V�����ʒu�Ɉړ�
                yo_kaies[i].GetComponent<CS_DragandDrop>();
                yo_kaies[i].transform.position = newPosition;
                yo_kaies[i].SetPosition(yo_kaies[i].transform.position);

                yo_kaies[i].transform.SetParent(yo_kaiesIkon[i].transform);

                // �ړ������I�u�W�F�N�g�����X�g�ɒǉ�
                movedObjects.Add(new YoKai(yo_kaies[i], yo_kaiesIkon[i]));
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
            specifiedObject.gameObject.transform.position = yo_kaiesIkon[specifiedIndex].transform.position;

            specifiedObject.gameObject.transform.SetParent(yo_kaiesIkon[specifiedIndex].transform);

            // �A�C�R�������ɖ߂�
            SpriteRenderer sprite = yo_kaiesIkon[specifiedIndex].GetComponent<SpriteRenderer>();
            CS_DragandDrop Ikon = specifiedObject.gameObject.GetComponent<CS_DragandDrop>();
            sprite.sprite = Ikon.IkonSprite;

            return;
        }

       // specifiedObject.gameObject.transform.SetParent(null);

        // �ʒu������
        Vector3 tempPosition = randomOtherObject.transform.position;  // randomOtherObject �̌��̈ʒu���ꎞ�ۑ�
        randomOtherObject.transform.position = yo_kaiesIkon[specifiedIndex].transform.position;  // randomOtherObject �� specifiedObject �̌��̈ʒu�Ɉړ�
        specifiedObject.gameObject.transform.position = tempPosition;  // specifiedObject �� randomOtherObject �̌��̈ʒu�Ɉړ�

        // �ʒu���X�V
        randomOtherObject.SetPosition(randomOtherObject.transform.position);
        //specifiedObject.initialPosition = specifiedObject.gameObject.transform.position;  // �w��I�u�W�F�N�g�̐V�����ʒu�� initialPosition �ɐݒ�

        // randomOtherObject �̐V�����ʒu�������� YoKai �C���X�^���X���쐬
        YoKai randomOtherYoKai = new YoKai(randomOtherObject, specifiedObject.Ikonobject);

        // Yokai�C���X�^���X����X�v���C�g�����擾���A�摜�����ւ���
        SpriteRenderer renderer = randomOtherYoKai.Ikonobject.GetComponent<SpriteRenderer>();
        if(renderer!=null)
        {
            renderer.sprite = randomOtherObject.IkonSprite;
        }
        
        // movedObjects���X�g���X�V
        movedObjects.Remove(specifiedObject);       // specifiedObject�����X�g����폜
        movedObjects.Add(randomOtherYoKai);         // �V���ɑI�����ꂽ�I�u�W�F�N�g��ǉ�

        randomOtherObject.transform.SetParent(yo_kaiesIkon[specifiedIndex].transform);

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
            // ��[����d�����Ȃ�����
        }
        else
        {
            // ���̃I�u�W�F�N�g���烉���_����1�I��
            randomOtherObject = otherObjects[Random.Range(0, otherObjects.Count)];

            Sprite sprite = randomOtherObject.GetSprite();

            // �K�v�ɉ����ăX�v���C�g���g�p
            // ��: UI�摜�ɐݒ肷��
            if (NextYo_kaiImage != null)
            {
                NextYo_kaiImage.sprite = sprite;
            }
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

        // ��D�����܂��Ă��Ȃ���ԂŁA�d�����ǉ����ꂽ�Ƃ��̏���
        {
            if(yo_kaies.Count<=MaxReSource)
            {
                movedObjects.Add(new YoKai(Yo_kai, yo_kaiesIkon[count]));
                
                Yo_kai.gameObject.transform.position = yo_kaiesIkon[count].transform.position;
                Yo_kai.SetPosition(Yo_kai.transform.position);

                Yo_kai.transform.SetParent(yo_kaiesIkon[count].transform);

                SpriteRenderer sprite = yo_kaiesIkon[count].GetComponent<SpriteRenderer>();
                sprite.sprite = Yo_kai.IkonSprite;
            }

            count++;
        }
        // randomOtherObject��null�̏ꍇ��NextYo_kai()���Ăяo��
        if (randomOtherObject == null || string.IsNullOrEmpty(randomOtherObject.name))
        {
            NextYo_kai();
            // ���̗d���o������NextYo_kaiImage�̃A���t�@�l��1�ɐݒ�
            if (NextYo_kaiImage != null)
            {
                Color color = NextYo_kaiImage.color;
                color.a = 1f; // �A���t�@�l��1��
                NextYo_kaiImage.color = color;
            }
            return;
        }
    }

    public void UsedYo_kai(string objectName)
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

        YoKai used = new YoKai(specifiedObject.gameObject, specifiedObject.Ikonobject);

        // Yokai�C���X�^���X����X�v���C�g�����擾���A�摜�����ւ���
        SpriteRenderer renderer = used.Ikonobject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = usedYo_kaiImage;
        }

    }
}