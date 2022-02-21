using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputController : MonoBehaviour
{
    public PlayerState playerState;
    public MovementSettings movementSettings;

    private PauseUI pauseUI;

    public void Start()
    {
        pauseUI = FindObjectOfType<PauseUI>();
        playerState.wantToMove = true;
    }

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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pauseUI.OnPause();
        }
    }
}
