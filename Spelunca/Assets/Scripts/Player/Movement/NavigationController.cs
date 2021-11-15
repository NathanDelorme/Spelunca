using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public PlayerState playerState;
    public MovementSettings movementSettings;
    public List<LayerMask> groundLayers;

    public Collider2D groundCheckCollider;
    private float coyoteTimeCounter;
    private float dashTimeBuffer = 0.5f;
    private float dashTimeBufferCounter;

    private void FixedUpdate()
    {
        CheckCanJump();
        CheckCanDash();
    }

    private void CheckCanJump()
    {
        if(CheckTouchingGround())
        {
            coyoteTimeCounter = movementSettings.maxCoyoteTime;
            playerState.linearDragType = PlayerState.DragType.GROUND;
            playerState.canJump = true;
            playerState.isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            playerState.linearDragType = PlayerState.DragType.AIR;
            playerState.canJump = playerState.isJumping == false && coyoteTimeCounter > 0f;
        }
    }

    private void CheckCanDash()
    {
        if (playerState.isDashing)
            dashTimeBufferCounter = dashTimeBuffer;

        if(CheckTouchingGround())
            playerState.canDash = playerState.currentDashTime <= 0f && dashTimeBufferCounter <= 0f;

        

        if (playerState.currentDashTime <= 0f)
            dashTimeBufferCounter -= Time.deltaTime;
    }

    private bool CheckTouchingGround()
    {
        foreach (LayerMask l in groundLayers)
        {
            if (groundCheckCollider.IsTouchingLayers(l))
                return true;
        }
        return false;
    }
}
