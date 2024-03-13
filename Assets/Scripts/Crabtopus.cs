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

    [SerializeField] AudioSource walkAudio;
    [SerializeField] float walkPlayCooldown = 0.75f;
    float walkPlayCooldownTimer;

    [SerializeField] float rotSpeed = 5f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    override public void Update()
    {
        base.Update();
        if(!landed) return;

        if(Vector3.Distance(agent.destination, transform.position) > agent.stoppingDistance + 1f && walkPlayCooldownTimer <= 0f)
        {
            //let audio play
            walkAudio.pitch = Random.Range(0.9f, 1.1f);
            walkPlayCooldownTimer = walkPlayCooldown;
            walkAudio.Play();
        }
        else
        {
            walkPlayCooldownTimer -= Time.deltaTime;
        }

        if(Vector3.Distance(agent.destination, transform.position) < agent.stoppingDistance)
        {
            Vector3 lookPos = agent.destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed);
        }

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
