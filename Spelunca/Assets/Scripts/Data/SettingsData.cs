using UnityEngine;

namespace Data
{
    /// <summary>
    /// Permet de stocker paramètres du joueur.
    /// Les paramètres sont modifiés en temp réel lors de l'exécution du programme.
    /// </summary>
    public class SettingsData : MonoBehaviour
    {
        /// <value>
        /// Stocke la langue dans laquelle le joueur veut jouer.
        /// </value>
        public string language = "EN";

        /// <value>
        /// Stocke le volume de la musique du jeu.
        /// </value>
        public float musicVolume = 0.5f;
        /// <value>
        /// Stocke le volume des effets sonores du jeu. Exemple : mort du personnage, bruit de pas, etc...
        /// </value>
        public float sfxVolume = 0.5f;

        /// <value>
        /// Stocke si le joueur souhaite jouer en plein écran ou non.
        /// </value>
        public bool isFullscreen = true;
        /// <value>
        /// Stocke la largeur de la résolution sélectionné par le joueur.
        /// </value>
        public int resolutionWidth = 1920;
        /// <value>
        /// Stocke la hauteur de la résolution sélectionné par le joueur.
        /// </value>
        public int resolutionheight = 1080;
    }
}
