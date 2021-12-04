using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        if (!PlayerPrefs.HasKey("player_lastScene"))
            PlayerPrefs.SetString("player_lastScene", "Scenes/Tests/Nathan/Test_1");
        SceneManager.LoadScene(PlayerPrefs.GetString("player_lastScene"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
