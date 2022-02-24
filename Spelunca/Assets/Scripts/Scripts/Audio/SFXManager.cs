using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip playerRunSound, playerJumpSound, playerWallSlideSound, playerDashSound, playerDeathSound;
    public AudioSource audioSource;
    private bool isPaused = false;

    public void flipFlopPause()
    {
        if(isPaused)
        {
            isPaused = false;
            audioSource.UnPause();
        }
        else
        {
            isPaused = true;
            audioSource.Pause();
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "playerRun":
                if (!audioSource.isPlaying)
                {
                    audioSource.pitch = 2f;
                    audioSource.clip = playerRunSound;
                } 
                break;
            case "playerJump":
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(playerJumpSound);
                //audioSource.clip = playerJumpSound;
                break;
            case "playerWallSlide":
                audioSource.pitch = 1f;
                audioSource.clip = playerWallSlideSound;
                break;
            case "playerDash":
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(playerDashSound);
                //audioSource.clip = playerDashSound;
                break;
            case "playerDeath":
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(playerDeathSound);
                //audioSource.clip = playerDeathSound;
                break;
        }
        audioSource.Play();
    }
}
