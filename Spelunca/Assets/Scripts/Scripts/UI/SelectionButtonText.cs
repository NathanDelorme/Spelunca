using Language;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Classe permettant le fonctionnement du menu qui s'ouvre après la sélection d'un niveau dans la liste des niveaux.
    /// </summary>
    public class SelectionButtonText : MonoBehaviour
    {
        /// <value>
        /// Texte du bouton de lancement du niveau.
        /// </value>
        private TextMeshProUGUI textComponent => GetComponentInChildren<TextMeshProUGUI>();
        /// <value>
        /// Bouton de lancement du niveau
        /// </value>
        private Button button => GetComponent<Button>();
        /// <value>
        /// Texte qui affiche les statistique du niveau sélectionné.
        /// </value>
        public StatsLevelTranslator statsText;
        /// <value>
        /// Identifiant du niveau sélectionné.
        /// </value>
        public int levelID = -1;

        /// <summary>
        /// Fonction appelé lorsque le menu est chargé où que l'on change de niveau sélectionné.
        /// </summary>
        public void loadMenu()
        {
            if (PlayerPrefs.GetInt(Application.version + "Level" + levelID.ToString()) != 1)
                button.interactable = false;
            else
                button.interactable = true;
            textComponent.SetText(levelID.ToString());
        }

        /// <summary>
        /// Fonction appelé juste avant la fermeture de l'application.
        /// </summary>
        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
            PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Ouvre le niveau sélectionné.
        /// </summary>
        public void LoadLevel()
        {
            if (levelID == 1)
                SceneManager.LoadScene("Scenes/History/Start");
            else
                SceneManager.LoadScene("Scenes/Levels/Level" + levelID.ToString());
        }

        /// <summary>
        /// Permet le changement de sélection du niveau.
        /// Si l'on appuis sur le bouton pour prendre le niveau précédent, cette fonction s'assure de rester dans l'interval de niveaux.
        /// </summary>
        public void ChangeLevelMenu()
        {
            if (levelID < 1)
                levelID = 20;
            else if (levelID > 20)
                levelID = 1;
            loadMenu();
            statsText.levelID = levelID;
            statsText.loadMenu();
        }

        /// <summary>
        /// Incrémente de 1 l'identifiant du niveau sélectionné.
        /// </summary>
        public void NextLevel()
        {
            levelID += 1;
        }

        /// <summary>
        /// Décrémente de 1 l'identifiant du niveau sélectionné.
        /// </summary>
        public void PreviousLevel()
        {
            levelID -= 1;
        }
    }
}