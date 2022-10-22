using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : Tower
{
    private void Start()
    {
        base.Start();
        damage = 60;
    }
    private void Update()
    {
        base.Update();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            base.enemiesList.Add(other.GetComponent<Enemy>());
        }
    }
}
