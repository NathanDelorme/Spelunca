using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrb : MonoBehaviour
{
    public PlayerState playerState;
    public BoxCollider2D playerCollider;

    private PolygonCollider2D _collider;
    private SpriteRenderer _sprite;
    private float _objectTotalTime = 2f;
    private float _objectTime = 0f;
    private bool _isEnable = true;

    void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private bool CheckCollision()
    {
        return _collider.IsTouching(playerCollider);
    }

    private void RecoverDash()
    {
        _isEnable = false;
        playerState.canDash = true;
        _sprite.color = Color.black;
        _objectTime = _objectTotalTime;
    }

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