using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crabtopus : MonoBehaviour
{
    NavMeshAgent agent;
    Transform playerTran;
    Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, playerTran.position) <= agent.stoppingDistance)
        {
            anim.SetBool("walk", false);
            anim.SetBool("attack", true);
        }
        else
        {
            anim.SetBool("walk", true);
            anim.SetBool("attack", false);
        }
        
        //Set Destination
        if(Vector3.Distance(agent.destination, playerTran.position) >= 0.1f && Vector3.Distance(transform.position, playerTran.position) >= agent.stoppingDistance)
        {
            agent.SetDestination(playerTran.position);
        }
    }
}
