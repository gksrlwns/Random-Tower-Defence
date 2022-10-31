using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlotManager : MonoBehaviour
{
    public GameObject saveFilePanel;
    public GameObject[] playerDataObj;
    public Text[] stageText;
    public Text[] restMoneyText;
    public Text[] restLifeText;
    public Text[] towerCountText;
    private int[] towerCount;

    TowerName towerName;

    public Text[] slotText;

    bool[] isSlot = new bool[3];
    void Start()
    {
        //타워 종류
        towerCount = new int[4];
        for (int i = 0; i < towerCount.Length; i++)
        {
            towerCount[i] = 0;
        }
        for (int i = 0; i < isSlot.Length; i++)
        {
            if (File.Exists(BackEndManager.instance.path + $"{i}") == true)
            {
                isSlot[i] = true;
                BackEndManager.instance.nowSlot = i;
                BackEndManager.instance.LoadFile();
                playerDataObj[i].SetActive(true);
                slotText[i].text = "";
                ShowPlayerDataTexts(i);
            }
            else
            {
                slotText[i].text = "비어있음";
            }
            BackEndManager.instance.ClearFile();
        }
    }
    public void Slot(int num)
    {
        BackEndManager.instance.nowSlot = num;
        if(isSlot[num])
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

    void ShowPlayerDataTexts(int num)
    {
        stageText[num].text = $"현재 스테이지 : " + BackEndManager.instance.nowPlayerData.stage.ToString();
        restLifeText[num].text = $"남은 목숨 : " + BackEndManager.instance.nowPlayerData.restLife.ToString();
        restMoneyText[num].text = $"남은 돈 : " + BackEndManager.instance.nowPlayerData.money.ToString();

        foreach (var tower in BackEndManager.instance.nowPlayerData.towers)
        {
            towerCount[tower.Towertype] = towerCount[tower.Towertype] + 1;
            if (num == 0)
            {
                towerCountText[num + tower.Towertype].text = towerCount[tower.Towertype].ToString();
            }
            else
            {
                towerCountText[(num * 4) + tower.Towertype].text = towerCount[tower.Towertype].ToString();
            }
        }
    }
    void GoGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
