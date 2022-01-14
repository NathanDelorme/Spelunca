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
        UpdateUI();
    }

    public void ChangeFullscreen()
    {
        settingsData.isFullscreen = !settingsData.isFullscreen;
    }

    public void UpdateUI()
    {
        fullscreenToggle.SetIsOnWithoutNotify(settingsData.isFullscreen);
    }
}
