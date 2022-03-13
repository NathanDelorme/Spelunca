using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Button previousButton;
    public Button restartButton;
    public Button nextButton;
    private PlayerInput playerInput;
    private EventSystem eventSystem;
    private int levelID = -1;
    private string previousLevel = "";
    private string restartLevel = "";
    private string nextLevel = "";

    void Start()
    {
        levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
        restartLevel = SceneManager.GetActiveScene().name;

        if (levelID - 1 < 1)
            previousButton.interactable = false;
        else
            previousLevel = "Scenes/Levels/Level" + (levelID - 1).ToString();

        if (levelID + 1 > 20)
            nextLevel = "Scenes/UI/MainMenu";
        else
            nextLevel = "Scenes/Levels/Level" + (levelID + 1).ToString();
    }

    private void OnEnable()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        playerInput = FindObjectOfType<PlayerInput>();

        playerInput.DeactivateInput();
        Time.timeScale = 0f;
        eventSystem.SetSelectedGameObject(restartButton.gameObject);
    }

    public void PreviousLevel()
    {
        SceneManager.LoadScene(previousLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(restartLevel);
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
