using UnityEngine;

/// <summary>
///  This class is used as a controller for the player. This is this class which will apply the movement of the player.
///  So after the modification done to the ScriptableObject <c>playerState</c> during the execution of the <c>InputController</c> and <c>NavigationController</c> classes,
///  <c>PlayerController</c> will apply the movements.
/// </summary>
public class PlayerController : MonoBehaviour
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
    ///  The <c>_rigidBody</c> property is a RigidBody2D which allow us to give physics to a <c>GameObject</c>.
    ///  </value>
    private Rigidbody2D _rigidBody;
    ///  <value>
    ///  The <c>_sprite</c> property is a SpriteRender which allow us to change the sprite of the player. This property will be remove when the AnimatorController will be ready for implementation.
    ///  </value>
    private SpriteRenderer _sprite;
    ///  <value>
    ///  The <c>_jumpTimeCounter</c> property is a float that is used as a time counter for the player's jump.
    ///  </value>
    private float _jumpTimeCounter;
    ///  <value>
    ///  The <c>_wallJumpTimeCounter</c> property is a float that is used as a time counter for the player's wall jump.
    ///  </value>
    private float _wallJumpTimeCounter;
    ///  <value>
    ///  The <c>_dashCurrentTimer</c> property is a float that is used as a time counter for the player's dash.
    ///  </value>
    private float _dashCurrentTimer;
    ///  <value>
    ///  The <c>_dashDirection</c> property is a Vector2 (Vector which have a x and y position) that is used to determine were the dash should go.
    ///  </value>
    private Vector2 _dashDirection;
    ///  <value>
    ///  The <c>jumpStoped</c> property is a boolean used to avoid multiple little jump during a long jump.
    ///  If the player jump and unpress the jump key, jumpStoped will be set on true and the player can't jump anymore before touching the ground.
    ///  </value>
    private bool jumpStoped = false;
    ///  <value>
    ///  The <c>wallJumpStoped</c> property is a boolean used to avoid multiple little jump during a long jump.
    ///  If the player jump and unpress the jump key, wallJumpStoped will be set on true and the player can't jump anymore before touching the ground.
    ///  </value>
    private bool wallJumpStoped = false;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_rigidBody</c>, <c>_sprite</c>) from the parent of the current <c>GameObject</c>.
    /// Moreover, we initialize the movements variables. (<c>movementSettings</c>)
    /// </summary>
    private void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _sprite = GetComponentInParent<SpriteRenderer>();
        movementSettings.Initialize();
    }

    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we execute the player wanted movements if they can be executed.
    /// As an example, if the player want to jump, but he is not touching the ground, the movement will not be executed.
    /// </summary>
    private void FixedUpdate()
    {
        UpdateAbility();

        if (!playerState.isDashing)
        {
            if (playerState.canWallSlide)
            {
                if ((playerState.wallSlideSide == 1 && playerState.horDir <= -0.5f) ||
                    (playerState.wallSlideSide == 2 && playerState.horDir >= 0.5f) ||
                    (playerState.wallSlideSide == 3 && (playerState.horDir >= 0.1f || playerState.horDir <= -0.1f)))
                    playerState.linearDragType = PlayerState.DragType.WALL;
            }

            if ((playerState.canWallJump && playerState.wantToJump) || (playerState.isWallJumping && _wallJumpTimeCounter > 0f && !wallJumpStoped && playerState.wantToJump) && !playerState.isJumping)
                WallJump();
            else
                wallJumpStoped = true;

            if ((playerState.wantToJump && playerState.canJump) || (playerState.isJumping && _jumpTimeCounter > 0f && !jumpStoped && playerState.wantToJump) && !playerState.isWallJumping)
                Jump();
            else
                jumpStoped = true;

            if (playerState.wantToMove && playerState.canMove)
                Move();
        }
        if (playerState.wantToDash && playerState.canDash && !playerState.isDashing)
        {
            playerState.canDash = false;
            playerState.isDashing = true;
            _rigidBody.velocity = new Vector2(0, 0);
            _dashCurrentTimer = movementSettings.dashTime;
            _sprite.color = Color.red;
            _dashDirection = new Vector2(playerState.facing, 0f).normalized;
            if (playerState.horDir != 0 || playerState.verDir != 0)
                _dashDirection = new Vector2(playerState.horDir, playerState.verDir).normalized;
        }
        if (playerState.isDashing)
            Dash();
        ApplyLayerEffect();
    }

    private void UpdateAbility()
    {
        
    }

    /// <summary>
    /// Function that put a high force during <c>movementSettings.dashTime</c> to the player in the <c>_dashDirection</c> Vector2's direction.
    /// </summary>
    private void Dash()
    {
        _rigidBody.gravityScale = 0f;
        _rigidBody.AddForce(_dashDirection * movementSettings.dashForce, ForceMode2D.Impulse);

        _dashCurrentTimer -= Time.deltaTime;
        if (_dashCurrentTimer <= 0)
        {
            playerState.isDashing = false;
            _sprite.color = Color.white;
            _rigidBody.gravityScale = 7f;
            _rigidBody.velocity = _rigidBody.velocity / 4;
        }
    }

    /// <summary>
    /// Function that put a force to the upper direction on the player's (<c>_rigidBody</c>).
    /// </summary>
    private void WallJump()
    {
        _rigidBody.velocity = new Vector2(-_rigidBody.velocity.x, 0f);
        _rigidBody.AddForce(Vector2.up * movementSettings.jumpForce * 1.5f, ForceMode2D.Impulse);

        Vector2 vect = Vector2.left;
        if (playerState.wallSlideSide == 1)
            vect = Vector2.right;

        _rigidBody.AddForce(vect * movementSettings.wallJumpForce * 1.6f, ForceMode2D.Impulse);

        if (!playerState.isWallJumping)
            _wallJumpTimeCounter = movementSettings.maxJumpTime;
        else
            _wallJumpTimeCounter -= Time.deltaTime;

        playerState.canWallJump = false;
        playerState.isWallJumping = true;
    }

    /// <summary>
    /// Function that put a force to the upper direction on the player's (<c>_rigidBody</c>).
    /// </summary>
    private void Jump()
    {
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
        _rigidBody.AddForce(Vector2.up * movementSettings.jumpForce, ForceMode2D.Impulse);

        if (!playerState.isJumping)
            _jumpTimeCounter = movementSettings.maxJumpTime;
        else
            _jumpTimeCounter -= Time.deltaTime;

        playerState.canJump = false;
        playerState.isJumping = true;
    }

    /// <summary>
    /// Function that moves the player in the wanted direction (<c>playerState.horDir</c>).
    /// If the player's not on the ground, the friction will be different than if he were on the ground.
    /// </summary>
    private void Move()
    {
        if(Mathf.Abs(_rigidBody.velocity.x) < movementSettings.maxMoveSpeed && playerState.linearDragType == PlayerState.DragType.GROUND)
            _rigidBody.AddForce(new Vector2(playerState.horDir, 0f) * movementSettings.maxAcceleration);

        else if(Mathf.Abs(_rigidBody.velocity.x) < (movementSettings.maxSpeed / 3) && playerState.linearDragType == PlayerState.DragType.AIR)
            _rigidBody.AddForce(new Vector2(playerState.horDir, 0f) * movementSettings.maxAcceleration);
        /*
        if (Mathf.Abs(_rigidBody.velocity.x) > movementSettings.maxMoveSpeed && playerState.linearDragType == PlayerState.DragType.GROUND)
            _rigidBody.velocity = new Vector2(Mathf.Sign(_rigidBody.velocity.x) * movementSettings.maxMoveSpeed, _rigidBody.velocity.y);

        if (Mathf.Abs(_rigidBody.velocity.x) > (movementSettings.maxSpeed / 3) && playerState.linearDragType == PlayerState.DragType.AIR)
            _rigidBody.velocity = new Vector2(Mathf.Sign(_rigidBody.velocity.x) * (movementSettings.maxSpeed / 3), _rigidBody.velocity.y);
        */
    }

    /// <summary>
    /// Function that apply differents effet if the player is in the <c>PlayerState.DragType.GROUND</c>, <c>PlayerState.DragType.AIR</c> or <c>PlayerState.DragType.Wall</c> State.
    /// </summary>
    private void ApplyLayerEffect()
    {
        bool isChangingDir = (_rigidBody.velocity.x > 0f && playerState.horDir < 0f) || (_rigidBody.velocity.x < 0f && playerState.horDir > 0f);

        switch (playerState.linearDragType)
        {
            case PlayerState.DragType.GROUND:
                jumpStoped = false;
                wallJumpStoped = false;
                if (Mathf.Abs(playerState.horDir) < 0.4f || isChangingDir)
                    _rigidBody.drag = movementSettings.groundLinearDrag;
                else
                    _rigidBody.drag = 0f;
                break;

            case PlayerState.DragType.AIR:
                _rigidBody.drag = movementSettings.airLinearDrag;
                break;

            case PlayerState.DragType.WALL:
                _rigidBody.drag = movementSettings.wallLinearDrag;
                break;
        }
    }
}
