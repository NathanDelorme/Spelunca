using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [Header("Run variables")]
    public float maxAcceleration;
    public float maxSpeed;
    public float groundLinearDrag;

    [Header("Jump variables")]
    public float jumpForce;
    public float airLinearDrag;
    public float fallMultiplier;
    public float lowFallMultiplier;
    public float downMultiplier;
    public float jumpTime;

    public void Initialize()
    {
        maxAcceleration = 40f;
        maxSpeed = 10f;
        groundLinearDrag = 10f;

        jumpForce = 12f;
        airLinearDrag = 2.5f;
        fallMultiplier = 8f;
        lowFallMultiplier = 5f;
        downMultiplier = 12f;
        jumpTime = 0.125f;
    }
}
