using Language;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Classe permettant le fonctionnement du menu de sélection des niveaux.
    /// </summary>
    public class UILevelButton : MonoBehaviour
    {
        /// <value>
        /// Texte du bouton de lancement du niveau
        /// </value>
        private string text => GetComponentInChildren<TextMeshProUGUI>().text;
        /// <value>
        /// Bouton de sélection du niveau.
        /// </value>
        private Button button => GetComponentInChildren<Button>();
        /// <value>
        /// Zone de texte dédiée aux statistiques globales du joueur.
        /// </value>
        public StatsLevelTranslator stats;
        /// <value>
        /// Bouton de lancement du niveau
        /// </value>
        private SelectionButtonText buttonPlay => FindObjectOfType<SelectionButtonText>();

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        public void Start()
        {
            if (!PlayerPrefs.HasKey(Application.version + "Level" + text))
            {
                PlayerPrefs.SetInt(Application.version + "Level" + text, 0);
                if (text.Equals("1"))
                    PlayerPrefs.SetInt(Application.version + "Level" + text, 1);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Fonction appelée lorsque l'object passe de "désactivé" à "activé".
        /// </summary>
        public void OnEnable()
        {
            if (PlayerPrefs.GetInt(Application.version + "Level" + text) != 1)
                button.interactable = false;
            else
                button.interactable = true;
        }

        /// <summary>
        /// Ouvre le menu du niveau selectionné.
        /// </summary>
        public void LoadLevelSelection()
        {
            stats.levelID = Convert.ToInt16(text);
            stats.loadMenu();
            buttonPlay.levelID = Convert.ToInt16(text);
            buttonPlay.loadMenu();
        }

        /// <summary>
        /// Fonction qui lance le niveau.
        /// </summary>
        public void LoadLevel()
        {
            SceneManager.LoadScene("Scenes/Levels/Level" + text);
        }
    }
}
