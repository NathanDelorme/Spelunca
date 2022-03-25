using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Cette classe récupère et interprète les inputs du joueur (touches appuyées).
/// Cela permet de savoir ce que le joueur souhaite faire comme actions.
/// </summary>
public class NewInputController : MonoBehaviour
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
    /// <value>
    /// Composant permettant de mettre le jeu en pause.
    /// </value>
    private PauseUI pauseUI;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// </summary>
    public void Start()
    {
        pauseUI = FindObjectOfType<PauseUI>();
        playerState.wantToMove = true;
    }

    /// <summary>
    /// Fonction exécuté lorsque le joueur appuis sur une touche de déplacement.
    /// </summary>
    /// <param name="context">Informations sur la touches appuyée</param>
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            if(context.ReadValue<Vector2>().x > 0f)
                playerState.facing = 1f;
            else
                playerState.facing = -1f;
        }
        playerState.horDir = context.ReadValue<Vector2>().x;
        playerState.verDir = context.ReadValue<Vector2>().y;
    }

    /// <summary>
    /// Fonction exécuté lorsque le joueur appuis sur une touche de saut.
    /// </summary>
    /// <param name="context">Informations sur la touches appuyée</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerState.wantToJump = true;
        }
        if (context.canceled)
        {
            playerState.wantToJump = false;
        }
    }

    /// <summary>
    /// Fonction exécuté lorsque le joueur appuis sur une touche de dash.
    /// </summary>
    /// <param name="context">Informations sur la touches appuyée</param>
    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            playerState.wantToDash = true;
        }
        if (context.canceled)
        {
            playerState.wantToDash = false;
        }
    }

    /// <summary>
    /// Fonction exécuté lorsque le joueur appuis sur une touche de mise en pause du jeu.
    /// </summary>
    /// <param name="context">Informations sur la touches appuyée</param>
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pauseUI.OnPause();
        }
    }
}
