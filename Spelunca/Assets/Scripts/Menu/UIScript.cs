using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
///  This class is used to allow the user to change his settings in the settings menu present in the main menu and the pause menu.
/// </summary>
public class UIScript : MonoBehaviour
{
    /// <value>
    /// The <c>mixer</c> property is a AudioMixer.
    /// It contain the music of the game. and allorw us to modify the properties of the music.
    /// </value>
    public AudioMixer mixer;
    /// <value>
    /// The <c>volumeSlider</c> property is a Slider which is a graphical element of the user interface.
    /// This slider have a value the indicates the power of the sound volume.
    /// </value>
    public Slider volumeSlider;
    /// <value>
    /// The <c>resolutionDropdown</c> property is a TMP_Dropdown which is a graphical element of the user interface.
    /// From this dropdown element, we will get the resolution choice made by the user.
    /// </value>
    public TMP_Dropdown resolutionDropdown;
    /// <value>
    /// The <c>fullscreenToggle</c> property is a Toggle which is a graphical element of the user interface.
    /// This Toggle element allow us to know if the user want his game in a windowed mode or in a fullscreen mode.
    /// </value>
    public Toggle fullscreenToggle;

    /// <summary>
    /// Function executed at the start of the program.
    /// First, we initialize the PlayerPrefs if they were not initialized before.
    /// For example, the first time where you open the project
    /// Then the UI will set the value in function of what has been save
    /// And finaly, apply the current settings.
    /// </summary>
    void Start()
    {
        InitializePlayerPref();
        SetUI();
        ApplyVolume();
        ApplyFullScreen();
    }

    /// <summary>
    /// Function that initialize all PlayerPrefs for the settings.
    /// </summary>
    public void InitializePlayerPref()
    {
        if (!PlayerPrefs.HasKey("settings_soundvolume"))
            PlayerPrefs.SetFloat("settings_soundvolume", 0.5f);

        if (!PlayerPrefs.HasKey("settings_resolution"))
            PlayerPrefs.SetInt("settings_resolution", 0);

        if (!PlayerPrefs.HasKey("settings_fullscreen"))
            PlayerPrefs.SetInt("settings_fullscreen", 1);
    }

    /// <summary>
    /// Function that apply the current settings to the user interface
    /// </summary>
    public void SetUI()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("settings_soundvolume");
        resolutionDropdown.value = PlayerPrefs.GetInt("settings_resolution");
        fullscreenToggle.isOn = PlayerPrefs.GetInt("settings_fullscreen") == 1 ? true : false;
    }

    /// <summary>
    /// Function that apply the current settings volume.
    /// We used a logarithmic calculation because we talk about decibel and note pourcent.
    /// </summary>
    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("settings_soundvolume", volumeSlider.value);
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("settings_soundvolume")) * 20);
    }

    /// <summary>
    /// Function that apply the current settings resolution.
    /// </summary>
    public void ApplyResolution()
    {
        PlayerPrefs.SetInt("settings_resolution", resolutionDropdown.value);

        switch (PlayerPrefs.GetInt("settings_resolution"))
        {
            case 0:
                Screen.SetResolution(1920, 1080, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 1:
                Screen.SetResolution(1280, 720, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 2:
                Screen.SetResolution(3840, 2160, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 3:
                Screen.SetResolution(5120, 2880, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 4:
                Screen.SetResolution(7680, 4320, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
            case 5:
                Screen.SetResolution(15360, 8640, PlayerPrefs.GetInt("settings_fullscreen") == 1);
                break;
        }
    }

    /// <summary>
    /// Function that apply the current settings if the fullscreen mode is enable or not.
    /// </summary>
    public void ApplyFullScreen()
    {
        PlayerPrefs.SetInt("settings_fullscreen", (fullscreenToggle.isOn ? 1 : 0));
        Screen.fullScreen = PlayerPrefs.GetInt("settings_fullscreen") == 1;
        ApplyResolution();
    }
}
