using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject saveFilePanel;
    public void GameStart()
    {
        saveFilePanel.SetActive(true);
        //SceneManager.LoadScene(1);
        //var bro = Backend.BMember.GuestLogin("게스트 로그인으로 로그인");
        //if (bro.IsSuccess()) print("게스트 로그인 성공");
        //else
        //{
        //    print(bro.GetMessage());
        //}
    }
    public void ExitGame()
    {
        Application.Quit();
    }    
    
}
