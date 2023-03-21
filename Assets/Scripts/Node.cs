using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    public Color selectedColor;
    private Color startColor;
    public GameObject standardTower;
    public GameObject upgradeTower;
    public GameManager _gameManager;
    public BuildManager _buildManager;
    public Vector3 positionOffset;
    public int towerTypeNum;
    Tower.TowerType towerType;


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _buildManager = FindObjectOfType<BuildManager>();
    }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (!rend) return;
        rend.material.color = selectedColor;
    }
    private void OnMouseDown()
    {
        foreach(var tr in _buildManager.towerTrs)
        {
            if(tr.Count >= 3) return;
        }
        
        if (standardTower && _gameManager.isCreate == true)
        {
            print("타워가 있습니다");
            return;
        }
        if (!standardTower && _gameManager.isCreate == true && _gameManager.money >= 50)
        {
            _buildManager.BuildStandardTower(this);
            if(towerType == Tower.TowerType.CannonTower)
            {
                print($"{TowerName.대포타워} 생성");
            }
            else
            {
                print($"{TowerName.공속타워} 생성");
            }
            _gameManager.money -= 50;
            
        }
        if(!standardTower && _gameManager.isCreate == true && _gameManager.money < 50)
        {
            print("돈이 부족합니다.");
        }
        if (standardTower && _gameManager.isDelete == true)
        {
            _buildManager.DemolishStandardTower(this);
            _gameManager.money += 18;
            Destroy(standardTower);
            
        }


    }
    private void OnMouseExit()
    {
        if (!rend) return;
        rend.material.color = startColor;
    }

    public Vector3 GetNodePosition()
    {
        return transform.position + positionOffset;
    }
}
