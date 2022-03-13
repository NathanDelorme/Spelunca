using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionButtonText : MonoBehaviour
{
    private TextMeshProUGUI textComponent => GetComponentInChildren<TextMeshProUGUI>();
    private Button button => GetComponent<Button>();
    public StatsLevelTranslator statsText;
    public int levelID = -1;

    public void loadMenu()
    {
        if (PlayerPrefs.GetInt(Application.version + "Level" + levelID.ToString()) != 1)
            button.interactable = false;
        else
            button.interactable = true;
        textComponent.SetText(levelID.ToString());
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
        PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
        PlayerPrefs.Save();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level" + levelID.ToString());
    }

    public void ChangeLevelMenu()
    {
        if (levelID < 1)
            levelID = 20;
        else if (levelID > 20)
            levelID = 1;
        loadMenu();
        statsText.levelID = levelID;
        statsText.loadMenu();
    }

    public void NextLevel()
    {
        levelID += 1;
    }

    public void PreviousLevel()
    {
        levelID -= 1;
    }
}
