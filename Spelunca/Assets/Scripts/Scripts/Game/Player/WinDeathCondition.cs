using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  This class get and interpret when the player should die or win.
///  By winning, we mean finish the current play level
///  By dying, we mean being touche by something that kill the player. like spikes or void.
/// </summary>
public class WinDeathCondition : MonoBehaviour
{
    /// <value>
    /// The <c>killZone</c> property is a Layer which represent dead zone in the level.
    /// </value>
    public LayerMask killZone;
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
    private BoxCollider2D _playerCollider;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_rigidBody</c>, <c>_playerCollider</c>) from the parent of the current <c>GameObject</c>.
    /// Moreover, we initialize spawn the player at the good position.
    /// </summary>
    void Start()
    {
        SaveSceneName();
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _playerCollider = GetComponentInParent<BoxCollider2D>();
        SpawnPlayer();
    }
    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we check if the player is touching the kill zone or not.
    /// If he is touching the kill zone, he will be teleported to the spawnpoint
    /// </summary>
    private void FixedUpdate()
    {
        if (CheckIsInDeadZone())
            SpawnPlayer();
    }

    /// <summary>
    /// Function that teleport the player at the spawnpoint of the level.
    /// </summary>
    private void SpawnPlayer()
    {
        _rigidBody.transform.position = spawnPoint.transform.position;
        _rigidBody.velocity = new Vector2(0f, 0f);
    }

    /// <summary>
    /// Function that check if the player touch the dead zone or not.
    /// </summary>
    /// <returns>
    /// True if the player is in the dead zone, else false.
    /// </returns>
    private bool CheckIsInDeadZone()
    {
        return _playerCollider.IsTouchingLayers(killZone.value);
    }

    private void SaveSceneName()
    {
        PlayerPrefs.SetString("player_lastScene", SceneManager.GetActiveScene().name);
    }
}
