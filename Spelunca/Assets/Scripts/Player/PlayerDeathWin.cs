using UnityEngine;

public class PlayerDeathWin : MonoBehaviour
{
    public LayerMask deadZone;
    public GameObject spawnPoint;
    private Rigidbody2D _rb;
    private BoxCollider2D _playerCollider;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _playerCollider = GetComponentInParent<BoxCollider2D>();
        SpawnPlayer();
    }

    private void FixedUpdate()
    {
        CheckIsInDeadZone();
    }

    private void SpawnPlayer()
    {
        _rb.transform.position = spawnPoint.transform.position;
    }

    private void CheckIsInDeadZone()
    {
        if(_playerCollider.IsTouchingLayers(deadZone.value))
            SpawnPlayer();
    }
}
