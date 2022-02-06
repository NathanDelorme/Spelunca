using System.Collections;
using UnityEngine;

public class FallingPlateform : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public float timeToFall = 1f;
    public float timeToRespawn = 4f;
    private Vector2 _startPos;
    private bool _plateformIsTouch;
    public bool respawns = true;
    

    private void Start()
    {
        _startPos = transform.position;
        _plateformIsTouch = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && respawns && !_plateformIsTouch)
        {
            StartCoroutine(Fall(timeToFall));
            _plateformIsTouch = true;
        }
    }

    private IEnumerator Fall(float timeUntilFallDelay)
    {
        yield return new WaitForSeconds(timeUntilFallDelay);

        rb.isKinematic = false;
        StartCoroutine(Respawn(timeToRespawn));
    }
    
    private IEnumerator Respawn(float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);

        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        transform.position = _startPos;
        _plateformIsTouch = false;
    }
}