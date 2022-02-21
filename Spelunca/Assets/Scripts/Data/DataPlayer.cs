using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : MonoBehaviour
{
    [SerializeField] private int jumpNumber = 0;
    [SerializeField] private int dashNumber = 0;
    [SerializeField] private int wallJumpNumber = 0;

    private bool isJumpingOld = false;
    private bool isDashingOld = false;
    private bool IsWallJumpingOld = false;


    public PlayerController playerControler;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("JumpNumberData"))
        {
            jumpNumber = PlayerPrefs.GetInt("JumpNumberData");
        }

        if (PlayerPrefs.HasKey("DashNumberData"))
        {
            dashNumber = PlayerPrefs.GetInt("DashNumberData");
        }

        if (PlayerPrefs.HasKey("wallJumpNumberData"))
        {
            wallJumpNumber = PlayerPrefs.GetInt("wallJumpNumberData");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.DeleteAll();*/
        
        if ( isJumpingOld == false && playerControler.playerState.isJumping == true )
        {
            isJumpingOld = true;
            jumpNumber++;
            PlayerPrefs.SetInt("JumpNumberData", jumpNumber);
        }
        if(playerControler.playerState.isJumping == false)
            isJumpingOld = false;

        if ( isDashingOld == false && playerControler.playerState.isDashing == true )
        {
            isDashingOld = true;
            dashNumber++;
            PlayerPrefs.SetInt("DashNumberData", dashNumber);
        }
        if ( playerControler.playerState.isDashing == false)
            isDashingOld = false;

        if ( IsWallJumpingOld == false && playerControler.playerState.isWallJumping == true )
        {
            IsWallJumpingOld = true;
            wallJumpNumber++;
            PlayerPrefs.SetInt("wallJumpNumberData", wallJumpNumber);
        }
        if (playerControler.playerState.isWallJumping == false)
            IsWallJumpingOld = false;

        
    }
}
