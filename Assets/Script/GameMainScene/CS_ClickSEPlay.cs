using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ClickSEPlay : MonoBehaviour
{
    public AudioClip soundEffect;  // ���ʉ���AudioClip
    public GameObject audioSourceObject;  // SE���Đ����邽�߂�AudioSource���A�^�b�`���ꂽ�Q�[���I�u�W�F�N�g

    private AudioSource audioSource;  // AudioSource�R���|�[�l���g

    void Start()
    {
        // �w�肳�ꂽ�Q�[���I�u�W�F�N�g����AudioSource�R���|�[�l���g���擾
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

        // AudioSource���������擾�ł��Ă��Ȃ��ꍇ�͌x�����o��
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component is not attached to the specified object.");
        }

        // AudioSource��Play On Awake�𖳌��ɂ���
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    // �N���b�N�C�x���g������
    void OnMouseDown()
    {
        // �����Đ�
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
        else
        {
            Debug.LogWarning("AudioSource or soundEffect is missing!");
        }
    }
}