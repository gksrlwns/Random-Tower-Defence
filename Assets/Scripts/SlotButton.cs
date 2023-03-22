using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public Color onClickCol;
    public Color startCol;
    GameManager _gameManager;
    public Button createBtn;
    public Button deleteBtn;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

    }
    private void Update()
    {
        if(!_gameManager.isCreate)
        {
            createBtn.image.color = startCol;
        }
        else if(_gameManager.isCreate)
        {
            createBtn.image.color = onClickCol;
        }

        if(!_gameManager.isDelete)
        {
            deleteBtn.image.color = startCol;
        }
        else if(_gameManager.isDelete)
        {
            deleteBtn.image.color = onClickCol;
        }
    }
}
