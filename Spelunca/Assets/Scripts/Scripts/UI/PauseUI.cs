using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private bool paused = false;
    private UIManager uiManager;
    public UI.UIScript pauseMenu;
    public UI.UIScript settingsMenu;
    public UI.UIScript creditsMenu;
    public GameObject menu;
    public EventSystem eventSystem;

    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        Resume();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused && menu.activeSelf)
                Resume();
            else if (paused == false)
                PauseGame();
        }
    }

    private void PauseGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        pauseMenu.Open();
        settingsMenu.Close();
        creditsMenu.Close();
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        paused = true;
    }

    public void Resume()
    {
        pauseMenu.Close();
        settingsMenu.Close();
        creditsMenu.Close();
        menu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void OpenPause()
    {
        pauseMenu.Open();
        settingsMenu.Close();
        creditsMenu.Close();
    }

    public void OpenSettings()
    {
        pauseMenu.Close();
        settingsMenu.Open();
        creditsMenu.Close();
    }

    public void OpenCredits()
    {
        pauseMenu.Close();
        settingsMenu.Close();
        creditsMenu.Open();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene("Scenes/UI/MainMenu");
    }
}
