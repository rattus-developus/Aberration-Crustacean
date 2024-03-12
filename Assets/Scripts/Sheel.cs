using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Sheel : Enemy
{
    NavMeshAgent agent;
    Transform playerTran;
    Animator anim;
    Vector3 zapTargetLocation;
    [SerializeField] float hitboxSize = 1f;
    [SerializeField] VisualEffect zapVFX;
    [SerializeField] Transform vfxStart;

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

    public void TakeAim()
    {
        zapTargetLocation = playerTran.position;
    }

    public void Attack()
    {
        zapVFX.SetVector3("Start", vfxStart.position);
        zapVFX.SetVector3("End", zapTargetLocation);
        zapVFX.Play();

        LayerMask mask = 1 << 6;
        Vector3 extents = new Vector3(hitboxSize / 2f, hitboxSize / 2f, hitboxSize / 2f);

        if(Physics.CheckBox(zapTargetLocation, extents, Quaternion.identity, mask))
        {
            playerTran.gameObject.GetComponent<CharacterHealthManager>().TakeHit(damage);
        }
    }
}
