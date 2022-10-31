using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class TowerPrefabs
//{
//    public GameObject[] standardPrefabs;
//    public GameObject[] upgradePrefanbs;
//}

public class BuildManager : MonoBehaviour
{
    //public static BuildManager instance;
    public GameObject[] towerPrefabs;
    public List<Vector3>[] towerTrs;
    public GameObject createTowerEffect;
    public GameObject upgradeTowerEffect;
    public Vector3 buildEffectPositionOffset;
    public Vector3 upgradeEffectPositionOffset;
    private GameManager _gameManager;
    private Node[] nodes;

    Tower.TowerType towerType;
    
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        nodes = FindObjectsOfType<Node>();
    }
    private void Start()
    {
        towerTrs = new List<Vector3>[(int)Tower.TowerType.kindnum];
        for (int i = 0; i < nodes.Length; i++)
        {
            Destroy(nodes[i].standardTower);
        }
        for (int i = 0; i < towerTrs.Length; i++)
        {
            towerTrs[i] = new List<Vector3>();
        }
        foreach (var tower in BackEndManager.instance.nowPlayerData.towers)
        {
            if (tower.Towertype < 2)
            {
                towerTrs[tower.Towertype].Add(tower.TowerPosition);
            }
            for (int i = 0; i < nodes.Length; i++)
            {
                if(nodes[i].GetNodePosition() == tower.TowerPosition)
                {
                    GameObject nodeTower = Instantiate(towerPrefabs[tower.Towertype], tower.TowerPosition, Quaternion.identity);
                    nodes[i].standardTower = nodeTower;
                    print("저장된 타워 생성");
                    break;
                }
            }
            
            //for (int i = 0; i < nodes.Length; i++)
            //{
            //    if (nodes[i].standardTower) return;
            //    //nodes[i].standardTower = Instantiate(towerPrefabs[tower.Towertype], tower.TowerPosition, Quaternion.identity);
            //}
        }
    }

    private void Update()
    {

        for (int i = 0; i < towerTrs.Length; i++)
        {
            if (towerTrs[i].Count >= 3)
            {
                Debug.Log($"{((TowerName)i)} 3개 삭제 후 업그레이드");
                TowerUpgrade(towerPrefabs[i+2], towerTrs[i], i);
            }
        }
    }
    public void BuildStandardTower(Node node)
    {
        RandomPrefabs();
        if (towerType == Tower.TowerType.CannonTower)
        {
            print($"{TowerName.대포타워} 생성");
        }
        else
        {
            print($"{TowerName.공속타워} 생성");
        }
        GameObject tower = Instantiate(towerPrefabs[(int)towerType], node.GetNodePosition(), Quaternion.identity);
        node.standardTower = tower;
        node.towerTypeNum = (int)towerType;
        towerTrs[(int)towerType].Add(node.standardTower.transform.position);
        if (towerTrs[(int)towerType].Count < 3)
        {
            GameObject createTowerEff = Instantiate(createTowerEffect, node.GetNodePosition() + buildEffectPositionOffset, node.transform.rotation);
            Destroy(createTowerEff, 1f);
        }
    }

    public void DemolishStandardTower(Node node)
    {
        towerTrs[(int)towerType].Remove(node.standardTower.transform.position);
    }

    void RandomPrefabs()
    {
        towerType = (Tower.TowerType)Random.Range(0, towerPrefabs.Length - 2);
        //return TowerKindNum;
    }
    void TowerUpgrade(GameObject towerPrefab, List<Vector3> towerTrs, int num)
    {
        num += 2;
        for (int i = 0; i < nodes.Length; i++)
        {
            if(nodes[i].GetNodePosition() == towerTrs[0])
            {
                Destroy(nodes[i].standardTower);
            }
            if (nodes[i].GetNodePosition() == towerTrs[1])
            {
                Destroy(nodes[i].standardTower);
            }
            if (nodes[i].GetNodePosition() == towerTrs[2])
            {
                Destroy(nodes[i].standardTower);
            }

        }
        GameObject upgradeTower = Instantiate(towerPrefab, towerTrs[0], Quaternion.identity);
        
        for (int i = 0; i < nodes.Length; i++)
        {
            if(nodes[i].GetNodePosition() == towerTrs[0])
            {
                nodes[i].standardTower = upgradeTower;
                nodes[i].towerTypeNum = num;
            }
        }
        var upgradeTowerEff = Instantiate(upgradeTowerEffect, towerTrs[0] + upgradeEffectPositionOffset, Quaternion.identity);
        Destroy(upgradeTowerEff, 2f);
        towerTrs.Clear();
    }

}
