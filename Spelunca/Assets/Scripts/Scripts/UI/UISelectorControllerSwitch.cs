using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Classe permettant de récupérer le focus malgré le changement entre manette et clavier/souris pour la navigation dans les menus.
/// </summary>
public class UISelectorControllerSwitch : MonoBehaviour, IPointerClickHandler
{
    /// <value>
    /// Système qui gère les entrées clavier/souris/manette du joueur.
    /// Permet de spécifier des paramètres pour la navigation dans les menus.
    /// </value>
    private EventSystem eventSystem;
    /// <value>
    /// Dernier GameObject sélectionné par l'<c>eventSystem</c>.
    /// </value>
    private GameObject lastSelectedGO;

    /// <summary>
    /// Fonction appelé lorsque la fenêtre du jeu récupère le focus du joueur.
    /// </summary>
    /// <param name="focus">Vrai s'il y a le focus. Sinon Faux.</param>
    private void OnApplicationFocus(bool focus)
    {
        if(focus && lastSelectedGO != null)
            eventSystem.SetSelectedGameObject(lastSelectedGO);
    }

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// </summary>
    public void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    /// <summary>
    /// Change la selection du curseur du joueur sur le GameObject "selected" si possible.
    /// </summary>
    /// <param name="selected">GameObject à sélectionner.</param>
    public void ChangeSelected(GameObject selected)
    {
        eventSystem.SetSelectedGameObject(selected);
    }

    /// <summary>
    /// Fonction exécuté à chaque frame.
    /// </summary>
    public void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
            lastSelectedGO = eventSystem.currentSelectedGameObject;
    }

    /// <summary>
    /// Si le cursuer ne sélectionne pas de GameObject valide, alors il sélectionne le dernier GameObject valide qui a été selectionné.
    /// </summary>
    /// <param name="pointerEventData">Event</param>
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (eventSystem.currentSelectedGameObject == null)
            eventSystem.SetSelectedGameObject(lastSelectedGO);
    }
}
