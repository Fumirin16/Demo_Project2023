//�S���ҁF�R����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �V�[���̑J�ڏ���
public class TranstionScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// �wTitle�x�V�[���ɑJ�ڂ���
    //public void Trans_Title()
    //{
    //    SceneManager.LoadScene(0);
    //}

    //// �wWayOfPlaying�x�V�[���ɑJ�ڂ���
    //public void Trans_WayPlay()
    //{
    //    SceneManager.LoadScene(1);
    //}

    //// �wMain�x�V�[���ɑJ�ڂ���
    //public void Trans_Main()
    //{
    //    SceneManager.LoadScene(2);
    //}

    //public void Trans_GameClear()
    //{
    //    SceneManager.LoadScene(3);
    //}

    //public void Trans_GameOver()
    //{
    //    SceneManager.LoadScene(4);
    //}

    public void Trans_Scene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // ���݂̃V�[�����ēǂݍ��݂���
    public void Trans_Retry()
    {
        SceneManager.LoadScene(2);
    }

    // �Q�[�����I��������
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
