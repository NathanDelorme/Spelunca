using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDeathCondition : MonoBehaviour
{
    public LayerMask killZone;
    public GameObject spawnPoint;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _playerCollider;

    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _playerCollider = GetComponentInParent<BoxCollider2D>();
        SpawnPlayer();
    }

    private void FixedUpdate()
    {
        CheckIsInDeadZone();
    }

    private void SpawnPlayer()
    {
        _rigidBody.transform.position = spawnPoint.transform.position;
    }

    private void CheckIsInDeadZone()
    {
        if (_playerCollider.IsTouchingLayers(killZone.value))
            SpawnPlayer();
    }
}
