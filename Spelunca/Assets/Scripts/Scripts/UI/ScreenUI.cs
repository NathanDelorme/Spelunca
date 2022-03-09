using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    public SettingsData settingsData;
    public Toggle fullscreenToggle;
    public List<int> widths = new List<int>() {1280, 1366, 1600, 1920, 2560, 3840};
    public List<int> heights = new List<int>() {720, 768, 900, 1080, 1440, 2160};

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
    
    public void ChangeScreenSize(int index)
    {
        settingsData.resolutionWidth = widths[index];
        settingsData.resolutionheight = heights[index];
    }
}
