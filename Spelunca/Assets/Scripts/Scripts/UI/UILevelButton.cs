using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelButton : MonoBehaviour
{
    private string text => GetComponentInChildren<TextMeshProUGUI>().text;
    private Button button => GetComponentInChildren<Button>();
    private StatsLevelTranslator stats => FindObjectOfType<StatsLevelTranslator>();
    private SelectionButtonText buttonPlay => FindObjectOfType<SelectionButtonText>();

    public void Start()
    {
        if (!PlayerPrefs.HasKey("Level" + text))
        {
            PlayerPrefs.SetInt("Level" + text, 0);
            if (text.Equals("1"))
                PlayerPrefs.SetInt("Level" + text, 1);
            PlayerPrefs.Save();
        }
    }

    public void OnEnable()
    {
        if (PlayerPrefs.GetInt("Level" + text) != 1)
            button.interactable = false;
        else
            button.interactable = true;
    }

    public void LoadLevelSelection()
    {
        stats.levelID = Convert.ToInt16(text);
        stats.loadMenu();
        buttonPlay.levelID = Convert.ToInt16(text);
        buttonPlay.loadMenu();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level"+ text);
    }
}
