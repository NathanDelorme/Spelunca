using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject[] pos;
    [SerializeField] private GameObject platform;
    private int _currentPositionIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector2.Distance(pos[_currentPositionIndex].transform.position, platform.transform.position) < .1f)
        {
            _currentPositionIndex++;
            if (_currentPositionIndex >= pos.Length)
            {
                _currentPositionIndex = 0;
            }
        }
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, pos[_currentPositionIndex].transform.position, Time.deltaTime * speed);
    }

    public void Respawn()
    {
        _currentPositionIndex = 0;
        platform.transform.position = pos[_currentPositionIndex].transform.position;
    }
}

