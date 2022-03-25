using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script qui g�re le menu de fin lorsque le joueur fini un niveau.
/// </summary>
public class EndUI : MonoBehaviour
{
    /// <value>
    /// Bouton pour lancer le niveau pr�c�dent.
    /// </value>
    public Button previousButton;
    /// <value>
    /// Bouton pour relancer le niveau courant.
    /// </value>
    public Button restartButton;
    /// <value>
    /// Bouton pour lancer le niveau suivant.
    /// </value>
    public Button nextButton;
    /// <value>
    /// R�cup�ration des inputs du joueur.
    /// </value>
    private PlayerInput playerInput;
    /// <value>
    /// Syst�me qui g�re les entr�es clavier/souris/manette du joueur.
    /// Permet de sp�cifier des param�tres pour la navigation dans les menus.
    /// </value>
    private EventSystem eventSystem;
    /// <value>
    /// Identifiant du niveau courant.
    /// </value>
    private int levelID = -1;
    /// <value>
    /// Nom du niveau pr�c�dent.
    /// </value>
    private string previousLevel = "";
    /// <value>
    /// Nom du niveau courant.
    /// </value>
    private string restartLevel = "";
    /// <value>
    /// Nom du niveau suivant.
    /// </value>
    private string nextLevel = "";

    /// <summary>
    /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
    /// </summary>
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

    /// <summary>
    /// Fonction appel�e lorsque l'object passe de "d�sactiv�" � "activ�".
    /// </summary>
    private void OnEnable()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        playerInput = FindObjectOfType<PlayerInput>();

        playerInput.DeactivateInput();
        Time.timeScale = 0f;
        eventSystem.SetSelectedGameObject(restartButton.gameObject);
    }

    /// <summary>
    /// Fonction qui lance le niveau pr�c�dent.
    /// </summary>
    public void PreviousLevel()
    {
        SceneManager.LoadScene(previousLevel);
    }

    /// <summary>
    /// Fonction qui relance le niveau.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(restartLevel);
    }

    /// <summary>
    /// Fonction qui lance le niveau suivant.
    /// </summary>
    public void NextLevel()
    {
        if(levelID == 20)
            nextLevel = "Scenes/History/End";
        SceneManager.LoadScene(nextLevel);
    }
}
