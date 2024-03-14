using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject settings;
    public Slider sensSlider;
    public Slider volumeSlider;

    [SerializeField] AudioMixer mixer;
    public GameObject musicSource;
    [SerializeField] CharacterMovement movementScript;
    [SerializeField] GameObject buttonInner;
    [SerializeField] Color buttonInnerColor1;
    [SerializeField] Color buttonInnerColor2;

    [SerializeField] GameObject deathScreen;

    SettingsHolder settingsHolder;

    bool paused = false;
    public bool dead;

    public void Die()
    {
        dead = true;
        deathScreen.SetActive(true);
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        buttonInner.GetComponent<Image>().color = buttonInnerColor1;

        settingsHolder = GameObject.Find("SettingsHolder").GetComponent<SettingsHolder>();

        if(settingsHolder.firstTimeStarting)
        {
            ChangeVolume();
            ChangeSensitivity();
            settingsHolder.firstTimeStarting = false;
        }
        else
        {
            volumeSlider.value = settingsHolder.volume;
            sensSlider.value = settingsHolder.sens;

            if(!settingsHolder.musicEnabled) ToggleMusic();
        }
    }

    public void ChangeVolume()
    {
        mixer.SetFloat("SettingsVolume", Mathf.Log10(volumeSlider.value) * 20);
        settingsHolder.volume = volumeSlider.value;
    }

    public void ToggleMusic()
    {
        if(musicSource.GetComponent<AudioSource>().isPlaying)
        {
            musicSource.GetComponent<AudioSource>().Pause();
            settingsHolder.musicEnabled = false;
        }
        else
        {
            musicSource.GetComponent<AudioSource>().UnPause();
            settingsHolder.musicEnabled = true;
        }

        if(buttonInnerColor1 == buttonInner.GetComponent<Image>().color)
        {
            buttonInner.GetComponent<Image>().color = buttonInnerColor2;
        }
        else
        {
            buttonInner.GetComponent<Image>().color = buttonInnerColor1;
        }
    }

    public void ChangeSensitivity()
    {
        movementScript.sens = sensSlider.value;
        settingsHolder.sens = sensSlider.value;
    }

    void Update()
    {
        if(dead)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(paused)
            {
                paused = false;
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                settings.SetActive(false);
            }
            else
            {
                paused = true;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                settings.SetActive(true);
            }
        }
    }
}
