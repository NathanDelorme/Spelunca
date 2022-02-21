using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    private float timer = 0.4f;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        sprite.sprite = playerObject.GetComponent<SpriteRenderer>().sprite;
        sprite.color = new Vector4(50, 50, 50, 0.2f);
        transform.localPosition = playerObject.transform.localPosition;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        sprite.color = Color.Lerp(sprite.color, new Vector4(50, 50, 50, 0f), 0.4f);

        if (timer <= 0f)
            Destroy(gameObject);
    }
}
