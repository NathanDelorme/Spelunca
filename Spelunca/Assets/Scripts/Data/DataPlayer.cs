using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Permet de stocker les statistiques basiques du joueur. On parle donc du nombre de saut et du nombre de mort du joueur.
/// </summary>
public class DataPlayer : MonoBehaviour
{
    /// <value>
    /// Stocke si le joueur a été tué à la frame précédente. Permet d'éviter de compter plusieurs fois une mort.
    /// </value>
    private bool isKilledOld = false;
    /// <value>
    /// Stocke si le joueur a sauté à la frame précédente. Permet d'éviter de compter plusieurs fois un saut.
    /// </value>
    private bool isJumpingOld = false;
    /// <value>
    /// Stocke si le joueur a effectué un saut mural à la frame précédente. Permet d'éviter de compter plusieurs fois un saut mural.
    /// </value>
    private bool IsWallJumpingOld = false;
    /// <value>
    /// Nombre de mort total.
    /// </value>
    public int deathTotalNb = 0;
    /// <value>
    /// Nombre de saut et de saut mural total.
    /// </value>
    public int jumpTotalNb = 0;
    /// <value>
    /// Référence à l'état du joueur <see cref="PlayerState"/>, mis à jour en temps réel.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Référence au script qui s'occupe des conditions de mort et de victoire du joueur. <see cref="WinDeathCondition"/>
    /// </value>
    public WinDeathCondition winDeathCondition;
    /// <value>
    /// Contient l'ID du niveau (nombre entier entre 1 et 20).
    /// </value>
    private int levelID = -1;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à <see cref="Update"/>.
    /// Cette fonction agit comme un constructeur permettant d'initialiser les données et effectuer des actions au chargement du script.
    /// </summary>
    void Start()
    {
        levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
        deathTotalNb = PlayerPrefs.GetInt(Application.version + "LEVEL_DEATHS" + levelID);
        jumpTotalNb = PlayerPrefs.GetInt(Application.version + "LEVEL_JUMP" + levelID);
    }

    /// <summary>
    /// Fonction exécuté à chaque frame.
    /// Vérifie les conditions du joueur grâce aux attributs playerState et winDeathCondition,
    /// afin de déterminer si l'une des statistiques doit-être incrémenté.
    /// </summary>
    void Update()
    {
        if(isKilledOld == false && winDeathCondition.isKilled)
        {
            isKilledOld = true;
            deathTotalNb++;
            PlayerPrefs.SetInt(Application.version + "LEVEL_DEATHS" + levelID, deathTotalNb);
        }
        if (winDeathCondition.isKilled == false)
            isKilledOld = false;

        if (isJumpingOld == false && playerState.isJumping == true)
        {
            isJumpingOld = true;
            jumpTotalNb++;
            PlayerPrefs.SetInt(Application.version + "LEVEL_JUMP" + levelID, jumpTotalNb);
        }
        if (playerState.isJumping == false)
            isJumpingOld = false;

        if (IsWallJumpingOld == false && playerState.isWallJumping == true)
        {
            IsWallJumpingOld = true;
            jumpTotalNb++;
            PlayerPrefs.SetInt(Application.version + "LEVEL_JUMP" + levelID, jumpTotalNb);
        }
        if (playerState.isWallJumping == false)
            IsWallJumpingOld = false;
    }
}
