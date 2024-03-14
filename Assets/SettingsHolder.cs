using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsHolder : MonoBehaviour
{
    public float volume;
    public float sens;
    public bool musicEnabled = true;
    public bool firstTimeStarting = true;

    void Awake()
    {
        if(GameObject.Find("SettingsHolder") != gameObject) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
