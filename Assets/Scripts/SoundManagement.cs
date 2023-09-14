using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManagement : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    private float currentMusicVolume;
    private float currentSFXVolume;

    private void Awake()
    {
        mixer.GetFloat(MIXER_MUSIC, out currentMusicVolume);
        mixer.GetFloat(MIXER_SFX, out currentSFXVolume);

        musicVolume.value = Remap(currentMusicVolume);
        sfxVolume.value = Remap(currentSFXVolume);

        musicVolume.onValueChanged.AddListener(UpdateMusicVolumne);
        sfxVolume.onValueChanged.AddListener(UpdateSFXVolume);
    }

    void UpdateMusicVolumne(float amount)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(amount) * 20);
    }

    void UpdateSFXVolume(float amount)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(amount) * 20);
    }

    float Remap(float value)
    {
        return Mathf.Pow(10, (value / 20));
    }
}