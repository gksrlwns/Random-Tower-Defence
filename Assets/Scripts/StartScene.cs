using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;

public class StartScene : MonoBehaviour
{
    
    public void GameStart()
    {
        SceneManager.LoadScene(1);
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
