using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHP : Living
{
    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

    protected override void Die()
    {
        WaveManager.instance.enemyCount--;
        animator.SetTrigger("Die");
        Destroy(gameObject, 2);
    }
}
