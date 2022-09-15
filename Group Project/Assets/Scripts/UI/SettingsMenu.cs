using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Menu displaying settings button, either pause menu or main menu
    public GameObject hostMenu;

    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public void Start()
    {
        updateSliders();
    }

    public void updateSliders()
    {
        float val;
        audioMixer.GetFloat("MasterVolume", out val);
        mainSlider.value = val;
        audioMixer.GetFloat("MusicVolume", out val);
        musicSlider.value = val;
        audioMixer.GetFloat("SFXVolume", out val);
        SFXSlider.value = val;
    }

    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void setSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void showSettings()
    {
        gameObject.SetActive(true);
        hostMenu.SetActive(false);
    }

    public void hideSettings()
    {
        gameObject.SetActive(false);
        hostMenu.SetActive(true);
    }

    public void loadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
