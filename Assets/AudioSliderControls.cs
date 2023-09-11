using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSliderControls : MonoBehaviour
{
    public AudioMixer mix;

    public void ChangeMusicVol (float newValue)
    {
        mix.SetFloat("MusicVolume", Mathf.Log10(newValue) * 20);
        Debug.Log("M - " + newValue);
    }

    public void ChangeSFXVol(float newValue)
    {
        mix.SetFloat("SFXVolume", Mathf.Log10(newValue) * 20);
    }

    public void ChangeMasterVol(float newValue)
    {
        mix.SetFloat("MasterVolume", Mathf.Log10(newValue) * 20);
    }
}
