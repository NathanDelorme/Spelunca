using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private SettingsData data;
    /*private static string directory = "/SaveData/";
    private static string fileName = "SettingsData.txt";*/

    public void Start()
    {
        data = GetComponent<SettingsData>();
        LoadSettings();
    }

    public void OnApplicationQuit()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        /*string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dir + fileName, json);*/

        PlayerPrefs.SetString("settings_language", data.language);
        PlayerPrefs.SetFloat("settings_musicVolume", data.musicVolume);
        PlayerPrefs.SetFloat("settings_sfxVolume", data.sfxVolume);
        PlayerPrefs.SetInt("settings_fullscreen", data.isFullscreen ? 1 : 0);
    }

    public void LoadSettings()
    {
        /*string path = Application.persistentDataPath + directory + fileName;
        SettingsData savedSettings = new SettingsData();

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            savedSettings = JsonUtility.FromJson<SettingsData>(json);
        }

        data.language = savedSettings.language;
        data.musicVolume = savedSettings.musicVolume;
        data.sfxVolume = savedSettings.sfxVolume;
        data.isFullscreen = savedSettings.isFullscreen;*/
        if(!PlayerPrefs.HasKey("settings_language"))
            PlayerPrefs.SetString("settings_language", "EN");
        if (!PlayerPrefs.HasKey("settings_musicVolume"))
            PlayerPrefs.SetFloat("settings_musicVolume", 0.5f);
        if (!PlayerPrefs.HasKey("settings_sfxVolume"))
            PlayerPrefs.SetFloat("settings_sfxVolume", 0.5f);
        if (!PlayerPrefs.HasKey("settings_fullscreen"))
            PlayerPrefs.SetInt("settings_fullscreen", 1);

        data.language = PlayerPrefs.GetString("settings_language");
        data.musicVolume = PlayerPrefs.GetFloat("settings_musicVolume");
        data.sfxVolume = PlayerPrefs.GetFloat("settings_sfxVolume");
        data.isFullscreen = PlayerPrefs.GetInt("settings_fullscreen") == 1;
    }
}
