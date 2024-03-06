using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] protected float damage;

    virtual public void Update()
    {
        if(health <= 0f)
        {
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
