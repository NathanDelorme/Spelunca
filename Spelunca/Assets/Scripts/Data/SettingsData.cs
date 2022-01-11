using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsData
{
    public string language = "EN";

    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;

    public bool isFullscreen = true;
}
