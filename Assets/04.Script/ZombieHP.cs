using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHP : Living
{
    void Awake()
    {
        
    }

    void Update()
    {

    }

    protected override void Die()
    {
        WaveManager.instance.enemyCount--;
        Destroy(gameObject);
    }
}
