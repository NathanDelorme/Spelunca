using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionButtonText : MonoBehaviour
{
    private TextMeshProUGUI textComponent => GetComponentInChildren<TextMeshProUGUI>();
    public int levelID = -1;

    public void loadMenu()
    {
        textComponent.SetText(levelID.ToString());
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level" + levelID.ToString());
    }
}
