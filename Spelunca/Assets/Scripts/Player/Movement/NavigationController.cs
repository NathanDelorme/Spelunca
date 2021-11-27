using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class check what the player can do. It's independent from what he want to do.
/// </summary>
public class NavigationController : MonoBehaviour
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
    ///  <value>
    ///  The <c>_rigidBody</c> property is a RigidBody2D which allow us to get and give physics to a <c>GameObject</c>.
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// The <c>groundLayers</c> property is a list that contains all layers that need to be interpreted as the ground.
    /// </value>
    public List<LayerMask> groundLayers;
    /// <value>
    /// The <c>groundCheckCollider</c> property is a collider2D. It's an hitbox which is usefull to detect when the player is on the ground.
    /// </value>
    public Collider2D groundCheckCollider;
    /// <value>
    /// The <c>wallLeftCheckCollider</c> property is a collider2D. It's an hitbox which is usefull to detect when the player is close to the wall on the left.
    /// </value>
    public Collider2D wallLeftCheckCollider;
    /// <value>
    /// The <c>wallRightCheckCollider</c> property is a collider2D. It's an hitbox which is usefull to detect when the player is close to the wall on the left.
    /// </value>
    public Collider2D wallRightCheckCollider;


    /// <value>
    /// The <c>_coyoteTimeCounter</c> property is a float used as a time counter.
    /// </value>
    private float _coyoteTimeCounter;
    /// <value>
    /// The <c>_dashTimeBuffer</c> property is a float used as a cooldown max time before being able to dash after a dash..
    /// </value>
    private float _dashTimeBuffer = 0.5f;
    /// <value>
    /// The <c>_dashTimeBufferCounter</c> property is a float used as a cooldown counter before being able to dash after a dash..
    /// </value>
    private float _dashTimeBufferCounter;

    // <summary>
    /// Function executed at the start of the program.
    /// Used to get component (<c>_rigidBody</c>) from the parent of the current <c>GameObject</c>.
    /// </summary>
    private void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
    }

    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we check if the player want to move, jump or dash.
    /// </summary>
    private void FixedUpdate()
    {
        CheckCanJump();
        CheckCanDash();
        CheckCanWallSlide();
    }

    /// <summary>
    /// Function that detect if the player can jump.
    /// </summary>
    private void CheckCanJump()
    {
        if(CheckTouchingGround())
        {
            _coyoteTimeCounter = movementSettings.maxCoyoteTime;
            playerState.linearDragType = PlayerState.DragType.GROUND;
            playerState.canJump = true;
            playerState.isJumping = false;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            playerState.linearDragType = PlayerState.DragType.AIR;
            playerState.canJump = playerState.isJumping == false && _coyoteTimeCounter > 0f;
        }
    }

    /// <summary>
    /// Function that detect if the player can dash.
    /// </summary>
    private void CheckCanDash()
    {
        if (playerState.isDashing)
            _dashTimeBufferCounter = _dashTimeBuffer;

        if(CheckTouchingGround())
            playerState.canDash = playerState.currentDashTime <= 0f && _dashTimeBufferCounter <= 0f;

        

        if (playerState.currentDashTime <= 0f)
            _dashTimeBufferCounter -= Time.deltaTime;
    }

    /// <summary>
    /// Function that detect if the player can wall slide.
    /// </summary>
    private void CheckCanWallSlide()
    {
        playerState.canWallSlide = !CheckTouchingGround() && CheckTouchingWall() && _rigidBody.velocity.y < -0.1f;
        if (!playerState.canWallSlide)
            playerState.wallSlideSide = -1;
        else
        {
            if (wallLeftCheckCollider.IsTouchingLayers(groundLayers[0]) && wallRightCheckCollider.IsTouchingLayers(groundLayers[0]))
                playerState.wallSlideSide = 3;
            else if (wallLeftCheckCollider.IsTouchingLayers(groundLayers[0]))
                playerState.wallSlideSide = 1;
            else
                playerState.wallSlideSide = 2;
        }
    }

    /// <summary>
    /// Function that detect if the player is on the ground.
    /// </summary>
    /// <returns>
    /// True if is on the ground, else false.
    /// </returns>
    private bool CheckTouchingGround()
    {
        foreach (LayerMask l in groundLayers)
        {
            if (groundCheckCollider.IsTouchingLayers(l))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Function that detect if the player is close to the wall to slide.
    /// </summary>
    /// <returns>
    /// True if is close to the ground, else false.
    /// </returns>
    private bool CheckTouchingWall()
    {
        if (wallLeftCheckCollider.IsTouchingLayers(groundLayers[0]) || wallRightCheckCollider.IsTouchingLayers(groundLayers[0]))
            return true;
        return false;
    }
}
