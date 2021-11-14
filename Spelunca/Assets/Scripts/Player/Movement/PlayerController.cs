using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    public MovementSettings movementSettings;
    private Rigidbody2D _rigidBody;

    private float jumpTimeCounter;
    private float dashTimeCounter;

    private void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        movementSettings.Initialize();
    }

    private void FixedUpdate()
    {
        if (playerState.wantToDash && true)
            Dash();
        if ((playerState.wantToJump && playerState.canJump) || playerState.isJumping)
            Jump();
        if (playerState.wantToMove && playerState.canMove)
            Move();
        
        ApplyLayerEffect();
    }

    private void Dash()
    {
        if (!playerState.isDashing)
        {
            _rigidBody.velocity = Vector2.zero;
            _rigidBody.drag = 0f;

            Vector2 dir;
            if (_rigidBody.velocity.x != 0f || _rigidBody.velocity.y != 0f) 
                dir = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y);
            else
            {
                /*if (playerState.facing) */dir = new Vector2(1f, 0f);
                /*else dir = new Vector2(-1f, 0f);*/
            }
            
            while (Time.time < movementSettings.dashTime + movementSettings.dashLength)
            {
                _rigidBody.velocity = dir.normalized * movementSettings.dashSpeed;
            }
                    
                    
            //_rigidBody.AddForce(Vector2.up * movementSettings.dashSpeed, ForceMode2D.Impulse);
        }

        else
        {
            //dashTimeCounter > 0f && 
            if (playerState.wantToDash)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.up * movementSettings.dashSpeed, ForceMode2D.Impulse);
            }
        }
        playerState.canDash = false;
        playerState.isDashing = true;
    }

    private void Jump()
    {
        if (!playerState.isJumping)
        {
            jumpTimeCounter = movementSettings.maxJumpTime;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
            _rigidBody.AddForce(Vector2.up * movementSettings.jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            if (jumpTimeCounter > 0f && playerState.wantToJump)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.up * movementSettings.jumpForce, ForceMode2D.Impulse);
            }
            jumpTimeCounter -= Time.deltaTime;
        }
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
