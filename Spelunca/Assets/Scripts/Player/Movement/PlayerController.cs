using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    public MovementSettings movementSettings;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _sprite;

    private float jumpTimeCounter;
    private float dashCurrentTimer;
    private Vector2 dashDirection;

    private void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _sprite = GetComponentInParent<SpriteRenderer>();
        movementSettings.Initialize();
    }

    private void FixedUpdate()
    {
        if (!playerState.isDashing)
        {
            if ((playerState.wantToJump && playerState.canJump) || (playerState.isJumping && jumpTimeCounter > 0f && playerState.wantToJump))
                Jump();
            if (playerState.wantToMove && playerState.canMove)
                Move();
        }

        if (playerState.wantToDash && playerState.canDash && !playerState.isDashing)
        {
            dashCurrentTimer = movementSettings.dashTime;
            playerState.canDash = false;
            playerState.isDashing = true;
            _sprite.color = Color.red;
            dashDirection = new Vector2(playerState.facing, 0f).normalized;
            if (playerState.horDir != 0 || playerState.verDir != 0)
                dashDirection = new Vector2(playerState.horDir, playerState.verDir).normalized;
        }
        if (playerState.isDashing)
            Dash();
        ApplyLayerEffect();
    }

    private void Dash()
    {
        _rigidBody.gravityScale = 0f;
        _rigidBody.AddForce(dashDirection * movementSettings.dashForce, ForceMode2D.Impulse);

        dashCurrentTimer -= Time.deltaTime;
        if (dashCurrentTimer <= 0)
        {
            playerState.isDashing = false;
            _sprite.color = Color.white;
            _rigidBody.gravityScale = 7f;
            _rigidBody.velocity = _rigidBody.velocity / 4;
        }
    }

    private void Jump()
    {
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
        _rigidBody.AddForce(Vector2.up * movementSettings.jumpForce, ForceMode2D.Impulse);

        if (!playerState.isJumping)
            jumpTimeCounter = movementSettings.maxJumpTime;
        else
            jumpTimeCounter -= Time.deltaTime;

        playerState.canJump = false;
        playerState.isJumping = true;
    }

    private void Move()
    {
        _rigidBody.AddForce(new Vector2(playerState.horDir, 0f) * movementSettings.maxAcceleration);

        if (Mathf.Abs(_rigidBody.velocity.x) > movementSettings.maxMoveSpeed && playerState.linearDragType == PlayerState.DragType.GROUND)
            _rigidBody.velocity = new Vector2(Mathf.Sign(_rigidBody.velocity.x) * movementSettings.maxMoveSpeed, _rigidBody.velocity.y);

        if (Mathf.Abs(_rigidBody.velocity.x) > (movementSettings.maxSpeed / 3) && playerState.linearDragType == PlayerState.DragType.AIR)
            _rigidBody.velocity = new Vector2(Mathf.Sign(_rigidBody.velocity.x) * (movementSettings.maxSpeed / 3), _rigidBody.velocity.y);
    }

    private void ApplyLayerEffect()
    {
        bool isChangingDir = (_rigidBody.velocity.x > 0f && playerState.horDir < 0f) || (_rigidBody.velocity.x < 0f && playerState.horDir > 0f);

        switch (playerState.linearDragType)
        {
            case PlayerState.DragType.GROUND:
                if (Mathf.Abs(playerState.horDir) < 0.4f || isChangingDir)
                    _rigidBody.drag = movementSettings.groundLinearDrag;
                else
                    _rigidBody.drag = 0f;
                break;

            case PlayerState.DragType.AIR:
                _rigidBody.drag = movementSettings.airLinearDrag;
                break;

            case PlayerState.DragType.WALL:
                Debug.Log("Not implemented yet.");
                break;
        }
    }
}
