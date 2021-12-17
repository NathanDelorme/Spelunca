using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
///  This class is used by the pause menu.
///  To see where the settings functionnality is stored, see <c>UIScript</c>.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <value>
    /// The <c>paused</c> property is a boolean.
    /// It's the value of if the game is paused or not.
    /// </value>
    private bool paused = false;
    /// <value>
    /// The <c>menuPause</c> property is a GameObject which contain the pause menu.
    /// </value>
    public GameObject menuPause;
    /// <value>
    /// The <c>settingsPause</c> property is a GameObject which contain the pause settings menu.
    /// </value>
    public GameObject settingsPause;
    /// <value>
    /// The <c>eventSystem</c> property is an EventSystem which allow us to control the menu with the controller.
    /// </value>
    public EventSystem eventSystem;

    /// <summary>
    /// Function executed at the start of the program.
    /// By default, the game is in the play mode.
    /// </summary>
    public void Start()
    {
        Resume();
    }

    /// <summary>
    /// Function executed each frame of the program.
    /// We check if the player want to open or close the pause menu.
    /// </summary>
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

    /// <summary>
    /// Function that pause the game by set the timeScale to 0f.
    /// </summary>
    private void PauseGame()
    {
        menuPause.SetActive(true);
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        Time.timeScale = 0f;
        paused = true;
    }

    /// <summary>
    /// Function that palyer the game by set the timeScale to 1f.
    /// </summary>
    public void Resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    /// <summary>
    /// Function that load the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/UI/Main");
    }
}
