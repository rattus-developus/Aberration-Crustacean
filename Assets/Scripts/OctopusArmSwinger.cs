using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusArmSwinger : MonoBehaviour
{
    [SerializeField] Transform swingTrans;
    [SerializeField] float rotationSpeed;
    [SerializeField] float hitCooldown;
    float hitTimer;
    bool swinging;

    void Awake()
    {
        hitTimer = hitCooldown;
    }

    void Update()
    {
        AnimatorClipInfo[] animatorInfo = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);

        if(animatorInfo[0].clip.name != "OctoPunch")
        {
            swinging = false;
        }

        if(swinging)
        {
            hitTimer -= Time.deltaTime;

            if(hitTimer <= 0)
            {
                //Do hit
                GetComponent<ArmCommands>().Attack();
                hitTimer = hitCooldown;
            }

            swingTrans.RotateAround(transform.forward, rotationSpeed);
        }
    }

    public void StartSwinging()
    {
        hitTimer = hitCooldown;
        swinging = true;
    }
}
