using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Tilemaps;

public class CS_ResidentsManager : MonoBehaviour
{
    public static CS_ResidentsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����ׂ��ł������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // ���ɑ��݂���ꍇ�͔j��
        }
    }

    //public CS_CreateResident[] resident; // �X�N���v�^�u���I�u�W�F�N�g�̔z��
    //public GameObject residentPrefab; // UI�v���n�u

    // �^�C�����W���L�[�A�Z���f�[�^��l�ɂ��鎫��
    private Dictionary<Vector3Int, Resident> residents = new Dictionary<Vector3Int, Resident>();

    // �^�C���}�b�v�̃C���X�^���X���擾
    public Tilemap tilemap; // �^�C���}�b�v�̎Q��
    public TileBase vacantTile; // �������p�^�C���̎Q��
    public TileBase occupiedTile; // �����ς݃^�C��

    void Start()
    {
        //PopulateResidents();

        // �Z���f�[�^
        Resident resident1 = new Resident
        {
            name = "�c��",
            age = 25,
            gender = "�j",
            personality = "�P",
            info = "101�̏Z��",
            portrait = Resources.Load<Sprite>("Images/RImage01") // �摜��ݒ�iResources�t�H���_���ɔz�u�j
        };
        Resident resident2 = new Resident
        {
            name = "����",
            age = 28,
            gender = "�j",
            personality = "��",
            info = "102�̏Z��",
            portrait = Resources.Load<Sprite>("Images/RImage02") // �摜��ݒ�iResources�t�H���_���ɔz�u�j
        };
        Resident resident3 = new Resident
        {
            name = "�R�{",
            age = 25,
            gender = "��",
            personality = "�P",
            info = "103�̏Z��",
            portrait = Resources.Load<Sprite>("Images/RImage03") // �摜��ݒ�iResources�t�H���_���ɔz�u�j
        };


        residents.Add(new Vector3Int(-7, -2, 0), resident1);
        residents.Add(new Vector3Int(-4, -2, 0), resident2);
        residents.Add(new Vector3Int(-1, -2, 0), resident3);

    }

    // �Z����ǉ����郁�\�b�h
    public void AddResident(Vector3Int position, Resident resident)
    {
        if (residents.ContainsKey(position))
        {
            Debug.LogWarning("���̈ʒu�ɂ͊��ɏZ�������܂��B");
            return;
        }

        residents[position] = resident; // �Z����ǉ�
        tilemap.SetTile(position, occupiedTile); // �^�C��������ς݂ɕύX
        Debug.Log($"{resident.name}��{position}�ɒǉ�����܂����B");
    }

    public Resident GetResident(Vector3Int position)
    {
        residents.TryGetValue(position, out Resident resident);
        return resident;
    }

    public void RemoveResident(Vector3Int position)
    {
        if (residents.ContainsKey(position))
        {
            residents.Remove(position);
        }
    }

    // �Z���v�]�Ɋ�Â��]�����v�Z
    public float EvaluateResident(Resident resident, ResidentRequest request)
    {
        float score = 0;

        // ���i�̈�v
        if (resident.personality == request.personality) score += 1;
        // �N��͈̔͂̃`�F�b�N
        if (resident.age >= request.age - 5 && resident.age <= request.age + 5) score += 1;
        // ���ʂ̈�v
        if (resident.gender == request.gender) score += 1;

        //// �X�R�A�Ɋ�Â��Ă����𑝂₷����
        //CS_MoneyManager.Instance.AddMoney(score * 10000); // �X�R�A�ɉ����Ă����𑝉��i��: 1�_���Ƃ�10000�~������j

        return score; // �X�R�A��Ԃ�
    }

    public void ChooseResident(Vector3Int position, ResidentRequest request)
    {
        Resident resident = GetResident(position);
        if (resident != null)
        {
            float score = EvaluateResident(resident, request);
            Debug.Log($"Evaluation Score: {score}");

            // �Z���f�[�^���폜
            RemoveResident(position); // �������������Ă΂�邩�m�F

            // �^�C���𖢓����p�^�C���ɐ؂�ւ��鏈��
            SwitchTileToVacant(position);
        }
        else
        {
            Debug.LogWarning("�Z����������܂���ł����B�폜�ł��܂���B");
        }
    }

    public void AddResidentToTile(Vector3Int tilePos)
    {
        Resident resident = SelectedResidentData.Instance.selectedResident;

        if (resident != null)
        {
            // �^�C��������ς݂ɕύX
            tilemap.SetTile(tilePos, occupiedTile);
            // �Z�����������Ȃǂɕۑ��i��: residents.Add(tilePos, resident);�j
            Debug.Log($"{resident.name} ���������܂����B");
        }
    }

    public void SwitchTileToVacant(Vector3Int position)
    {
        tilemap.SetTile(position, vacantTile); // �w�肳�ꂽ�ʒu�̃^�C����ύX
    }

    // �󂢂Ă��镔�����`�F�b�N���郁�\�b�h
    public bool IsRoomAvailable(Vector3Int position)
    {
        return !residents.ContainsKey(position);
    }

    // �ǉ��ŁA�Z������\�����郁�\�b�h
    public void DisplayResidentInfo(Vector3Int position)
    {
        if (residents.TryGetValue(position, out Resident resident))
        {
            // UI�ɏZ������\�����郍�W�b�N
            Debug.Log($"�Z����: {resident.name}, �N��: {resident.age}, ����: {resident.gender}");
        }
    }

    //    void PopulateResidents()
    //    {
    //        foreach (CS_CreateResident resident in residents)
    //        {
    //            GameObject residentGO = Instantiate(residentPrefab, transform);
    //            CS_ResidentUI residentUI = residentGO.GetComponent<CS_ResidentUI>();

    //            // UI�ɏZ������ݒ�
    //            residentUI.SetResidentData(resident);
    //        }
    //    }
}