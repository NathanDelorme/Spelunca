using UnityEngine;

/// <summary>
/// D�pr�ci�e : Cette classe ne doit plus �tre utilis�e.
/// 
/// Cette classe r�cup�re et interpr�te les inputs du joueur (touches appuy�es).
/// Cela permet de savoir ce que le joueur souhaite faire comme actions.
/// </summary>
public class InputController : MonoBehaviour
{
    /// <value>
    /// Cette propri�t� (<see cref="PlayerState"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour savoir ce que le joueur veut faire,
    /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Cette propri�t� (<see cref="MovementSettings"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour les mouvements (exemple : force du saut ou duration du dash).
    /// </value>
    public MovementSettings movementSettings;

    /// <summary>
    /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
    /// Ici on initialise les donn�es de l'attribut <c>playerState</c>.
    /// </summary>
    private void Start()
    {
        playerState.Initialize();
    }

    /// <summary>
    /// Fonction ex�cut� � chaque frame.
    /// On v�rifie si le joueur veut se d�placer, sauter ou dash.
    /// </summary>
    private void Update()
    {
        CheckMoveInput();
        CheckJumpInput();
        CheckDashInput();
    }

    /// <summary>
    /// Fonction qui d�tecte si le joueur veut bouger gr�ce aux touches press�es.
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
    /// Fonction qui d�tecte si le joueur veut sauter gr�ce aux touches press�es.
    /// </summary>
    private void CheckJumpInput()
    {
        playerState.wantToJump = Input.GetButtonDown("Jump") || Input.GetButton("Jump");
    }

    /// <summary>
    /// Fonction qui d�tecte si le joueur veut dash gr�ce aux touches press�es.
    /// </summary>
    private void CheckDashInput()
    {
        if (Input.GetButton("Dash"))
            playerState.wantToDash = true;
        else
            playerState.wantToDash = false;
    }
}
