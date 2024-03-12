using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitEffects : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] Material flashMat; 
    Material defaultMat;
    [SerializeField] float flashTime = 0.05f;
    float flashTimer;
    bool isFlashing;

    float kbTime = 0f;
    float kbSpeed = 0f;
    float kbTimer;
    bool isMoving;
    Vector3 kbDirection;

    void Awake()
    {
        defaultMat = model.GetComponent<Renderer>().material;
    }

    public void Flash()
    {
        model.GetComponent<Renderer>().material = flashMat;
        flashTimer = flashTime;
        isFlashing = true;
    }

    public void Knockback(Vector3 direction, float speed, float time)
    {
        if(time == 0) return;

        if(isMoving && kbSpeed > speed);
        else kbSpeed = speed;

        if(isMoving && kbTime > time);
        else kbTime = time;

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Animator>().enabled = false;

        isMoving = true;
        kbDirection = direction.normalized;
        kbTimer = time;
    }

    void Update()
    {
        flashTimer -= Time.deltaTime;
        if(isFlashing && flashTimer <= 0f)
        {
            model.GetComponent<Renderer>().material = defaultMat;
            isFlashing = false;
        }

        kbTimer -= Time.deltaTime;
        if (isMoving && kbTimer > 0f)
        {
            transform.position += kbDirection * kbSpeed * Time.deltaTime;
        }

        else if(isMoving && kbTimer <= 0f)
        {
            isMoving = false;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Animator>().enabled = true;
        }
    }
}
