using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIChangeSelected : MonoBehaviour
{
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    public void ChangeSelected(GameObject selected)
    {
        eventSystem.SetSelectedGameObject(selected);
    }
}
