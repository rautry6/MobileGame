using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSliderControls : MonoBehaviour
{
    public AudioMixer mix;
    public bool hasSettingsMenu = false;
    public Slider master, sfx, music;

    private void Start()
    {
        mix.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 1)) * 20);
        mix.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 1)) * 20);
        mix.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume", 1)) * 20);

        if (hasSettingsMenu)
        {
            master.value = PlayerPrefs.GetFloat("MasterVolume", 1);
            sfx.value = PlayerPrefs.GetFloat("SFXVolume", 1);
            music.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        }

    }

    public void ChangeMusicVol (float newValue)
    {
        mix.SetFloat("MusicVolume", Mathf.Log10(newValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", newValue);
    }

    public void ChangeSFXVol(float newValue)
    {
        Debug.Log(newValue);
        mix.SetFloat("SFXVolume", Mathf.Log10(newValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", newValue);
        Debug.Log(PlayerPrefs.GetFloat("SFXVolume", 1));
    }

    public void ChangeMasterVol(float newValue)
    {
        mix.SetFloat("MasterVolume", Mathf.Log10(newValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", newValue);
    }
}
