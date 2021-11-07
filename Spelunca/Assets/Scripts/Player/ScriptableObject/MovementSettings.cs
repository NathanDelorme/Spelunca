using UnityEngine;

[CreateAssetMenu]
public class MovementSettings : ScriptableObject
{
    [Header("Movement variables")]
    public float maxAcceleration = 50f;
    public float maxSpeed = 10f;
    
    [Header("Jump variables")]
    public float jumpForce = 12f;
    public float maxJumpTime = 0.125f;
    public float maxCoyoteTime = 0.1f;

    [Header("Drag variables")]
    public float groundLinearDrag = 10f;
    public float airLinearDrag = 2.5f;

    public void Initialize()
    {
        maxAcceleration = 50f;
        maxSpeed = 10f;

        jumpForce = 12f;
        maxJumpTime = 0.125f;
        maxCoyoteTime = 0.1f;

        groundLinearDrag = 10f;
        airLinearDrag = 2.5f;
    }
}