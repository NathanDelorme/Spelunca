using UnityEngine;

public class InputProvider : MonoBehaviour
{
    public MovementState movementState;

    private void Start()
    {
        movementState.Initialize();
    }

    private void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        movementState.verticalDir = Input.GetAxisRaw("Vertical");
        CheckRunning();
        CheckJumping();
    }

    public void CheckRunning()
    {
        movementState.run = Input.GetAxisRaw("Horizontal") != 0f;
        movementState.horizontalDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Horizontal") != 0f)
            movementState.facing = Input.GetAxisRaw("Horizontal");
    }

    public void CheckJumping()
    {
        if(Input.GetButtonDown("Jump"))
            movementState.jump = true;
    }
}
