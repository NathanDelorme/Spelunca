using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Classe permettant de sélectionner l'élément de l'UI (User Interface) lors du changement entre deux menus.
    /// </summary>
    public class UIChangeSelected : MonoBehaviour
    {
        /// <value>
        /// Système qui gère les entrées clavier/souris/manette du joueur.
        /// Permet de spécifier des paramètres pour la navigation dans les menus.
        /// </value>
        private EventSystem eventSystem;

        /// <summary>
        /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
        /// </summary>
        void Start()
        {
            eventSystem = GetComponent<EventSystem>();
        }

        /// <summary>
        /// Change la selection du curseur du joueur sur le GameObject "selected" si possible.
        /// </summary>
        /// <param name="selected">GameObject à sélectionner.</param>
        public void ChangeSelected(GameObject selected)
        {
            eventSystem.SetSelectedGameObject(selected);
        }
    }
}
