using UnityEngine;

[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    public enum DragType
    {
        GROUND,
        AIR,
        WALL,
        NONE
    }

    [Header("General variables")]
    public DragType linearDragType = DragType.NONE;
    public float facing = 1f;
    public float horDir = 1f;
    public float verDir = 0f;

    [Header("Movement variables")]
    public bool canMove = true;
    public bool wantToMove = false;
    public bool isMoving = false;

    [Header("Jump variables")]
    public bool canJump = true;
    public bool wantToJump = false;
    public bool isJumping = false;

    [Header("Dash variables")]
    public bool canDash = true;
    public bool wantToDash = false;
    public bool isDashing = false;
    public float currentDashTime = 0f;

    [Header("Wall slide variables")]
    public bool canWallSlide = true;
    public int wallSlideSide = 0;
    public bool isWallSliding = false;

    [Header("Wall jump variables")]
    public bool canWallJump = false;
    public int wallJumpSide = 0;
    public bool isWallJumping = false;

    public void Initialize()
    {
        linearDragType = DragType.NONE;
        facing = 1f;
        horDir = 1f;
        verDir = 0f;

        canMove = true;
        wantToMove = false;
        isMoving = false;

        canJump = true;
        wantToJump = false;
        isJumping = false;

        canDash = true;
        wantToDash = false;
        isDashing = false;
        currentDashTime = 0f;

        canWallSlide = true;
        wallSlideSide = 0;
        isWallSliding = false;

        canWallJump = false;
        wallJumpSide = 0;
        isWallJumping = false;
    }
}
