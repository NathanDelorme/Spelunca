using UnityEngine;

/// <summary>
///  This class get and interpret the player's inputs. This inform about what the player want to do.
/// </summary>
public class InputController : MonoBehaviour
{
    /// <value>
    /// The <c>playerState</c> property is a ScriptableObject (structure implemented by Unity) which is an object with values shared between all scripts and scene that use it.
    /// playerState store all variables usefull to know what the player want to do, what he can do and what he is doing.
    /// </value>
    public PlayerState playerState;
    ///  <value>
    ///  The <c>movementSettings</c> property is a ScriptableObject (structure implemented by Unity) which is an object with values shared between all scripts and scene that use it.
    ///  movementSettings store all variables that imply the parameters of the movements (example : the jump force or the dash duration).
    ///  </value>
    public MovementSettings movementSettings;

    /// <summary>
    /// Function executed at the start of the program.
    /// We initialize the variable of the ScriptableObject <c>playerState</c>.
    /// </summary>
    private void Start()
    {
        playerState.Initialize();
    }

    /// <summary>
    /// Function executed each frame.
    /// Each frame we check if the player want to move, jump or dash.
    /// </summary>
    private void Update()
    {
        CheckMoveInput();
        CheckJumpInput();
        CheckDashInput();
    }

    /// <summary>
    /// Function that detect if the player want to move.
    /// We get if the keys assign to the movement are trigered.
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
    /// Function that detect if the player want to jump.
    /// We get if the keys assign to the jump are trigered.
    /// </summary>
    private void CheckJumpInput()
    {
        playerState.wantToJump = Input.GetButtonDown("Jump") || Input.GetButton("Jump");
    }

    /// <summary>
    /// Function that detect if the player want to dash.
    /// We get if the keys assign to the dash are trigered.
    /// </summary>
    private void CheckDashInput()
    {
        if (Input.GetButton("Dash"))
            playerState.wantToDash = true;
        else if (Input.GetButtonUp("Dash"))
            playerState.wantToDash = false;
    }
}
