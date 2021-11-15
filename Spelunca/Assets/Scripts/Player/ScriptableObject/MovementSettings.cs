using UnityEngine;

[CreateAssetMenu]
public class MovementSettings : ScriptableObject
{
    [Header("Movement variables")]
    public float maxAcceleration = 50f;
    public float maxMoveSpeed = 10f;
    public float maxSpeed = 30f;

    [Header("Jump variables")]
    public float jumpForce = 12f;
    public float maxJumpTime = 0.125f;
    public float maxCoyoteTime = 0.1f;

    [Header("Drag variables")]
    public float groundLinearDrag = 10f;
    public float airLinearDrag = 2f;

    [Header("Dash variables")]
    public float dashForce = 25f;
    public float dashTime = 0.07f;

    public void Initialize()
    {
        maxAcceleration = 50f;
        maxSpeed = 10f;
        maxSpeed = 30f;

        jumpForce = 12f;
        maxJumpTime = 0.125f;
        maxCoyoteTime = 0.1f;

        groundLinearDrag = 10f;
        airLinearDrag = 2f;

        dashForce = 25f;
        dashTime = 0.07f;
    }
}