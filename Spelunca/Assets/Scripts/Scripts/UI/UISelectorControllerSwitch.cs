using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectorControllerSwitch : MonoBehaviour, IPointerClickHandler
{
    private EventSystem eventSystem;
    private GameObject lastSelectedGO;

    private void OnApplicationFocus(bool focus)
    {
        if(focus && lastSelectedGO != null)
            eventSystem.SetSelectedGameObject(lastSelectedGO);
    }
    public void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void ChangeSelected(GameObject selected)
    {
        eventSystem.SetSelectedGameObject(selected);
    }

    public void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
            lastSelectedGO = eventSystem.currentSelectedGameObject;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (eventSystem.currentSelectedGameObject == null)
            eventSystem.SetSelectedGameObject(lastSelectedGO);
    }
}
