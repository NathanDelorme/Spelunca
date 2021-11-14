using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerState playerState;

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
    }

    private void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
            playerState.wantToJump = true;
        else
            playerState.wantToJump = false;
    }

    private void CheckDashInput()
    {
        if (Input.GetButtonDown("Dash"))
            playerState.wantToDash = true;
        else
            playerState.wantToDash = false;
    }
}
