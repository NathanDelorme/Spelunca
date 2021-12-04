using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    void Start()
    {
        InitializePlayerPref();
        setUI();
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

    public void setUI()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("settings_soundvolume");
        resolutionDropdown.value = PlayerPrefs.GetInt("settings_resolution", 0);
        Debug.Log(resolutionDropdown.options.ToArray().ToString());
        fullscreenToggle.isOn = PlayerPrefs.GetInt("settings_fullscreen") == 1 ? true : false;
    }
}
