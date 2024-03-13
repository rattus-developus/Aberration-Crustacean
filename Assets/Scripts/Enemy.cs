using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float fallSpeed = 75f;
    [SerializeField] float height = 10f;
    [SerializeField] LayerMask landedMask;
    protected bool landed;

    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] protected float damage;
    [SerializeField] int coinsToDrop;
    [SerializeField] float coinsRandomOffset = 0.2f;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] protected int weaponDropChance;
    [SerializeField] protected GameObject weaponToDrop;

    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioSource impactSound;
    [SerializeField] AudioSource attackSound;

    virtual public void PlayAttackSound()
    {
        attackSound.pitch = Random.Range(0.9f, 1.1f);
        attackSound.Play();
    }

    virtual public void Update()
    {
        if(!landed && Physics.Raycast(transform.position, Vector3.down, height, landedMask))
        {
            impactSound.pitch = Random.Range(0.9f, 1.1f);
            impactSound.Play();
            landed = true;
            GetComponent<NavMeshAgent>().enabled = true;
        }

        if(!landed)
        {
            transform.position -= new Vector3(0f, fallSpeed * Time.deltaTime, 0f);
            return;
        }

        if(health <= 0f)
        {
            //Death
            GameObject.Find("WaveManager").GetComponent<WaveManager>().enemyCount--;

            for(int i = 0; i < coinsToDrop; i++)
            {
                float randomOffsetX = Random.Range(-coinsRandomOffset, coinsRandomOffset);
                float randomOffsetY = Random.Range(-coinsRandomOffset, coinsRandomOffset);
                float randomOffsetZ = Random.Range(-coinsRandomOffset, coinsRandomOffset);
                Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY + 0.5f, randomOffsetZ);

                float randomRotationX = Random.Range(0f, 360f);
                float randomRotationY = Random.Range(0f, 360f);
                float randomRotationZ = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(randomRotationX, randomRotationY, randomRotationZ);

                GameObject coin = Instantiate(coinPrefab);
                coin.transform.position = transform.position + randomOffset;
                coin.transform.rotation = randomRotation;
            }

            if(Random.Range(0, weaponDropChance) == 0)
            {
                float randomOffsetX = Random.Range(-coinsRandomOffset, coinsRandomOffset);
                float randomOffsetY = Random.Range(-coinsRandomOffset, coinsRandomOffset);
                float randomOffsetZ = Random.Range(-coinsRandomOffset, coinsRandomOffset);
                Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY + 0.5f, randomOffsetZ);

                float randomRotationX = Random.Range(0f, 360f);
                float randomRotationY = Random.Range(0f, 360f);
                float randomRotationZ = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(randomRotationX, randomRotationY, randomRotationZ);

                GameObject drop = Instantiate(weaponToDrop);
                drop.transform.position = transform.position + randomOffset;
                drop.transform.rotation = randomRotation;
            }

            Destroy(gameObject);
        }
    }

    public void TakeHit(float damage, float kbSpeed, float kbTime, Transform cam)
    {
        Vector3 kbDir = -transform.forward;

        health -= damage;
        GetComponent<HitEffects>().Flash();
        GetComponent<HitEffects>().Knockback(kbDir, kbSpeed, kbTime);

        hurtSound.Play();
        hurtSound.pitch = Random.Range(0.9f, 1.1f);
    }
}