using System.Collections.Generic;
using Data;
using Language;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Permet de g�rer les toggles pour le changement de langage dans les options.
    /// </summary>
    public class LanguageUI : MonoBehaviour
    {
        /// <value>
        /// Objet qui stocke les param�tre du joueur en temps r�el.
        /// </value>
        public SettingsData settingsData;
        /// <value>
        /// Liste des toggles pour les langues.
        /// </value>
        public List<Toggle> toggles;
        /// <value>
        /// Liste des images des toggles lorsqu'ils ne sont pas s�lectionn�s.
        /// </value>
        public List<Sprite> sprites;
        /// <value>
        /// Liste des images des toggles lorsqu'ils sont s�lectionn�s.
        /// </value>
        public List<Sprite> spritesSelected;
        /// <value>
        /// Liste des identifiant des langues.
        /// </value>
        public List<string> languages;
        /// <summary>
        /// R�f�rence vers le LanguageManager pour changer la langue du jeu.
        /// </summary>
        private LanguageManager languageManager;

        /// <summary>
        /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
        /// </summary>
        public void Start()
        {
            languageManager = FindObjectOfType<LanguageManager>();
            languageManager.ApplyLanguage(settingsData.language);
        }

        /// <summary>
        /// Fonction appel�e lorsque l'object passe de "d�sactiv�" � "activ�".
        /// </summary>
        public void OnEnable()
        {
            UpdateUI();
        }

        /// <summary>
        /// Mets � jour l'interface en fonction des derni�re valeurs sauvegard�es.
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
