using UnityEngine;

/// <summary>
/// Dépréciée : Cette classe ne doit plus être utilisée.
/// 
/// Cette classe récupère et interprète les inputs du joueur (touches appuyées).
/// Cela permet de savoir ce que le joueur souhaite faire comme actions.
/// </summary>
public class InputController : MonoBehaviour
{
    /// <value>
    /// Cette propriété (<see cref="PlayerState"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour savoir ce que le joueur veut faire,
    /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Cette propriété (<see cref="MovementSettings"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour les mouvements (exemple : force du saut ou duration du dash).
    /// </value>
    public MovementSettings movementSettings;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// Ici on initialise les données de l'attribut <c>playerState</c>.
    /// </summary>
    private void Start()
    {
        playerState.Initialize();
    }

    /// <summary>
    /// Fonction exécuté à chaque frame.
    /// On vérifie si le joueur veut se déplacer, sauter ou dash.
    /// </summary>
    private void Update()
    {
        CheckMoveInput();
        CheckJumpInput();
        CheckDashInput();
    }

    /// <summary>
    /// Fonction qui détecte si le joueur veut bouger grâce aux touches pressées.
    /// </summary>
    private void CheckMoveInput()
    {
        playerState.wantToMove = Input.GetAxisRaw("Horizontal") != 0f;
        playerState.horDir = Input.GetAxisRaw("Horizontal");
        playerState.verDir = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") > 0f)
            playerState.facing = 1f;
        if (Input.GetAxisRaw("Horizontal") < -0f)
            playerState.facing = -1f;
    }

    /// <summary>
    /// Fonction qui détecte si le joueur veut sauter grâce aux touches pressées.
    /// </summary>
    private void CheckJumpInput()
    {
        playerState.wantToJump = Input.GetButtonDown("Jump") || Input.GetButton("Jump");
    }

    /// <summary>
    /// Fonction qui détecte si le joueur veut dash grâce aux touches pressées.
    /// </summary>
    private void CheckDashInput()
    {
        if (Input.GetButton("Dash"))
            playerState.wantToDash = true;
        else
            playerState.wantToDash = false;
    }
}
