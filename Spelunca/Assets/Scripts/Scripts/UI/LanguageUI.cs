using System.Collections.Generic;
using Data;
using Language;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Permet de gérer les toggles pour le changement de langage dans les options.
    /// </summary>
    public class LanguageUI : MonoBehaviour
    {
        /// <value>
        /// Objet qui stocke les paramètre du joueur en temps réel.
        /// </value>
        public SettingsData settingsData;
        /// <value>
        /// Liste des toggles pour les langues.
        /// </value>
        public List<Toggle> toggles;
        /// <value>
        /// Liste des images des toggles lorsqu'ils ne sont pas sélectionnés.
        /// </value>
        public List<Sprite> sprites;
        /// <value>
        /// Liste des images des toggles lorsqu'ils sont sélectionnés.
        /// </value>
        public List<Sprite> spritesSelected;
        /// <value>
        /// Liste des identifiant des langues.
        /// </value>
        public List<string> languages;
        /// <summary>
        /// Référence vers le LanguageManager pour changer la langue du jeu.
        /// </summary>
        private LanguageManager languageManager;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        public void Start()
        {
            languageManager = FindObjectOfType<LanguageManager>();
            languageManager.ApplyLanguage(settingsData.language);
        }

        /// <summary>
        /// Fonction appelée lorsque l'object passe de "désactivé" à "activé".
        /// </summary>
        public void OnEnable()
        {
            UpdateUI();
        }

        /// <summary>
        /// Mets à jour l'interface en fonction des dernière valeurs sauvegardées.
        /// </summary>
        public void UpdateUI()
        {
            for (int i = 0; i < toggles.Count; i++)
            {
                if (languages[i] == settingsData.language)
                {
                    toggles[i].SetIsOnWithoutNotify(true);
                    toggles[i].gameObject.GetComponent<Image>().sprite = spritesSelected[i];
                }
                else
                {
                    toggles[i].SetIsOnWithoutNotify(false);
                    toggles[i].gameObject.GetComponent<Image>().sprite = sprites[i];
                }
            }
        }
    }
}
