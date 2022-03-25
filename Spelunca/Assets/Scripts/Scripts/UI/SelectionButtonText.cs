using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Classe permettant le fonctionnement du menu qui s'ouvre apr�s la s�lection d'un niveau dans la liste des niveaux.
/// </summary>
public class SelectionButtonText : MonoBehaviour
{
    /// <value>
    /// Texte du bouton de lancement du niveau.
    /// </value>
    private TextMeshProUGUI textComponent => GetComponentInChildren<TextMeshProUGUI>();
    /// <value>
    /// Bouton de lancement du niveau
    /// </value>
    private Button button => GetComponent<Button>();
    /// <value>
    /// Texte qui affiche les statistique du niveau s�lectionn�.
    /// </value>
    public StatsLevelTranslator statsText;
    /// <value>
    /// Identifiant du niveau s�lectionn�.
    /// </value>
    public int levelID = -1;

    /// <summary>
    /// Fonction appel� lorsque le menu est charg� o� que l'on change de niveau s�lectionn�.
    /// </summary>
    public void loadMenu()
    {
        if (PlayerPrefs.GetInt(Application.version + "Level" + levelID.ToString()) != 1)
            button.interactable = false;
        else
            button.interactable = true;
        textComponent.SetText(levelID.ToString());
    }

    /// <summary>
    /// Fonction appel� juste avant la fermeture de l'application.
    /// </summary>
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(Application.version + "IS_PLAYING_RUN", 0);
        PlayerPrefs.SetFloat(Application.version + "ALL_CURRENTRUN", 0f);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Ouvre le niveau s�lectionn�.
    /// </summary>
    public void LoadLevel()
    {
        if(levelID == 1)
            SceneManager.LoadScene("Scenes/History/Start");
        else
            SceneManager.LoadScene("Scenes/Levels/Level" + levelID.ToString());
    }

    /// <summary>
    /// Permet le changement de s�lection du niveau.
    /// Si l'on appuis sur le bouton pour prendre le niveau pr�c�dent, cette fonction s'assure de rester dans l'interval de niveaux.
    /// </summary>
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

    /// <summary>
    /// Incr�mente de 1 l'identifiant du niveau s�lectionn�.
    /// </summary>
    public void NextLevel()
    {
        levelID += 1;
    }

    /// <summary>
    /// D�cr�mente de 1 l'identifiant du niveau s�lectionn�.
    /// </summary>
    public void PreviousLevel()
    {
        levelID -= 1;
    }
}
