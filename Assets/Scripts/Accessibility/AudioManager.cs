using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer voulmeMixer;
    public Slider voulmeSlider;
    private float voulmeSliderValue;

    public void SetMasterVolume()
    {
        voulmeSliderValue = voulmeSlider.value;
        voulmeMixer.SetFloat("MusicVolume", Mathf.Log10(voulmeSliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", voulmeSliderValue);

    }

    public void SetSFXVolume()
    {
        voulmeSliderValue = voulmeSlider.value;
        voulmeMixer.SetFloat("SFXVolume", Mathf.Log10(voulmeSliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", voulmeSliderValue);

    }

}
