using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Classe permettant de s�lectionner l'�l�ment de l'UI (User Interface) lors du changement entre deux menus.
    /// </summary>
    public class UIChangeSelected : MonoBehaviour
    {
        /// <value>
        /// Syst�me qui g�re les entr�es clavier/souris/manette du joueur.
        /// Permet de sp�cifier des param�tres pour la navigation dans les menus.
        /// </value>
        private EventSystem eventSystem;

        /// <summary>
        /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
        /// </summary>
        void Start()
        {
            eventSystem = GetComponent<EventSystem>();
        }

        /// <summary>
        /// Change la selection du curseur du joueur sur le GameObject "selected" si possible.
        /// </summary>
        /// <param name="selected">GameObject � s�lectionner.</param>
        public void ChangeSelected(GameObject selected)
        {
            eventSystem.SetSelectedGameObject(selected);
        }
    }
}
