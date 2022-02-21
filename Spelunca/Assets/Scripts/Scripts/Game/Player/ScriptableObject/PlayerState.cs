using UnityEngine;

/// <summary>
///  This class is an ScriptableObject that contain all variables useful for getting the state of the player in real time.
/// </summary>
[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    /// <summary>
    /// Enumeration of the possible player state in the level.
    /// </summary>
    public enum DragType
    {
        GROUND,
        AIR,
        WALL,
        NONE
    }

    public enum Ability
    {
        SPIKE,
        CRYSTAL,
        NONE
    }

    [Header("General variables")]
    public Ability AbilityType = Ability.NONE;
    /// <value>
    /// Friction type to apply to the player.
    /// </value>
    public DragType linearDragType = DragType.NONE;
    /// <value>
    /// Face were the player look.
    /// 1f : player look on the right
    /// -1f : player look on the left
    /// </value>
    public float facing = 1f;
    /// <value>
    /// Face were the player look.
    /// Higher than 0 : movement go to the rigth
    /// Lower than 0 : movement go to the left
    /// 0f: No movement
    /// </value>
    public float horDir = 0f;
    /// <value>
    /// Face were the player look.
    /// higher than 0 : go up
    /// lower than 0 : go done
    /// 0 : Nothing
    /// </value>
    public float verDir = 0f;

    [Header("Movement variables")]
    /// <value>
    /// Boolean that define if the player can move.
    /// See <c>NavigationController</c> to know when the player can move.
    /// </value>
    public bool canMove = true;
    /// <value>
    /// Boolean that define if the user want to move the player.
    /// See <c>InputController</c> to understand the management of player input.
    /// </value>
    public bool wantToMove = true;
    /// <value>
    /// Boolean that is enable if the player is moving.
    /// </value>
    public bool isMoving = false;

    [Header("Jump variables")]
    /// <value>
    /// Boolean that define if the player can jump.
    /// See <c>NavigationController</c> to know when the player can jump.
    /// </value>
    public bool canJump = true;
    /// <value>
    /// Boolean that define if the user want to jump.
    /// See <c>InputController</c> to understand the management of player input.
    /// </value>
    public bool wantToJump = false;
    /// <value>
    /// Boolean that is enable if the player is jumping.
    /// </value>
    public bool isJumping = false;

    [Header("Dash variables")]
    /// <value>
    /// Boolean that define if the player can dash.
    /// See <c>NavigationController</c> to know when the player can dash.
    /// </value>
    public bool canDash = true;
    /// <value>
    /// Boolean that define if the user want to dash.
    /// See <c>InputController</c> to understand the management of player input.
    /// </value>
    public bool wantToDash = false;
    /// <value>
    /// Boolean that is enable if the player is dashing.
    /// </value>
    public bool isDashing = false;
    /// <value>
    /// float which is a time counter for the dash.
    /// </value>
    public float currentDashTime = 0f;

    [Header("Wall slide variables")]
    /// <value>
    /// Boolean that define if the player can wall slide.
    /// See <c>NavigationController</c> to know when the player can wall slide.
    /// </value>
    public bool canWallSlide = true;
    /// <value>
    /// Integer that define if the user want to wall slide and the side of the wall slide.
    /// 0 : no wall slide
    /// 1 : wall slide on the right
    /// -1 : wall slide ont he left
    /// 2 : wall slide on both side.
    /// See <c>InputController</c> to understand the management of player input.
    /// </value>
    public int wallSlideSide = 0;
    /// <value>
    /// Boolean that is enable if the player is wall sliding.
    /// </value>
    public bool isWallSliding = false;

    [Header("Wall jump variables")]
    /// <value>
    /// Boolean that define if the player can wall jump.
    /// See <c>NavigationController</c> to know when the player can wall jump.
    /// </value>
    public bool canWallJump = false;
    /// <value>
    /// Integer that define if the user want to wall jump and the side of the wall jump.
    /// 0 : no wall slide
    /// 1 : wall jump on the right
    /// -1 : wall jump ont the left
    /// See <c>InputController</c> to understand the management of player input.
    /// </value>
    public int wallJumpSide = 0;
    /// <value>
    /// Boolean that is enable if the player is wall jumping.
    /// </value>
    public bool isWallJumping = false;

    /// <summary>
    /// Function which initialise the variabkes of this ScriptableObject.
    /// </summary>
    public void Initialize()
    {
        AbilityType = Ability.NONE;
        linearDragType = DragType.NONE;
        facing = 1f;
        horDir = 0f;
        verDir = 0f;

        canMove = true;
        wantToMove = true;
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
