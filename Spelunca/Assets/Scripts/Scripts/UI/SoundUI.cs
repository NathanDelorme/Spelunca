using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public SettingsSO settingsSO;
    public List<Slider> sliders;

    public void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        sliders[0].value = settingsSO.data.musicVolume;
        sliders[1].value = settingsSO.data.sfxVolume;
    }

    public void ChangeVolume()
    {
        settingsSO.data.musicVolume = sliders[0].value;
        settingsSO.data.sfxVolume = sliders[1].value;
    }
}
