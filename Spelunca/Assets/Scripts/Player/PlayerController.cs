using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementState movementState;
    public PlayerStats playerStats;

    private Rigidbody2D _rb;

    public LayerMask _groundLayer;
    public Collider2D _groundCheckerCollider;

    private bool _isChangingDir => (_rb.velocity.x > 0f && movementState.horizontalDir < 0f) || (_rb.velocity.x < 0f && movementState.horizontalDir > 0f);
    private bool _isGrounded = false;
    private bool _canJump => (_isGrounded && movementState.jump);
    private float jumpTimeCounter;

    private void Start()
    {
        playerStats.Initialize();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckIsGrounded();

        if (_canJump)
        {
            Jump();
            jumpTimeCounter = playerStats.jumpTime;
            movementState.jump = true;
        }
        else if (movementState.run)
            Run();

        if(movementState.jump)
        {
            if (jumpTimeCounter > 0f)
            {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                movementState.jump = false;
        }
        if (!Input.GetButton("Jump"))
            movementState.jump = false;
        if (_isGrounded)
            ApplyGroundLinearDrag();
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }

    private void Run()
    {
        _rb.AddForce(new Vector2(movementState.horizontalDir, 0f) * playerStats.maxAcceleration);
         
        if (Mathf.Abs(_rb.velocity.x) > playerStats.maxSpeed)
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * playerStats.maxSpeed, _rb.velocity.y);
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(movementState.horizontalDir) < 0.4f || _isChangingDir)
            _rb.drag = playerStats.groundLinearDrag;
        else
            _rb.drag = 0f;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * playerStats.jumpForce, ForceMode2D.Impulse);
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = playerStats.airLinearDrag;
    }

    private void FallMultiplier()
    {
        if (movementState.verticalDir < 0f)
        {
            _rb.gravityScale = playerStats.downMultiplier;
        }
        else
        {
            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = playerStats.fallMultiplier;
            }
            else if (_rb.velocity.y > 0 && !movementState.jump)
            {
                _rb.gravityScale = playerStats.lowFallMultiplier;
            }
            else
            {
                _rb.gravityScale = 1f;
            }
        }
    }

    private void CheckIsGrounded()
    {
        _isGrounded = _groundCheckerCollider.IsTouchingLayers(_groundLayer.value);
    }
}
