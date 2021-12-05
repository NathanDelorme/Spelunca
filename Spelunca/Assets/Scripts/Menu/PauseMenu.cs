using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    private float tempo = 0f;
    public GameObject menus;
    public EventSystem eventSystem;

    public void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused)
                Resume();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        menus.SetActive(true);
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        menus.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/UI/Main");
    }
}
