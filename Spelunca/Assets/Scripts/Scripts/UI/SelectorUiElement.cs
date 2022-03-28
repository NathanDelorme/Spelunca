using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Classe permettant de rendre le GameObject courant en GameObject s�lectionnable.
    /// </summary>
    public class SelectorUiElement : MonoBehaviour, IPointerEnterHandler
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
            eventSystem = FindObjectOfType<EventSystem>();
        }

        /// <summary>
        /// Si le cursuer ne s�lectionne pas de GameObject valide, alors il s�lectionne GameObject courrant.
        /// </summary>
        /// <param name="pointerEventData">Event</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventSystem.SetSelectedGameObject(this.gameObject);
        }
    }
}
