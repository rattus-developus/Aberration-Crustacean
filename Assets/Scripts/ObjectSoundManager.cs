using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    void Awake()
    {
        foreach(AudioClip clip in audioClips)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }

}