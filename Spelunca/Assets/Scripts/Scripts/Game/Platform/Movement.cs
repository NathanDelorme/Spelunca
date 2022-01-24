using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject[] pos;
    [SerializeField] private GameObject platform;
    private int currentPositionIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector2.Distance(pos[currentPositionIndex].transform.position, platform.transform.position) < .1f)
        {
            currentPositionIndex++;
            if (currentPositionIndex >= pos.Length)
            {
                currentPositionIndex = 0;
            }
        }
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, pos[currentPositionIndex].transform.position, Time.deltaTime * speed);
    }

}

