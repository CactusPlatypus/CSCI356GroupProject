using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume");

        // Update sliders, causes mixer to update too
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        SFXSlider.value = SFXVolume;
    }

    public void setMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void setMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void setSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
