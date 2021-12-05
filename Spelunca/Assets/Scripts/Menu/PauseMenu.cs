using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    public GameObject menuPause;
    public GameObject settingsPause;
    public EventSystem eventSystem;

    public void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused && menuPause.activeSelf)
                Resume();
            else if (!settingsPause.activeSelf && !menuPause.activeSelf)
                PauseGame();
        }
    }

    private void PauseGame()
    {
        menuPause.SetActive(true);
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/UI/Main");
    }
}
