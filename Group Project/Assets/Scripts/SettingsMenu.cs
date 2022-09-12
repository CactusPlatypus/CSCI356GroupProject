using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public GameObject mainMenu;
    public GameObject settingsMenu;


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
