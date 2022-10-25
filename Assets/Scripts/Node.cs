using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    public Color hoverColor;
    private Color startColor;
    public GameObject tower;
    public GameObject tower2;
    public GameObject[] TowerPrefabs;
    public GameManager gameManager;
    public Vector3 positionOffset;
    public int towerTypeNum;
    Tower.TowerType towerType;
    //TowerKind TowerKindNum;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (!rend) return;
        rend.material.color = hoverColor;
    }
    private void OnMouseDown()
    {
        foreach(var tr in gameManager.TowerTr)
        {
            if(tr.Count >= 3) return;
        }
        //if (gameManager.node_TC1 >= 3)
        //    return;
        //if (gameManager.node_TC2 >= 3)
        //    return;
        if (tower && gameManager.isCreate == true)
        {
            print("타워가 있습니다");
            return;
        }
        if (tower2 && gameManager.isCreate == true)
        {
            print("업그레이드된 타워가 있습니다");
            return;
        }
        if (!tower && gameManager.isCreate == true && gameManager.money >= 50)
        {
            RandomPrefabs();
            if(towerType == Tower.TowerType.CannonTower)
            {
                print($"{TowerName.대포타워} 생성");
            }
            else
            {
                print($"{TowerName.공속타워} 생성");
            }
            tower = Instantiate(TowerPrefabs[(int)towerType], transform.position + positionOffset, transform.rotation , gameManager.tower_Tr);
            //TowerDataType 구분
            towerTypeNum = (int)towerType;

            gameManager.TowerTr[(int)towerType].Add(tower.transform);

            //if (TowerKindNum == 0)
            //{
            //    gameManager.TowerTr1.Add(tower.transform);
            //    //gameManager.node_TC1++;
            //}
            //else
            //{
            //    gameManager.TowerTr2.Add(tower.transform);
            //    //gameManager.node_TC2++;
            //}
            gameManager.money -= 50;
            
        }
        if(!tower && gameManager.isCreate == true && gameManager.money < 50)
        {
            print("돈이 부족합니다.");
        }
        if (tower && gameManager.isDelete == true)
        {
            gameManager.TowerTr[(int)towerType].Remove(tower.transform);
            //if (TowerKindNum == 0)
            //{
            //    gameManager.TowerTr1.Remove(tower.transform);
            //    gameManager.node_TC1--;
            //}
            //else
            //{
            //    gameManager.TowerTr2.Remove(tower.transform);
            //    gameManager.node_TC2--;
            //}
           gameManager.money += 18;
            Destroy(tower);
            
        }


    }
    private void OnMouseExit()
    {
        if (!rend) return;
        rend.material.color = startColor;
    }
    void RandomPrefabs()
    {
        towerType = (Tower.TowerType)Random.Range(0, TowerPrefabs.Length);
        //return TowerKindNum;
    }
}
