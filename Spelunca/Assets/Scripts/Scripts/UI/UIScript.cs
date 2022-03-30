using UnityEngine;

namespace UI
{
    /// <summary>
    /// Permet la transition entre les différents menus du jeu.
    /// </summary>
    public class UIScript : MonoBehaviour
    {
        /// <value>
        /// Type de menu du GameObject courant.
        /// </value>
        public UIType UIType;
        /// <value>
        /// GameObject du menu.
        /// </value>
        private GameObject obj;

        /// <summary>
        /// Fonction appelé à chaque fois que le script est chargé.
        /// </summary>
        private void Awake()
        {
            obj = this.gameObject;
        }

        /// <summary>
        /// Active le menu que l'on souhaite afficher.
        /// </summary>
        public void Open()
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
        }

        /// <summary>
        /// Désactive le menu qu'on ne souhaite pas afficher.
        /// </summary>
        public void Close()
        {
            if (obj.activeSelf)
                obj.SetActive(false);
        }

        /// <summary>
        /// Renvoi si le menu est ouvert ou non.
        /// </summary>
        /// <returns>Vrai si ouvert, sinon Faux.</returns>
        public bool IsOpen()
        {
            return obj.activeSelf;
        }
    }
}
