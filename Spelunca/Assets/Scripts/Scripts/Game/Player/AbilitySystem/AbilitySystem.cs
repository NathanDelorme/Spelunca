using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAbility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetOrb"))
        {
            Debug.Log("Reset");
        }
        else if (collision.CompareTag("SpikeOrb"))
        {
            Debug.Log("Spike");
        }
    }
}
