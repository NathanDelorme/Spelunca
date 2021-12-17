using UnityEngine;

/// <summary>
/// This class is the class that define the comportement of a dash orb
/// </summary>
public class DashOrb : MonoBehaviour
{
    /// <value>
    /// The <c>playerState</c> property is a ScriptableObject (structure implemented by Unity) which is an object with values shared between all scripts and scene that use it.
    /// playerState store all variables usefull to know what the player want to do, what he can do and what he is doing.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// The <c>playerCollider</c> property is a BoxCollider2D which represent the player hitbox.
    /// </value>
    public BoxCollider2D playerCollider;
    /// <value>
    /// The <c>_collider</c> property is a PolygonCollider2D which represent the orb's hitbox.
    /// </value>
    private PolygonCollider2D _collider;
    /// <value>
    /// The <c>_sprite</c> property is a SpriteRenderer which is the current sprite of the dash orb.
    /// </value>
    private SpriteRenderer _sprite;
    /// <value>
    /// The <c>_objectTotalTime</c> property is a float which is the number of second needed to switch the dash orb from enable to disable.
    /// </value>
    private float _objectTotalTime = 2f;
    /// <value>
    /// The <c>_objectTime</c> property is a float which is the current timer.
    /// </value>
    private float _objectTime = 0f;
    /// <value>
    /// The <c>_isEnable</c> property is a boolean which is the state of the dash orb.
    /// </value>
    private bool _isEnable = true;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_collider</c>, <c>_sprite</c>) from the parent of the current <c>GameObject</c>.
    /// </summary>
    void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Check if the player collide with the dash orb
    /// </summary>
    private bool CheckCollision()
    {
        return _collider.IsTouching(playerCollider);
    }

    /// <summary>
    /// Allow to the player to recover his dash.
    /// </summary>
    private void RecoverDash()
    {
        _isEnable = false;
        playerState.canDash = true;
        _sprite.color = Color.black;
        _objectTime = _objectTotalTime;
    }

    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we get if the player collide with the orb dash.
    /// </summary>
    void FixedUpdate()
    {
        if(CheckCollision() && _isEnable)
            RecoverDash();

        if(!_isEnable)
            _objectTime -= Time.deltaTime;

        if(!_isEnable && _objectTime <= 0)
        {
            _isEnable = true;
            _sprite.color = Color.white;
            _objectTime = 0f;
        }
    }
}