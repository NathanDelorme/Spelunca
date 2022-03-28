using Audio;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Script permettant la gestion et la mise en pause du jeu.
    /// </summary>
    public class PauseUI : MonoBehaviour
    {
        /// <value>
        /// Est-ce que le jeu est en pause ou non.
        /// </value>
        private bool paused = false;
        /// <value>
        /// Script menu pause
        /// </value>
        public UIScript pauseMenu;
        /// <value>
        /// Script menu des options
        /// </value>
        public UIScript settingsMenu;
        /// <value>
        /// Script menu des crédits
        /// </value>
        public UIScript creditsMenu;
        /// <value>
        /// Script du menu
        /// </value>
        public GameObject menu;
        /// <value>
        /// Système qui gère les entrées clavier/souris/manette du joueur.
        /// Permet de spécifier des paramètres pour la navigation dans les menus.
        /// </value>
        public EventSystem eventSystem;
        /// <value>
        /// Gestionnaire des effets sonores du jeu.
        /// </value>
        private SFXManager sfxManager;
        /// <value>
        /// Récupération des inputs du joueur.
        /// </value>
        private PlayerInput playerInput;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        public void Start()
        {
            sfxManager = FindObjectOfType<SFXManager>();
            playerInput = FindObjectOfType<PlayerInput>();
            Resume();
        }

        /// <summary>
        /// Fonction qui switch entre "Pause Mode" et "Play Mode".
        /// </summary>
        public void OnPause()
        {
            sfxManager.flipFlopPause();
            if (paused && menu.activeSelf)
                Resume();
            else if (paused == false)
                PauseGame();
        }

        /// <summary>
        /// Fonction qui met en pause le jeu ainsi que toutes les taches que le joueur effectuait lrosqu'il jouait.
        /// </summary>
        private void PauseGame()
        {
            playerInput.DeactivateInput();
            menu.SetActive(true);
            Time.timeScale = 0f;
            pauseMenu.Open();
            settingsMenu.Close();
            creditsMenu.Close();
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            paused = true;
        }

        /// <summary>
        /// Remet le jeu en "Play Mode". Le joueur peut à nouveau se déplacer et jouer.
        /// </summary>
        public void Resume()
        {
            playerInput.ActivateInput();
            pauseMenu.Close();
            settingsMenu.Close();
            creditsMenu.Close();
            menu.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }

        /// <summary>
        /// Ouvrir le menu pause et ferme les autres menus.
        /// </summary>
        public void OpenPause()
        {
            pauseMenu.Open();
            settingsMenu.Close();
            creditsMenu.Close();
        }

        /// <summary>
        /// Ouvrir le menu options et ferme les autres menus.
        /// </summary>
        public void OpenSettings()
        {
            pauseMenu.Close();
            settingsMenu.Open();
            creditsMenu.Close();
        }

        /// <summary>
        /// Ouvrir le menu crédits et ferme les autres menus.
        /// </summary>
        public void OpenCredits()
        {
            pauseMenu.Close();
            settingsMenu.Close();
            creditsMenu.Open();
        }

        /// <summary>
        /// Fonction qui permet au joueur de revenir au menu principal.
        /// </summary>
        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            paused = false;
            PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
            PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Scenes/UI/MainMenu");
        }
    }
}
