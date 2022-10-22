using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    
    private void Awake()
    {
        //if(instance)
        //{
        //    print("BuildManager가 하나 더 있습니다");
        //    return;
        //}
        //instance = this;
    }
    public GameObject StandardTowerPrefab;
    private void Start()
    {
        TowerToBuild = StandardTowerPrefab;
    }
    private GameObject TowerToBuild;
    
}
