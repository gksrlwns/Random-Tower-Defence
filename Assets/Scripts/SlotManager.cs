using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlotManager : MonoBehaviour
{
    public GameObject saveFilePanel;
    public GameObject playerDataObj;
    public Text stageText;
    public Text restMoneyText;
    public Text restLifeText;

    public Text[] slotText;

    bool[] isSlot = new bool[3];
    void Start()
    {
        for (int i = 0; i < isSlot.Length; i++)
        {
            if (File.Exists(BackEndManager.instance.path + $"{i}") == true)
            {
                isSlot[i] = true;
                BackEndManager.instance.nowSlot = i;
                BackEndManager.instance.LoadFile();
                playerDataObj.SetActive(true);
                slotText[i].text = "";
                ShowPlayerDataTexts();
            }
            else
            {
                slotText[i].text = "비어있음";
            }
            BackEndManager.instance.ClearFile();
        }
    }
    public void Slot(int number)
    {
        BackEndManager.instance.nowSlot = number;
        if(isSlot[number])
        {
            BackEndManager.instance.LoadFile();
            GoGameScene();
        }
        else
        {
            GoGameScene();
        }
    }

    public void Back()
    {
        saveFilePanel.SetActive(false);
    }
    void ShowPlayerDataTexts()
    {
        stageText.text = $"현재 스테이지 : " + BackEndManager.instance.nowPlayerData.stage.ToString();
        restLifeText.text = $"남은 목숨 : " + BackEndManager.instance.nowPlayerData.restLife.ToString();
        restMoneyText.text = $"남은 돈 : " + BackEndManager.instance.nowPlayerData.money.ToString();
    }
    void GoGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
