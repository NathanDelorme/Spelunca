using System.Collections;
using System.Collections.Generic;
using Language;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIFunctions : MonoBehaviour
    {
        public SettingsData settingsData;
        public AudioMixer musicMixer;
        //private AudioMixer sfxMixer;

        private LanguageManager languageManager;

        public void Start()
        {
            languageManager = FindObjectOfType<LanguageManager>();
            languageManager.ApplyLanguage(settingsData.language);
            ApplyVolume();
        }

        public void Play()
        {
            SceneManager.LoadScene("Scenes/Templates/Level");
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ChangeLanguage(string language)
        {
            settingsData.language = language;
            languageManager.ApplyLanguage(language);
        }

        public void ApplyVolume()
        {
            musicMixer.SetFloat("MusicVol", Mathf.Log10(settingsData.musicVolume) * 20);
            //sfxMixer.SetFloat("SFXVol", Mathf.Log10(settingsSO.data.sfxVolume) * 20);
        }

        public void ApplyFullscreen()
        {
            Screen.fullScreen = settingsData.isFullscreen;

            if (settingsData.isFullscreen)
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
    }
}
