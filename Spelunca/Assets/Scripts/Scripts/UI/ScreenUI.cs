using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUI : MonoBehaviour
{
    public SettingsData settingsData;
    public Toggle fullscreenToggle;
    public TMP_Dropdown screenResolutionDropdown;
    public List<int> widths = new List<int>() {1280, 1366, 1600, 1920, 2560, 3840};
    public List<int> heights = new List<int>() {720, 768, 900, 1080, 1440, 2160};
    public int dropdownSelectedValue = 0;

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
        getSelectedValueFromSettings();
        screenResolutionDropdown.SetValueWithoutNotify(dropdownSelectedValue);
    }
    
    public void ChangeScreenSize()
    {
        dropdownSelectedValue = screenResolutionDropdown.value;
        settingsData.resolutionWidth = widths[dropdownSelectedValue];
        settingsData.resolutionheight = heights[dropdownSelectedValue];
    }

    public void getSelectedValueFromSettings()
    {
        switch(settingsData.resolutionWidth)
        {
            case 1280:
                dropdownSelectedValue = 0;
                break;
            case 1366:
                dropdownSelectedValue = 1;
                break;
            case 1600:
                dropdownSelectedValue = 2;
                break;
            case 1920:
                dropdownSelectedValue = 3;
                break;
            case 2560:
                dropdownSelectedValue = 4;
                break;
            case 3840:
                dropdownSelectedValue = 5;
                break;
            default:
                dropdownSelectedValue = 3;
                break;
        }
    }
}
