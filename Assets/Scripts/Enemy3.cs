using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        base.Start();
        hp = 1000;
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
