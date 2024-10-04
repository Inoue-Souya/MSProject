using UnityEngine;
using UnityEngine.UI; // UI�R���|�[�l���g���g�p

public class ImageCycler : MonoBehaviour
{
    public UnityEngine.UI.Image targetImage;  // �ύX����Ώۂ�Image�R���|�[�l���g
    public Sprite[] sprites;                  // �摜��ێ�����X�v���C�g�̔z��
    private int currentIndex = 0;             // ���ݕ\�����Ă���摜�̃C���f�b�N�X

    // �{�^���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void CycleImage()
    {
        if (sprites.Length == 0 || targetImage == null) return;

        // ���݂̃C���f�b�N�X��i�߂Ď��̉摜�ɐ؂�ւ�
        currentIndex = (currentIndex + 1) % sprites.Length;

        // �摜��ύX
        targetImage.sprite = sprites[currentIndex];
    }
}

