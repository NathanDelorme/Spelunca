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
        public SettingsSO settingsSO;
        public AudioMixer musicMixer;
        //private AudioMixer sfxMixer;

        private LanguageManager languageManager;

        public void Start()
        {
            languageManager.ApplyLanguage(settingsSO.data.language);
            ApplyVolume();
        }

        public void Awake()
        {
            languageManager = FindObjectOfType<LanguageManager>();
        }

        public void Play()
        {
            SceneManager.LoadScene("Scenes/Tests/Nathan/Old/NewScene");
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ChangeLanguage(string language)
        {
            settingsSO.data.language = language;
            languageManager.ApplyLanguage(language);
        }

        public void ApplyVolume()
        {
            musicMixer.SetFloat("MusicVol", Mathf.Log10(settingsSO.data.musicVolume) * 20);
            //sfxMixer.SetFloat("SFXVol", Mathf.Log10(settingsSO.data.sfxVolume) * 20);
        }

        public void ApplyFullscreen()
        {
            Screen.fullScreen = settingsSO.data.isFullscreen;

            if (settingsSO.data.isFullscreen)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            }
        }
    }
}
