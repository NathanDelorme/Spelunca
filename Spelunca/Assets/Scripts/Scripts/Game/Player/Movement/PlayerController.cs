using Audio;
using Data;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Cette classe est utilis� comme un controlleur pour le joueur.
    /// Elle permet d'appliquer les mouvements du joueur en fonction de ce qu'il veut/peut faire.
    /// </summary>
    public class PlayerController : MonoBehaviour
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
        /// <value>
        /// Animator qui permet de d�finir des variables dans l'animator pour savoir l'animation que le personnage doit executer.
        /// </value>
        private Animator animator;
        ///  <value>
        ///  RigidBody2D qui permet d'ajouter de la physique � un GameObject.
        ///  </value>
        private Rigidbody2D _rigidBody;
        ///  <value>
        ///  SpriteRenderer qui permet de changer l'apparence du joueur.
        ///  </value>
        private SpriteRenderer _sprite;
        ///  <value>
        ///  Float utilis� comme compteur pour le saut du joueur.
        ///  </value>
        private float _jumpTimeCounter;
        ///  <value>
        ///  Float utilis� comme compteur pour le wzll jump du joueur.
        ///  </value>
        private float _wallJumpTimeCounter;
        ///  <value>
        ///  Float utilis� comme compteur pour le dash du joueur.
        ///  </value>
        private float _dashCurrentTimer;
        ///  <value>
        ///  Vector en 2D qui repr�sente la direction du dash du joueur.
        ///  </value>
        private Vector2 _dashDirection;
        ///  <value>
        ///  Variable qui emp�che de faire de multiples jumps lors du spam de la touche de saut.
        ///  Si le joueur arr�te d'appuyer sur la touche de saut, automatiquement cette variable sera mise sur Vrai et empechera le joueur de re sauter. 
        ///  </value>
        private bool jumpStoped = false;
        private bool wallJumpStoped = false;
        /// <value>
        /// R�f�rence au manager des effets sonores du jeu.
        /// </value>
        private SFXManager sfxManager;

        /// <value>
        /// Permet l'ajout d'un effet de fantome lors du dash du joueur.
        /// </value>
        [SerializeField]
        public GameObject playerGhost;

        /// <summary>
        /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
        /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
        /// </summary>
        private void Start()
        {
            sfxManager = FindObjectOfType<SFXManager>();
            _rigidBody = GetComponentInParent<Rigidbody2D>();
            _sprite = GetComponentInParent<SpriteRenderer>();
            animator = GetComponentInParent<Animator>();
            movementSettings.Initialize();
        }

        /// <summary>
        /// Fonction ex�cut� � chaque frame.
        /// </summary>
        private void Update()
        {
            UpdateAnimations();
            FlipSprite();

            if (playerState.isDashing)
                Instantiate(playerGhost, this.transform.position, this.transform.rotation);
        }

        /// <summary>
        /// Fonction ex�cut� un nombre d�terminer de fois par seconde.
        /// Execute les fonction n�cessaire au d�placement du joueur.
        /// </summary>
        private void FixedUpdate()
        {
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
                {
                    wallJumpStoped = true;
                    playerState.isWallJumping = false;
                }

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
                if (_rigidBody.bodyType != RigidbodyType2D.Static)
                    _rigidBody.velocity = new Vector2(0, 0);
                _dashCurrentTimer = movementSettings.dashTime;
                _dashDirection = new Vector2(playerState.facing, 0f).normalized;
                if (playerState.horDir != 0 || playerState.verDir != 0)
                    _dashDirection = new Vector2(playerState.horDir, playerState.verDir).normalized;
            }
            if (playerState.isDashing)
                Dash();
            ApplyLayerEffect();
        }

        /// <summary>
        /// Change l'animation du joueur qui est jou� par l'attribut <c>animator</c>.
        /// </summary>
        private void UpdateAnimations()
        {
            bool isIdle = ((Mathf.Abs(playerState.horDir) < 0.05 || Mathf.Abs(_rigidBody.velocity.x) < 0.05) && Mathf.Abs(_rigidBody.velocity.y) < 0.05 && playerState.canJump);
            bool isFalling = (_rigidBody.velocity.y < -0.05 && !playerState.canJump && !playerState.isWallSliding);
            bool isRunning = (Mathf.Abs(playerState.horDir) >= 0.05 && Mathf.Abs(_rigidBody.velocity.x) >= 0.05 && playerState.canJump && Mathf.Abs(_rigidBody.velocity.y) < 0.05);
            bool isJumping = (_rigidBody.velocity.y >= 0.05 && ((playerState.isJumping && !playerState.isWallSliding) || (playerState.isWallJumping)));
            bool isDashing = (playerState.isDashing);
            bool isWallSliding = (playerState.isWallSliding && !playerState.isWallJumping && Mathf.Abs(_rigidBody.velocity.y) > 0.05);

            animator.SetBool("IsIdle", isIdle);
            animator.SetBool("IsFalling", isFalling);
            animator.SetBool("IsRunning", isRunning);
            animator.SetBool("IsJumping", isJumping);
            animator.SetBool("IsDashing", isDashing);
            animator.SetBool("IsWallSliding", isWallSliding);
        }

        /// <summary>
        /// Change de sens le sprite du joueur pour le faire regarder � droite ou � gauche.
        /// </summary>
        private void FlipSprite()
        {
            if (!playerState.isWallSliding && !playerState.isWallJumping)
            {
                if (playerState.horDir < -0.05)
                    _sprite.flipX = true;
                else if (playerState.horDir > 0.05)
                    _sprite.flipX = false;
            }
            else if (!playerState.isWallJumping)
            {
                if (_rigidBody.velocity.x < -0.05)
                    _sprite.flipX = false;
                else if (_rigidBody.velocity.x > 0.05)
                    _sprite.flipX = true;
            }
            else
            {
                if (_rigidBody.velocity.x < -0.05)
                    _sprite.flipX = true;
                else if (_rigidBody.velocity.x > 0.05)
                    _sprite.flipX = false;
            }
        }

        /// <summary>
        /// Fonction qui applique une grande force durant <c>movementSettings.dashTime</c> au joueur dans la direction <c>_dashDirection</c> qui est un vector2D.
        /// </summary>
        private void Dash()
        {
            _rigidBody.gravityScale = 0f;
            _rigidBody.AddForce(_dashDirection * movementSettings.dashForce, ForceMode2D.Impulse);

            _dashCurrentTimer -= Time.deltaTime;
            if (_dashCurrentTimer <= 0)
            {
                playerState.isDashing = false;
                _rigidBody.gravityScale = 7f;
                if (_rigidBody.bodyType != RigidbodyType2D.Static)
                    _rigidBody.velocity = _rigidBody.velocity / 4;
            }
        }

        /// <summary>
        /// Fonction qui applique une force dans la direction oppos� au mur pour le wall jump.
        /// </summary>
        private void WallJump()
        {
            if (_rigidBody.bodyType != RigidbodyType2D.Static)
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
        /// Fonction qui applique une force vers le haut du joueur.
        /// </summary>
        private void Jump()
        {
            if (_rigidBody.bodyType != RigidbodyType2D.Static)
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
        /// Fonction qui d�place le joueur dans la direction souhait� <c>playerState.horDir</c>.
        /// Si le joueur n'est pas sur le sol, alors la friction appliqu� au joueur sera diff�rente de celle appliqu� s'il �tait sur le sol.
        /// </summary>
        private void Move()
        {
            if (Mathf.Abs(_rigidBody.velocity.x) < movementSettings.maxMoveSpeed && playerState.linearDragType == PlayerState.DragType.GROUND)
            {
                _rigidBody.AddForce(new Vector2(playerState.horDir, 0f) * movementSettings.maxAcceleration);
            }

            else if (Mathf.Abs(_rigidBody.velocity.x) < (movementSettings.maxSpeed / 3) && playerState.linearDragType == PlayerState.DragType.AIR)
                _rigidBody.AddForce(new Vector2(playerState.horDir, 0f) * movementSettings.maxAcceleration);
        }

        /// <summary>
        /// Fonction qui applique les diff�rents effets de friction sur le joueur.
        /// <c>PlayerState.DragType.GROUND</c>, <c>PlayerState.DragType.AIR</c> ou <c>PlayerState.DragType.Wall</c>.
        /// </summary>
        private void ApplyLayerEffect()
        {
            bool isChangingDir = (_rigidBody.velocity.x > 0f && playerState.horDir < 0f) || (_rigidBody.velocity.x < 0f && playerState.horDir > 0f);

            switch (playerState.linearDragType)
            {
                case PlayerState.DragType.GROUND:
                    playerState.isWallSliding = false;
                    jumpStoped = false;
                    wallJumpStoped = false;
                    if (Mathf.Abs(playerState.horDir) < 0.4f || isChangingDir)
                        _rigidBody.drag = movementSettings.groundLinearDrag;
                    else
                        _rigidBody.drag = 0f;
                    break;

                case PlayerState.DragType.AIR:
                    playerState.isWallSliding = false;
                    _rigidBody.drag = movementSettings.airLinearDrag;
                    break;

                case PlayerState.DragType.WALL:
                    playerState.isWallSliding = true;
                    _rigidBody.drag = movementSettings.wallLinearDrag;
                    break;
            }
        }
    }
}
