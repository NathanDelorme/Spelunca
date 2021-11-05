using UnityEngine;

[CreateAssetMenu]
public class MovementState : ScriptableObject
{
    [Header("Movement variables")]
    public bool run;
    public float horizontalDir;
    public float verticalDir;
    public float facing;

    public bool jump;

    public void Initialize()
    {
        run = false;
        horizontalDir = 1f;
        verticalDir = 0f;
        facing = 1f;

        jump = false;
    }
}
