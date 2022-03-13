using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
    private SFXManager sfxManager;
    private PlayerInput playerInput;

    public void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        playerInput = FindObjectOfType<PlayerInput>();
        uiManager = FindObjectOfType<UIManager>();
        Resume();
    }

    public void OnPause()
    {
        sfxManager.flipFlopPause();
        if (paused && menu.activeSelf)
            Resume();
        else if (paused == false)
            PauseGame();
    }

    private void PauseGame()
    {
        playerInput.DeactivateInput();
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
        playerInput.ActivateInput();
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
        PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
        PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scenes/UI/MainMenu");
    }
}
