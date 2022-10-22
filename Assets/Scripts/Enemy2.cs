using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{

    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
       
        hp = 500;
        base.Start();
    }
    private void OnDestroy()
    {
        base.OnDestroy();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
