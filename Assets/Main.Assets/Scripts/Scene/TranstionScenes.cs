// �S���ҁF�R����
// �V�[���̑J�ڏ���
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class TranstionScenes : MonoBehaviour
{
    // �wTitle�x�V�[���ɑJ�ڂ���
    public void Trans_Title()
    {
        SceneManager.LoadScene(0);
    }

    // �wWayOfPlaying�x�V�[���ɑJ�ڂ���
    public void Trans_WayPlay()
    {
        SceneManager.LoadScene(1);
    }

    // �wMain�x�V�[���ɑJ�ڂ���
    public void Trans_Main()
    {
        SceneManager.LoadScene(2);
    }

    // ���݂̃V�[�����ēǂݍ��݂���
    public void Trans_Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �Q�[�����I��������
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
