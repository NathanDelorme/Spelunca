using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    public SettingsData settingsData;
    public Toggle fullscreenToggle;

    public void OnEnable()
    {
        fullscreenToggle.isOn = settingsData.isFullscreen;
    }

    public void UpdateUI()
    {
        settingsData.isFullscreen = !settingsData.isFullscreen;
    }
}
