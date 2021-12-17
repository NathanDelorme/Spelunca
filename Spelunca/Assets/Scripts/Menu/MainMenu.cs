using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  This class is used by the main menu for the interaction with the button "Jouer" and the button "Quitter".
///  To see where the settings functionnality is stored, see <c>UIScript</c>.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Function which load the last played scene.
    /// It looks if the PlayerPref "player_lastScene" has been initialized.
    /// If it is, we will just load the level with the same name than the stored value.
    /// Else we initialize it with the first level.
    /// </summary>
    public void Play()
    {
        if (!PlayerPrefs.HasKey("player_lastScene"))
            PlayerPrefs.SetString("player_lastScene", "Scenes/Tests/Nathan/Test_1");
        SceneManager.LoadScene(PlayerPrefs.GetString("player_lastScene"));
    }

    /// <summary>
    /// Function which quit the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
