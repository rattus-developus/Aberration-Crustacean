using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crabtopus : Enemy
{
    [SerializeField] BoxCollider attackBox;
    NavMeshAgent agent;
    Transform playerTran;
    Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    override public void Update()
    {
        base.Update();

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
        if(agent.enabled)
        {
            if(Vector3.Distance(agent.destination, playerTran.position) >= 0.1f && Vector3.Distance(transform.position, playerTran.position) >= agent.stoppingDistance)
            {
                agent.SetDestination(playerTran.position);
            }
        }
    }

    public void Attack()
    {
        LayerMask mask = 1 << 6;

        Vector3 size = new Vector3(attackBox.size.x * attackBox.transform.localScale.x * 2, attackBox.size.y * attackBox.transform.localScale.y * 2, attackBox.size.z * attackBox.transform.localScale.z * 2);
        Collider[] hitColliders = Physics.OverlapBox(attackBox.transform.position, size, transform.rotation, mask);
        if(hitColliders.Length > 0)
        {
            hitColliders[0].GetComponent<CharacterHealthManager>().TakeHit(damage);
        }
    }
}
