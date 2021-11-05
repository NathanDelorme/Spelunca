using UnityEngine;

public class PlayerDeathWin : MonoBehaviour
{
    public LayerMask deadZone;
    private BoxCollider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponentInParent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckIsInDeadZone();
    }

    private void CheckIsInDeadZone()
    {
        if(playerCollider.IsTouchingLayers(deadZone.value))
        {
            Debug.Log("Mort");
        }
    }
}
