using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Applique un mouvement à une layer afin de créer une sensation de profondeur.
    /// </summary>
    public class Parallax : MonoBehaviour
    {
        /// <value>
        /// Intensité de l'effet de parallax appliqué au layer.
        /// </value>
        public float parallaxEffect;
        /// <value>
        /// Stocke la position et la rotation de la caméra dans la scène.
        /// </value>
        public Transform cameraTransform;
        /// <value>
        /// Stocke la position de la camera lors de la dernière frame.
        /// </value>
        private Vector3 lastCamPos;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
        /// </summary>
        void Start()
        {
            lastCamPos = cameraTransform.position;
        }

        /// <summary>
        /// Fonction exécuté à chaque frame.
        /// On met à jour la position du layer en fonction de la position et du mouvement de la camera.
        /// </summary>
        void LateUpdate()
        {
            Vector3 movement = new Vector3(cameraTransform.position.x - lastCamPos.x, 0f);
            transform.position = new Vector3(transform.position.x + movement.x * parallaxEffect, cameraTransform.position.y);
            lastCamPos = cameraTransform.position;
        }
    }
}
