using UnityEngine;

/// <summary>
///  This class is an ScriptableObject that contain all variables useful for the movements.
///  It's the settings of the player movement.
/// </summary>
[CreateAssetMenu]
public class MovementSettings : ScriptableObject
{
    [Header("Movement variables")]
    /// <value>
    /// Max acceleration of the player on the ground.
    /// </value>
    public float maxAcceleration = 50f;
    /// <value>
    /// Max speed of the player on the ground.
    /// </value>
    public float maxMoveSpeed = 10f;
    /// <value>
    /// Max speed of the player in the air.
    /// </value>
    public float maxSpeed = 30f;

    [Header("Jump variables")]
    /// <value>
    /// Jump force applied to the player when jumping.
    /// </value>
    public float jumpForce = 12f;
    /// <value>
    /// Wall jump force applied to the player when wall jumping.
    /// </value>
    public float wallJumpForce = 8f;
    /// <value>
    /// Max duration while the player can keep his jumping touch pressed to do high jump.
    /// </value>
    public float maxJumpTime = 0.125f;
    /// <value>
    /// Coyote time define the time during the player can jump before getting out of a 2D plateform.
    /// </value>
    public float maxCoyoteTime = 0.1f;

    [Header("Drag variables")]
    /// <value>
    /// Ground friction applied to the player when on the ground.
    /// </value>
    public float groundLinearDrag = 10f;
    /// <value>
    /// Air friction applied to the player when in air.
    /// </value>
    public float airLinearDrag = 2f;
    /// <value>
    /// Wall friction applied to the player when wall sliding.
    /// </value>
    public float wallLinearDrag = 20f;

    [Header("Dash variables")]
    /// <value>
    /// Dash force applied to the player when dashing.
    /// </value>
    public float dashForce = 25f;
    /// <value>
    /// Duration of the dash in second.
    /// </value>
    public float dashTime = 0.07f;

    /// <summary>
    /// Function which initialise the variabkes of this ScriptableObject.
    /// </summary>
    public void Initialize()
    {
        maxAcceleration = 50f;
        maxSpeed = 10f;
        maxSpeed = 30f;

        jumpForce = 12f;
        wallJumpForce = 8f;
        maxJumpTime = 0.125f;
        maxCoyoteTime = 0.1f;

        groundLinearDrag = 10f;
        airLinearDrag = 2f;
        wallLinearDrag = 20f;

        dashForce = 25f;
        dashTime = 0.07f;
    }
}