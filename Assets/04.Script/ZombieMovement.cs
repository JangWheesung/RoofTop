using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float attack;

    private GameObject player;
    private Rigidbody rb;
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        agent.destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerHP>().OnDmage(attack);
            AttackDelay(1f);
        }
    }

    IEnumerator AttackDelay(float time)
    {
        agent.isStopped = true;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(time);
        agent.isStopped = false;
        animator.SetBool("Attack", false);
    }
}
