using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] protected float damage;
    [SerializeField] int coinsToDrop;
    [SerializeField] float coinsRandomOffset = 0.2f;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] protected int weaponDropChance;
    [SerializeField] protected GameObject weaponToDrop;

    virtual public void Update()
    {
        if(health <= 0f)
        {
            //Death
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
    }
}