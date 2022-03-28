using Data;
using Language;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Classe poss�dant les fonctions principales du menu principal.
    /// </summary>
    public class UIFunctions : MonoBehaviour
    {
        /// <value>
        /// Objet qui stocke les param�tre du joueur en temps r�el.
        /// </value>
        public SettingsData settingsData;
        /// <value>
        /// Objet permettant la diffusion de la musique dans le jeu.
        /// </value>
        public AudioMixer musicMixer;
        /// <value>
        /// Objet permettant la diffusion des effets sonores dans le jeu.
        /// </value>
        public AudioMixer SFXMixer;
        /// <value>
        /// LanguageManager permettant la gestion des diff�rentes langues et traduction des textes.
        /// </value>
        private LanguageManager languageManager;

        /// <summary>
        /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
        /// </summary>
        public void Start()
        {
            languageManager = FindObjectOfType<LanguageManager>();
            languageManager.ApplyLanguage(settingsData.language);
            ApplyVolume();
        }

        /// <summary>
        /// Fonction permettant de lancer le niveau template.
        /// </summary>
        public void Play()
        {
            SceneManager.LoadScene("Scenes/Templates/Level");
        }

        /// <summary>
        /// Fonction permettant de quitter le jeu.
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Fonction appelant la fonction permettant de changer de langue.
        /// </summary>
        /// <param name="language">Langue que le joueur souhaite utiliser.</param>
        public void ChangeLanguage(string language)
        {
            settingsData.language = language;
            languageManager.ApplyLanguage(language);
        }

        /// <summary>
        /// Fonction permettant d'appliquer le volume de la musique et des effets sonores.
        /// </summary>
        public void ApplyVolume()
        {
            musicMixer.SetFloat("MusicVol", Mathf.Log10(settingsData.musicVolume) * 20);
            SFXMixer.SetFloat("SFXVol", Mathf.Log10(settingsData.sfxVolume) * 20);
        }

        /// <summary>
        /// Applique le dernier param�tre plein �cran pour mettre le jeu en plein �cran ou non.
        /// </summary>
        public void ApplyFullscreen()
        {
            Screen.fullScreen = settingsData.isFullscreen;

            if (settingsData.isFullscreen)
                Screen.SetResolution(settingsData.resolutionWidth, settingsData.resolutionheight, true);
        }

        /// <summary>
        /// Applique le dernier param�tre r�solution pour changer la r�solution du jeu.
        /// </summary>
        public void ApplyResolution()
        {
            Screen.SetResolution(settingsData.resolutionWidth, settingsData.resolutionheight, settingsData.isFullscreen);
        }
    }
}
