using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  This class get and interpret when the player should die or win.
///  By winning, we mean finish the current play level
///  By dying, we mean being touche by something that kill the player. like spikes or void.
/// </summary>
public class WinDeathCondition : MonoBehaviour
{
    private AbilitySystem abilitySystem;
    /// <value>
    /// The <c>killZone</c> property is a Layer which represent dead zone in the level.
    /// </value>
    /// <value>
    /// The <c>spawnPoint</c> property is a GameObject that is placed at the desired spawn point.
    /// </value>
    public GameObject spawnPoint;
    public BoxCollider2D finishPoint;
    ///  <value>
    ///  The <c>_rigidBody</c> property is a RigidBody2D which allow us to give physics to a <c>GameObject</c>.
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// The <c>_playerCollider</c> property is a BoxCollider2D. It's an hitbox which is usefull to detect when the player is hit by something.
    /// </value>
    public BoxCollider2D _playerCollider;
    public bool reverseSpikeZone = false;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_rigidBody</c>, <c>_playerCollider</c>) from the parent of the current <c>GameObject</c>.
    /// Moreover, we initialize spawn the player at the good position.
    /// </summary>
    void Start()
    {
        SaveSceneName();
        abilitySystem = FindObjectOfType<AbilitySystem>();
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        //_playerCollider = GetComponentInParent<BoxCollider2D>();
        SpawnPlayer();
    }

    /// <summary>
    /// Function that teleport the player at the spawnpoint of the level.
    /// </summary>
    private void SpawnPlayer()
    {
        abilitySystem.SetState(new NoneState(abilitySystem));
        _rigidBody.transform.position = spawnPoint.transform.position;
        _rigidBody.velocity = new Vector2(0f, 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (reverseSpikeZone)
        {
            if (collision.collider.CompareTag("Ground"))
                SpawnPlayer();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            NextLevel();

        else if (collision.CompareTag("KillZone"))
            SpawnPlayer();

        else if (!reverseSpikeZone)
        {
            if (collision.CompareTag("SpikeZone"))
                SpawnPlayer();
        }
    }

    private void NextLevel()
    {
        int id = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5)) + 1;

        if (id < 20)
        {
            PlayerPrefs.SetInt("Level" + id.ToString(), 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Scenes/Levels/Level" + id);
        }
        else
        {
            SceneManager.LoadScene("Scenes/UI/MainMenu");
        }
    }

    private void SaveSceneName()
    {
        PlayerPrefs.SetString("player_lastScene", SceneManager.GetActiveScene().name);
    }
}
