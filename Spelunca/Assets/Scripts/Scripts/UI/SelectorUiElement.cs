using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Classe permettant de rendre le GameObject courant en GameObject sélectionnable.
    /// </summary>
    public class SelectorUiElement : MonoBehaviour, IPointerEnterHandler
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
            eventSystem = FindObjectOfType<EventSystem>();
        }

        /// <summary>
        /// Si le cursuer ne sélectionne pas de GameObject valide, alors il sélectionne GameObject courrant.
        /// </summary>
        /// <param name="pointerEventData">Event</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventSystem.SetSelectedGameObject(this.gameObject);
        }
    }
}
