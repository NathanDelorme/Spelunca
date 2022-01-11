using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    public SettingsSO settingsSO;
    public Toggle fullscreenToggle;

    public void OnEnable()
    {
        fullscreenToggle.isOn = settingsSO.data.isFullscreen;
    }

    public void UpdateUI()
    {
        settingsSO.data.isFullscreen = !settingsSO.data.isFullscreen;
    }
}
