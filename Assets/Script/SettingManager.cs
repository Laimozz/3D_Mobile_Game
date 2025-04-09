using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("BG")]
    [SerializeField] private AudioMixer background;
    [SerializeField] private Slider sliderBG;

    [Header("SFX")]
    [SerializeField] private AudioMixer SFX;
    [SerializeField] private Slider sliderSFX;

    private void Awake()
    {
        sliderBG.value = PlayerPrefs.GetFloat("BGMVALUE", 0);
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVALUE", 0);
    }
    private void Start()
    {
        background.SetFloat("BGM", sliderBG.value);
        SFX.SetFloat("SFX", sliderSFX.value);
    }

    public void VolumeAudioBG()
    {
        background.SetFloat("BGM" , sliderBG.value);
        PlayerPrefs.SetFloat("BGMVALUE" , sliderBG.value);
    }

    public void VolumeAudioSFX()
    {
        SFX.SetFloat("SFX" , sliderSFX.value);
        PlayerPrefs.SetFloat("SFXVALUE" , sliderSFX .value);
    }
}
