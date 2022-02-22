using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DataPlayer : MonoBehaviour
{
    private bool isKilledOld = false;
    private bool isJumpingOld = false;
    private bool IsWallJumpingOld = false;

    public int deathTotalNb = 0;
    public int jumpTotalNb = 0;

    public PlayerState playerState;
    public WinDeathCondition winDeathCondition;
    private int levelID = -1;

    void Start()
    {
        levelID = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));

        deathTotalNb = PlayerPrefs.GetInt("LEVEL_DEATHS" + levelID);
        jumpTotalNb = PlayerPrefs.GetInt("LEVEL_JUMP" + levelID);
    }

    void Update()
    {
        if(isKilledOld == false && winDeathCondition.isKilled)
        {
            isKilledOld = true;
            deathTotalNb++;
            PlayerPrefs.SetInt("LEVEL_DEATHS" + levelID, deathTotalNb);
        }
        if (winDeathCondition.isKilled == false)
            isKilledOld = false;

        if (isJumpingOld == false && playerState.isJumping == true)
        {
            isJumpingOld = true;
            jumpTotalNb++;
            PlayerPrefs.SetInt("LEVEL_JUMP" + levelID, jumpTotalNb);
        }
        if (playerState.isJumping == false)
            isJumpingOld = false;

        if (IsWallJumpingOld == false && playerState.isWallJumping == true)
        {
            IsWallJumpingOld = true;
            jumpTotalNb++;
            PlayerPrefs.SetInt("LEVEL_JUMP" + levelID, jumpTotalNb);
        }
        if (playerState.isWallJumping == false)
            IsWallJumpingOld = false;
    }
}
