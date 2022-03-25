using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe v�rifie ce que le joueur peut faire.
/// </summary>
public class NavigationController : MonoBehaviour
{
    /// <value>
    /// Cette propri�t� (<see cref="PlayerState"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour savoir ce que le joueur veut faire,
    /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Cette propri�t� (<see cref="MovementSettings"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour les mouvements (exemple : force du saut ou duration du dash).
    /// </value>
    public MovementSettings movementSettings;
    ///  <value>
    ///  RigidBody2D qui permet d'ajouter de la physique � un GameObject.
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// Liste des layers qui doivent �tre consid�r�s comme le sol.
    /// </value>
    public LayerMask groundLayers;
    /// <value>
    /// Zone de collision du sol.
    /// </value>
    public Collider2D groundCheckCollider;
    /// <value>
    /// Zone de collision permettant de savoir si le joueur peut wall jump vers la droite.
    /// </value>
    public Collider2D wallLeftCheckCollider;
    /// <value>
    /// Zone de collision permettant de savoir si le joueur peut wall jump vers la gauche.
    /// </value>
    public Collider2D wallRightCheckCollider;
    /// <value>
    /// Float utilis� comme compteur pour le coyote time.
    /// </value>
    private float _coyoteTimeCounter;
    /// <value>
    /// Float utilis� comme temps maximum avant de pouvoir de nouveau dash apr�s un dash.
    /// </value>
    private float _dashTimeBuffer = 0.05f;
    /// <value>
    /// Float utilis� comme cooldown avant de pouvoir de nouveau dash apr�s un dash.
    /// </value>
    private float _dashTimeBufferCounter;

    /// <summary>
    /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
    /// </summary>
    private void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
    }

    /// <summary>
    /// Fonction ex�cut� un nombre d�termin� de fois par secondes.
    /// Permet de r�cup�rer si le joueur peut sauter, dash, wall slide ou wall jump.
    /// </summary>
    private void FixedUpdate()
    {
        CheckCanJump();
        CheckCanDash();
        CheckCanWallSlide();
        CheckCanWallJump();
    }

    /// <summary>
    /// Detecte si le joueur peut sauter.
    /// </summary>
    private void CheckCanJump()
    {
        if(CheckTouchingGround())
        {
            _coyoteTimeCounter = movementSettings.maxCoyoteTime;
            playerState.linearDragType = PlayerState.DragType.GROUND;
            playerState.canJump = true;
            playerState.isJumping = false;
            playerState.isWallJumping = false;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            playerState.linearDragType = PlayerState.DragType.AIR;
            playerState.canJump = playerState.isJumping == false && _coyoteTimeCounter > 0f;
        }
    }

    /// <summary>
    /// Detecte si le joueur peut dash.
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
    /// Detecte si le joueur peut wall slide.
    /// </summary>
    private void CheckCanWallSlide()
    {
        playerState.canWallSlide = !CheckTouchingGround() && CheckTouchingWall() && _rigidBody.velocity.y < -0.2f;
        if (!playerState.canWallSlide)
            playerState.wallSlideSide = -1;
        else
        {
            if (wallLeftCheckCollider.IsTouchingLayers(groundLayers) && wallRightCheckCollider.IsTouchingLayers(groundLayers))
                playerState.wallSlideSide = 3;
            else if (wallLeftCheckCollider.IsTouchingLayers(groundLayers))
                playerState.wallSlideSide = 1;
            else
                playerState.wallSlideSide = 2;
        }
    }

    /// <summary>
    /// Detecte si le joueur peut wall jump.
    /// </summary>
    private void CheckCanWallJump()
    {
        playerState.canWallJump = !CheckTouchingGround() && CheckTouchingWall() && _rigidBody.velocity.y < -0.2f;
        if (!playerState.canWallJump)
            playerState.wallJumpSide = -1;
        else
        {
            if (wallLeftCheckCollider.IsTouchingLayers(groundLayers) && wallRightCheckCollider.IsTouchingLayers(groundLayers))
                playerState.wallJumpSide = 3;
            else if (wallLeftCheckCollider.IsTouchingLayers(groundLayers))
                playerState.wallJumpSide = 1;
            else
                playerState.wallJumpSide = 2;
        }
    }

    /// <summary>
    /// Function qui d�tecte si le joueur est sur le sol.
    /// </summary>
    /// <returns>Vrai si sur le sol, sinon Faux.</returns>
    private bool CheckTouchingGround()
    {
        if (groundCheckCollider.IsTouchingLayers(groundLayers))
            return true;
        return false;
    }

    /// <summary>
    /// Function qui d�tecte si le joueur touche un mur.
    /// </summary>
    /// <returns>Vrai si touche un mur, sinon Faux.</returns>
    private bool CheckTouchingWall()
    {
        if (wallLeftCheckCollider.IsTouchingLayers(groundLayers) || wallRightCheckCollider.IsTouchingLayers(groundLayers))
            return true;
        return false;
    }
}
