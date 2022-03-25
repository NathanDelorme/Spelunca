using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// System de sauvegarde des données du joueur (des paramètres plus particulièrement).
/// </summary>
public class SaveSystem : MonoBehaviour
{
    /// <value>
    /// Objet qui stocke les paramètre du joueur en temps réel.
    /// </value>
    private SettingsData data;

    /// <summary>
    /// Fonction appelé quand le script est chargée.
    /// </summary>
    public void Awake()
    {
        data = GetComponent<SettingsData>();
        LoadSettings();
    }

    /// <summary>
    /// Fonction appelé juste avant la fermeture totale du jeu.
    /// </summary>
    public void OnApplicationQuit()
    {
        SaveSettings();
    }

    /// <summary>
    /// Fonction permettant de réinitialiser les données du joueur liées aux niveaux.
    /// </summary>
    public void resetGameData()
    {
        string saveId = "";

        for (int levelID = 1; levelID <= 20; levelID++)
        {
            if(levelID != 1)
            {
                saveId = Application.version + "Level" + levelID;
                PlayerPrefs.SetInt(saveId, -1);
            }
            saveId = Application.version + "LEVEL_DEATHS" + levelID;
            PlayerPrefs.SetInt(saveId, 0);
            saveId = Application.version + "LEVEL_JUMP" + levelID;
            PlayerPrefs.SetInt(saveId, 0);
            saveId = Application.version + "LEVEL_FULLTIME" + levelID;
            PlayerPrefs.SetFloat(saveId, 0);
            saveId = Application.version + "LEVEL_BESTTIME" + levelID;
            PlayerPrefs.SetFloat(saveId, 0);
        }

        saveId = Application.version + "ALL_DEATHS";
        PlayerPrefs.SetInt(saveId, 0);
        saveId = Application.version + "ALL_JUMP";
        PlayerPrefs.SetInt(saveId, 0);
        saveId = Application.version + "ALL_FULLTIME";
        PlayerPrefs.SetFloat(saveId, 0);
        saveId = Application.version + "ALL_BESTTIME";
        PlayerPrefs.SetFloat(saveId, 0);
    }

    /// <summary>
    /// Fonction qui permet de sauvegarder les données.
    /// </summary>
    public void SaveSettings()
    {
        if(data != null)
        {
            PlayerPrefs.SetString(Application.version + "settings_language", data.language);
            PlayerPrefs.SetFloat(Application.version + "settings_musicVolume", data.musicVolume);
            PlayerPrefs.SetFloat(Application.version + "settings_sfxVolume", data.sfxVolume);
            PlayerPrefs.SetInt(Application.version + "settings_fullscreen", data.isFullscreen ? 1 : 0);
            PlayerPrefs.SetInt(Application.version + "settings_fullscreenWidth", data.resolutionWidth);
            PlayerPrefs.SetInt(Application.version + "settings_fullscreenHeight", data.resolutionheight);
        }
    }

    /// <summary>
    /// Fonction qui permet de charger les données.
    /// </summary>
    public void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(Application.version + "settings_language"))
            PlayerPrefs.SetString(Application.version + "settings_language", "EN");
        if (!PlayerPrefs.HasKey(Application.version + "settings_musicVolume"))
            PlayerPrefs.SetFloat(Application.version + "settings_musicVolume", 0.5f);
        if (!PlayerPrefs.HasKey(Application.version + "settings_sfxVolume"))
            PlayerPrefs.SetFloat(Application.version + "settings_sfxVolume", 0.5f);
        if (!PlayerPrefs.HasKey(Application.version + "settings_fullscreen"))
            PlayerPrefs.SetInt(Application.version + "settings_fullscreen", 1);

        if (!PlayerPrefs.HasKey(Application.version + "settings_fullscreenWidth") || !PlayerPrefs.HasKey(Application.version + "settings_fullscreenHeight"))
        {
            List<int> widths = new List<int>() { 1280, 1366, 1600, 1920, 2560, 3840 };
            List<int> heights = new List<int>() { 720, 768, 900, 1080, 1440, 2160 };

            Resolution screenRes = Screen.currentResolution;
            int screenPx = screenRes.width * screenRes.height;
            int bestRes = 3;
            int minDiff = 3840 * 2160;

            for (int i = 0; i < widths.Count; i++)
            {
                int currentDif = widths[i] * heights[i] - screenPx;

                if (currentDif >= 0 && currentDif < minDiff)
                {
                    minDiff = currentDif;
                    bestRes = i;

                    if (minDiff == 0)
                        break;
                }
            }
            PlayerPrefs.SetInt(Application.version + "settings_fullscreenWidth", widths[bestRes]);
            PlayerPrefs.SetInt(Application.version + "settings_fullscreenHeight", heights[bestRes]);
        }
            

        data.language = PlayerPrefs.GetString(Application.version + "settings_language");
        data.musicVolume = PlayerPrefs.GetFloat(Application.version + "settings_musicVolume");
        data.sfxVolume = PlayerPrefs.GetFloat(Application.version + "settings_sfxVolume");
        data.isFullscreen = PlayerPrefs.GetInt(Application.version + "settings_fullscreen") == 1;
        data.resolutionWidth = PlayerPrefs.GetInt(Application.version + "settings_fullscreenWidth");
        data.resolutionheight = PlayerPrefs.GetInt(Application.version + "settings_fullscreenHeight");
    }
}
