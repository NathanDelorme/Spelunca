using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

namespace Language
{
    /// <summary>
    /// Classe permettant la traduction d'un texte affichant le niveau dans lequel le joueur ce trouve.
    /// </summary>
    public class LevelTranslator : MonoBehaviour
    {
        /// <value>
        /// Identifiant du texte que l'on recupère dans le fichier XML.
        /// </value>
        public string textId;

        /// <value>
        /// Component qui affiche le texte au joueur.
        /// </value>
        private TextMeshProUGUI textComponent => GetComponent<TextMeshProUGUI>();

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        void Start()
        {
            if (!textComponent)
                Debug.LogError("Translator - textComponent is null.");
        }

        /// <summary>
        /// Permet de changer le texte du composant par la traduction souhaitée.
        /// </summary>
        /// <param name="translatedText">Texte à afficher.</param>
        public void changeText(string translatedText)
        {
            int levelNb = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
            textComponent.SetText(translatedText + " " + levelNb);
        }
    }
}