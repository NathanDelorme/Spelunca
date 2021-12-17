using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    void Start()
    {
        InitializePlayerPref();
        SetUI();
        ApplyVolume();
        ApplyFullScreen();
    }

    public void InitializePlayerPref()
    {
        if (!PlayerPrefs.HasKey("settings_soundvolume"))
            PlayerPrefs.SetFloat("settings_soundvolume", 0.5f);

        if (!PlayerPrefs.HasKey("settings_resolution"))
            PlayerPrefs.SetInt("settings_resolution", 0);

        if (!PlayerPrefs.HasKey("settings_fullscreen"))
            PlayerPrefs.SetInt("settings_fullscreen", 1);
    }

    public void SetUI()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("settings_soundvolume");
        resolutionDropdown.value = PlayerPrefs.GetInt("settings_resolution");
        fullscreenToggle.isOn = PlayerPrefs.GetInt("settings_fullscreen") == 1 ? true : false;
    }

    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("settings_soundvolume", volumeSlider.value);
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("settings_soundvolume")) * 20);
    }

    public void ApplyResolution()
    {
        PlayerPrefs.SetInt("settings_resolution", resolutionDropdown.value);

        switch (PlayerPrefs.GetInt("settings_resolution"))
        {
            case 0:
                Screen.SetResolution(1920, 1080, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 1:
                Screen.SetResolution(1280, 720, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 2:
                Screen.SetResolution(3840, 2160, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 3:
                Screen.SetResolution(5120, 2880, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 4:
                Screen.SetResolution(7680, 4320, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 5:
                Screen.SetResolution(15360, 8640, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
        }
    }

    public void ApplyFullScreen()
    {
        PlayerPrefs.SetInt("settings_fullscreen", (fullscreenToggle.isOn ? 1 : 0));
        Screen.fullScreen = PlayerPrefs.GetInt("settings_fullscreen") == 1;
        ApplyResolution();
    }
}
