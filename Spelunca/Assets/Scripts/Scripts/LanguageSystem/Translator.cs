using UnityEngine;
using TMPro;

namespace Language
{
    /// <summary>
    /// Classe permettant la traduction d'un texte basique.
    /// </summary>
    public class Translator : MonoBehaviour
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
        public void changeText(string text)
        {
            textComponent.text = text;
        }
    }
}