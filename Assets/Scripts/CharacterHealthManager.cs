using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CharacterHealthManager : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;
    [SerializeField] float regenDelay;
    [SerializeField] float regenSpeed;

    [SerializeField] AudioSource hurtSound;

    float timeSinceLastHit;
    Volume volume;

    void Awake()
    {
        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        timeSinceLastHit = 0f;

        hurtSound.pitch = Random.Range(0.9f, 1.1f);
        hurtSound.Play();

        if(health <= 0f)
        {
            Debug.Log("Game over");
        }
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        if(timeSinceLastHit >= regenDelay && health < maxHealth) health += regenSpeed * Time.deltaTime;
        else if(health > maxHealth) health = maxHealth;

        foreach(VolumeComponent vc in volume.sharedProfile.components)
        {
            if(vc.GetType() == typeof(Vignette))
            {
                Vignette volumeVignette = (Vignette)vc;

                if(health == maxHealth)
                {
                    volumeVignette.intensity.Override(0f);
                }
                else
                {
                    float lerpedVal = Mathf.Lerp(1f, 0.25f, health / maxHealth);
                    volumeVignette.intensity.Override(lerpedVal);
                }

                break;
            }
        }
    }
}
