using System.Collections;
using UnityEngine;

public class FallingPlateform : MonoBehaviour
{
    public Rigidbody2D rb;
    public float timeToFall = 1f;
    public float timeToRespawn = 4f;
    private Vector2 _startPos;
    private bool _plateformIsTouch;
    public bool respawns = true;
    

    private void Start()
    {
        _startPos = gameObject.transform.position;
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
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, -5);
        StartCoroutine(Respawn(timeToRespawn));
    }
    
    private IEnumerator Respawn(float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);

        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        Debug.Log(_startPos);
        transform.position = _startPos;
        Debug.Log(transform.position);
        _plateformIsTouch = false;
    }

    public void InstantRespawn()
    {
        StopAllCoroutines();
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        this.transform.position = _startPos;
        _plateformIsTouch = false;
    }
}