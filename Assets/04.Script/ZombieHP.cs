using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHP : Living
{
    private Animator animator;
    private NavMeshAgent agent;
    private CapsuleCollider capsuleCollider;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    void Update()
    {

    }

    protected override void Die()
    {
        WaveManager.instance.enemyCount--;
        capsuleCollider.enabled = false;
        agent.isStopped = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 2);
    }
}
