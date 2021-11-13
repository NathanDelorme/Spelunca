using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public PlayerState playerState;
    public MovementSettings movementSettings;
    public LayerMask groundLayer;

    public Collider2D groundCheckCollider;
    private float coyoteTimeCounter;

    private void FixedUpdate()
    {
        CheckCanJump();
    }

    private void CheckCanJump()
    {
        if(groundCheckCollider.IsTouchingLayers(groundLayer))
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
}
