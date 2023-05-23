using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private GameObject player;
    private Rigidbody rb;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
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
}
