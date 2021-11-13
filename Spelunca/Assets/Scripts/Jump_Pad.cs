using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Jump_Pad : MonoBehaviour
{
    public LayerMask jumpPadZone;
    public Tilemap tilemap;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _playerCollider;
    private float _bounceForce = 30f;

    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _playerCollider = GetComponentInParent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckIsInJumpPad();
    }

    private void CheckIsInJumpPad()
    {
        if (_playerCollider.IsTouchingLayers(jumpPadZone.value))
        {
            JumpPad(GetTileDirection());
        }
    }
    
    private int GetTileDirection()
    {
        Tile tile = null;
        for(int x = (int)_rigidBody.position.x - 1; x <= (int)_rigidBody.position.x + 1; x++)
        {
            Vector3Int tilePos = new Vector3Int(x, (int)(_rigidBody.position.y - 1), 0);
            tile = (Tile)tilemap.GetTile(tilePos);

            if (tile != null)
            {
                string tileName = tile.name;
                return Int32.Parse(tileName.Substring(tileName.Length - 1));
            }
        }
        return -1;
    }

    private void JumpPad(int padDirection)
    {
        Debug.Log(padDirection);

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
                Debug.Log("Not on the Jump pad");
                break;
        }
        
    }
}
