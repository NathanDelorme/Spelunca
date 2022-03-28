using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Classe permettant de r�cup�rer le focus malgr� le changement entre manette et clavier/souris pour la navigation dans les menus.
    /// </summary>
    public class UISelectorControllerSwitch : MonoBehaviour, IPointerClickHandler
    {
        /// <value>
        /// Syst�me qui g�re les entr�es clavier/souris/manette du joueur.
        /// Permet de sp�cifier des param�tres pour la navigation dans les menus.
        /// </value>
        private EventSystem eventSystem;
        /// <value>
        /// Dernier GameObject s�lectionn� par l'<c>eventSystem</c>.
        /// </value>
        private GameObject lastSelectedGO;

        /// <summary>
        /// Fonction appel� lorsque la fen�tre du jeu r�cup�re le focus du joueur.
        /// </summary>
        /// <param name="focus">Vrai s'il y a le focus. Sinon Faux.</param>
        private void OnApplicationFocus(bool focus)
        {
            if (focus && lastSelectedGO != null)
                eventSystem.SetSelectedGameObject(lastSelectedGO);
        }

        /// <summary>
        /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
        /// </summary>
        public void Start()
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }

        /// <summary>
        /// Change la selection du curseur du joueur sur le GameObject "selected" si possible.
        /// </summary>
        /// <param name="selected">GameObject � s�lectionner.</param>
        public void ChangeSelected(GameObject selected)
        {
            eventSystem.SetSelectedGameObject(selected);
        }

        /// <summary>
        /// Fonction ex�cut� � chaque frame.
        /// </summary>
        public void Update()
        {
            if (eventSystem.currentSelectedGameObject != null)
                lastSelectedGO = eventSystem.currentSelectedGameObject;
        }

        /// <summary>
        /// Si le cursuer ne s�lectionne pas de GameObject valide, alors il s�lectionne le dernier GameObject valide qui a �t� selectionn�.
        /// </summary>
        /// <param name="pointerEventData">Event</param>
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (eventSystem.currentSelectedGameObject == null)
                eventSystem.SetSelectedGameObject(lastSelectedGO);
        }
    }
}