using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class is a script apply to different background layers to create a depth sensation.
/// </summary>
public class Parallax : MonoBehaviour
{
    public float parallaxEffect;

    public Transform cameraTransform;
    private Vector3 lastCamPos;

    /// <summary>
    /// Function executed at the start of the program.
    /// We initialize the variables used in the FixedUpdate.
    /// </summary>
    void Start()
    {
        lastCamPos = cameraTransform.position;
    }

    /// <summary>
    /// Function executed a fixed times per second.
    /// Each fixed frame we update the position of the current GameObject to make the depth sensation.
    /// </summary>
    void LateUpdate()
    {
        Vector3 movement = new Vector3(cameraTransform.position.x - lastCamPos.x, 0f);
        transform.position = new Vector3(transform.position.x + movement.x * parallaxEffect, cameraTransform.position.y);
        lastCamPos = cameraTransform.position;
    }
}
