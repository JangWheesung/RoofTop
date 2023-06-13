using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangeRadiue;
    public float attack;
    bool isAttacking;

    private GameObject player;
    private Rigidbody rb;
    private Animator animator;
    private NavMeshAgent agent;
    private ZombieHP zombieHP;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        zombieHP = gameObject.GetComponent<ZombieHP>();
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        if(!isAttacking)
            agent.destination = player.transform.position;
    }

    private void Attack()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, rangeRadiue, LayerMask.GetMask("Player"));
        if (col.Length > 0 && !isAttacking && !zombieHP.die)
        {
            isAttacking = true;
            player.transform.GetComponent<PlayerHP>().OnDmage(attack);
            StopAllCoroutines();
            StartCoroutine(AttackDelay(1f));
        }
    }

    IEnumerator AttackDelay(float time)
    {
        agent.isStopped = true;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(time);
        agent.isStopped = false;
        animator.SetBool("Attack", false);
        isAttacking = false;
    }
}
