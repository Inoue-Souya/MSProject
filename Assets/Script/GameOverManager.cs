using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // �^�C�g���V�[���ɖ߂�
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title Scene"); // TitleScene�̖��O���Ⴄ�ꍇ�́A���������O�ɕύX���Ă�������
    }

    // �Q�[�����C���V�[���ɖ߂�i�ăX�^�[�g�j
    public void RetryGame()
    {
        SceneManager.LoadScene("Stage1"); // GameMainScene�̖��O���Ⴄ�ꍇ�́A���������O�ɕύX���Ă�������
    }
}
