using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerState playerState;
    public MovementSettings movementSettings;

    private void Start()
    {
        playerState.Initialize();
    }

    private void Update()
    {
        CheckMoveInput();
        CheckJumpInput();
        CheckDashInput();
    }

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

    private void CheckJumpInput()
    {
        playerState.wantToJump = Input.GetButtonDown("Jump") || Input.GetButton("Jump");
    }

    private void CheckDashInput()
    {
        if (Input.GetButton("Dash"))
            playerState.wantToDash = true;
        else if (Input.GetButtonUp("Dash"))
            playerState.wantToDash = false;
    }
}
