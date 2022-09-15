using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    [SerializeField] Slider mainSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;


    public void loadGame()
    {
        SceneManager.LoadScene(0);
        float val = 0;
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

    public void showMainMneu()
    {
        mainMenu.SetActive(true);
        hideSettings();
    }

    public void hideMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void showSettings()
    {
        settingsMenu.SetActive(true);
        hideMainMenu();
    }

    public void hideSettings()
    {
        settingsMenu.SetActive(false);
    }

}
