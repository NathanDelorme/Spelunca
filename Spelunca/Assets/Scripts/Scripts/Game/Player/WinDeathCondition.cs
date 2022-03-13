using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  This class get and interpret when the player should die or win.
///  By winning, we mean finish the current play level
///  By dying, we mean being touche by something that kill the player. like spikes or void.
/// </summary>
public class WinDeathCondition : MonoBehaviour
{
    public GameObject endMenu;
    public PlayerState playerState;
    private AbilitySystem abilitySystem;
    /// <value>
    /// The <c>killZone</c> property is a Layer which represent dead zone in the level.
    /// </value>
    /// <value>
    /// The <c>spawnPoint</c> property is a GameObject that is placed at the desired spawn point.
    /// </value>
    public GameObject spawnPoint;
    ///  <value>
    ///  The <c>_rigidBody</c> property is a RigidBody2D which allow us to give physics to a <c>GameObject</c>.
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// The <c>_playerCollider</c> property is a BoxCollider2D. It's an hitbox which is usefull to detect when the player is hit by something.
    /// </value>
    public BoxCollider2D _playerCollider;
    public bool reverseSpikeZone = false;

    public Movement[] movementPlatforms;
    public FallingPlateform[] fallingPlatforms;
    public OrbsDashManager orbsDashManager;
    private bool firstLoad = true;

    private Animator animator;
    private SpriteRenderer _sprite;
    public bool isKilled = false;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_rigidBody</c>, <c>_playerCollider</c>) from the parent of the current <c>GameObject</c>.
    /// Moreover, we initialize spawn the player at the good position.
    /// </summary>
    void Start()
    {
        SaveSceneName();
        animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        orbsDashManager = FindObjectOfType<OrbsDashManager>();
        movementPlatforms = FindObjectsOfType<Movement>();
        fallingPlatforms = FindObjectsOfType<FallingPlateform>();
        abilitySystem = FindObjectOfType<AbilitySystem>();
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        SpawnPlayer();
    }

    /// <summary>
    /// Function that teleport the player at the spawnpoint of the level.
    /// </summary>
    private void SpawnPlayer()
    {
        playerState.isDashing = false;
        playerState.isJumping = false;
        playerState.isWallJumping = false;
        playerState.isWallSliding = false;
        playerState.isMoving = false;

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        if (!firstLoad)
        {
            orbsDashManager.resetOrbs();
            foreach (Movement movementScript in movementPlatforms)
                movementScript.Respawn();
            foreach (FallingPlateform fallingPlatformScript in fallingPlatforms)
                fallingPlatformScript.InstantRespawn();
        }

        abilitySystem.SetState(new NoneState(abilitySystem));
        isKilled = false;
        _sprite.flipX = false;
        _rigidBody.transform.position = spawnPoint.transform.position;
        _rigidBody.velocity = new Vector2(0f, 0f);
        _rigidBody.gravityScale = 7f;
        firstLoad = false;
        Timer timer = FindObjectOfType<Timer>();
        timer.SaveTime(true);
    }

    private void KillPlayer()
    {
        isKilled = true;
        _rigidBody.bodyType = RigidbodyType2D.Static;
        animator.Play("Death");
        Invoke("SpawnPlayer", 0.45f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (reverseSpikeZone)
        {
            if (collision.collider.CompareTag("Ground"))
                KillPlayer();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            NextLevel();

        else if (collision.CompareTag("KillZone"))
            KillPlayer();

        else if (!reverseSpikeZone)
        {
            if (collision.CompareTag("SpikeZone") && _playerCollider.IsTouchingLayers(7))
                KillPlayer();
        }
    }

    private void NextLevel()
    {
        Timer timer = FindObjectOfType<Timer>();
        timer.SaveTime();

        int id = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5)) + 1;

        if (id <= 20)
        {
            PlayerPrefs.SetInt(Application.version + "Level" + id.ToString(), 1);
            PlayerPrefs.Save();
        }
        endMenu.SetActive(true);
    }

    private void SaveSceneName()
    {
        PlayerPrefs.SetString(Application.version + "player_lastScene", SceneManager.GetActiveScene().name);
    }
}
