using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject infoObject;
    [SerializeField] GameObject mainMenuObject;

    public void OpenMenu()
    {
        infoObject.SetActive(false);
        mainMenuObject.SetActive(true);
    }

    public void OpenInfo()
    {
        infoObject.SetActive(true);
        mainMenuObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
