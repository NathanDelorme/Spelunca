using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public SettingsSO settingsSO;

    public void Start()
    {
        LoadSettings();
    }

    public void OnApplicationQuit()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, settingsSO.data);
        stream.Close();
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.fun";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            settingsSO.data = formatter.Deserialize(stream) as SettingsData;

            stream.Close();
        }
        else
            settingsSO.data = new SettingsData();
    }
}
