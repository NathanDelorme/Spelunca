using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public SettingsData settingsData;
    public List<Slider> sliders;

    public void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        sliders[0].SetValueWithoutNotify(settingsData.musicVolume);
        sliders[1].SetValueWithoutNotify(settingsData.sfxVolume);
    }

    public void ChangeVolume()
    {
        settingsData.musicVolume = sliders[0].value;
        settingsData.sfxVolume = sliders[1].value;
    }
}
