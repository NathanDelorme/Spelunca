using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Classe permettant le fonctionnement du menu de s�lection des niveaux.
/// </summary>
public class UILevelButton : MonoBehaviour
{
    /// <value>
    /// Texte du bouton de lancement du niveau
    /// </value>
    private string text => GetComponentInChildren<TextMeshProUGUI>().text;
    /// <value>
    /// Bouton de s�lection du niveau.
    /// </value>
    private Button button => GetComponentInChildren<Button>();
    /// <value>
    /// Zone de texte d�di�e aux statistiques globales du joueur.
    /// </value>
    public StatsLevelTranslator stats;
    /// <value>
    /// Bouton de lancement du niveau
    /// </value>
    private SelectionButtonText buttonPlay => FindObjectOfType<SelectionButtonText>();

    /// <summary>
    /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
    /// </summary>
    public void Start()
    {
        if (!PlayerPrefs.HasKey(Application.version + "Level" + text))
        {
            PlayerPrefs.SetInt(Application.version + "Level" + text, 0);
            if (text.Equals("1"))
                PlayerPrefs.SetInt(Application.version + "Level" + text, 1);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Fonction appel�e lorsque l'object passe de "d�sactiv�" � "activ�".
    /// </summary>
    public void OnEnable()
    {
        if (PlayerPrefs.GetInt(Application.version + "Level" + text) != 1)
            button.interactable = false;
        else
            button.interactable = true;
    }

    /// <summary>
    /// Ouvre le menu du niveau selectionn�.
    /// </summary>
    public void LoadLevelSelection()
    {
        stats.levelID = Convert.ToInt16(text);
        stats.loadMenu();
        buttonPlay.levelID = Convert.ToInt16(text);
        buttonPlay.loadMenu();
    }

    /// <summary>
    /// Fonction qui lance le niveau.
    /// </summary>
    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level"+ text);
    }
}
