using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Jump_Pad : MonoBehaviour
{
    /// <value>
    /// The <c>jumpPadZone</c> property is a Layer which represent the jump pads collisions in the level.
    /// </value>
    public LayerMask jumpPadZone;
    /// <value>
    /// The <c>tilemap</c> property is a Tilemap of the jump pad tilemap. It allow us to pass through the tiles and get their names.
    /// The goal is to detect the position of the jump pad to know what is the bounce to apply.
    /// </value>
    public Tilemap tilemap;

    ///  <value>
    ///  The <c>_rigidBody</c> property is a RigidBody2D which allow us to give physics to a <c>GameObject</c>.
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// The <c>_playerCollider</c> property is a BoxCollider2D. It's an hitbox which is usefull to detect when the player hit something like a jump pad.
    /// </value>
    private BoxCollider2D _playerCollider;
    /// <value>
    /// Force of the bounce to apply to the player when touching the <c>jumpPadZone</c> layer.
    /// </value>
    private float _bounceForce = 30f;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_rigidBody</c>, <c>_playerCollider</c>) from the parent of the current <c>GameObject</c>.
    /// </summary>
    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _playerCollider = GetComponentInParent<BoxCollider2D>();
    }

    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we check if the player is touching a jump pad or not.
    /// If he is touching a jump pad, we apply to the player the bounce with the good direction.
    /// </summary>
    private void FixedUpdate()
    {
        if(CheckIsInJumpPad())
            JumpPad(GetTileDirection());
    }

    /// <summary>
    /// Function that check if the player touch a jump pad.
    /// </summary>
    /// <returns>
    /// True if the player is in the dead zone, else false.
    /// </returns>
    private bool CheckIsInJumpPad()
    {
        return _playerCollider.IsTouchingLayers(jumpPadZone.value);
    }

    /// <summary>
    /// Get the direction of the jump pad tile.
    /// </summary>
    /// <return>
    /// Return an int :
    ///     0 : the jump pad bounce up.
    ///     1 : the jump pad bounce left.
    ///     2 : the jump pad bounce down.
    ///     3 : the jump pad bounce right.
    /// </return>
    private int GetTileDirection()
    {
        Tile tile = null;
        for(int x = (int)_rigidBody.position.x - 1; x <= (int)_rigidBody.position.x + 1; x++)
        {
            for (int y = (int)_rigidBody.position.y - 1; y <= (int)_rigidBody.position.y + 1; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                tile = (Tile)tilemap.GetTile(tilePos);

                if (tile != null)
                {
                    string tileName = tile.name;
                    return Int32.Parse(tileName.Substring(tileName.Length - 1));
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// Function that apply the good bounce from the direction
    /// 0 : the jump pad bounce up.
    /// 1 : the jump pad bounce left.
    /// 2 : the jump pad bounce down.
    /// 3 : the jump pad bounce right.
    /// </summary>
    /// <param name="padDirection">
    /// The direction of the trigered jump pad.
    /// </param>
    /// <argument>
    private void JumpPad(int padDirection)
    {
        switch(padDirection)
        {
            case 0:
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
                break;
            case 1:
                _rigidBody.velocity = new Vector2(0f, _rigidBody.velocity.y);
                _rigidBody.AddForce(Vector2.left * _bounceForce, ForceMode2D.Impulse);
                break;
            case 2:
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.down * _bounceForce, ForceMode2D.Impulse);
                break;
            case 3:
                _rigidBody.velocity = new Vector2(0f, _rigidBody.velocity.y);
                _rigidBody.AddForce(Vector2.right * _bounceForce, ForceMode2D.Impulse);
                break;
            default:
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
                break;
        }
        
    }
}
