using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorUiElement : MonoBehaviour, IPointerEnterHandler
{
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
