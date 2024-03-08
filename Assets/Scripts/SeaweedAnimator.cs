using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedAnimator : MonoBehaviour
{
    void Start()
    {
        float randomRotation = Random.Range(0f, 360f);
        float randomRotationX = Random.Range(0f, 10f);
        float randomRotationZ = Random.Range(0f, 10f);
        transform.Rotate(randomRotationX, randomRotation, randomRotationZ);

        StartCoroutine(StartAnimationCoroutine());
    }

    IEnumerator StartAnimationCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f));
        
        int randomAnimation = Random.Range(0, 3);
        GetComponent<Animator>().SetInteger("random", randomAnimation);
    }
}
