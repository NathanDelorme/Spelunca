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

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level"+ text);
    }
}
