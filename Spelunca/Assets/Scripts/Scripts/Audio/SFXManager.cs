using UnityEngine;

/// <summary>
/// Permet la gestion des effets sonore durant le jeu.
/// </summary>
public class SFXManager : MonoBehaviour
{
    /// <value>
    /// Référence vers les fichiers audio des différents effets sonores.
    /// </value>
    public AudioClip playerRunSound, playerJumpSound, playerWallSlideSound, playerDashSound, playerDeathSound;
    /// <value>
    /// Référence vers la source permettant de diffuser de l'audio au joueur.
    /// </value>
    public AudioSource audioSource;
    /// <value>
    /// Stocke si le jeu est en pause ou non.
    /// </value>
    private bool isPaused = false;

    /// <summary>
    /// Fonction permettant de mettre en pause les effets sonores lorsque le joueur ouvre le menu pause.
    /// </summary>
    public void flipFlopPause()
    {
        if(isPaused)
            audioSource.UnPause();
        else
            audioSource.Pause();

        isPaused = !isPaused;
    }

    /// <summary>
    /// Fonction qui arrête totalement les sons émis par l'attribut audioSource.
    /// </summary>
    public void StopSound()
    {
        if(audioSource != null)
            audioSource.Stop();
    }

    /// <summary>
    /// Fonction qui permet de jouer les différents effets sonores produits par le joueur.
    /// Cependant, il y a une limitation à ce système basique puisque l'audioSource ne peut pas jouer 2 sons au parallèle.
    /// Ainsi, si le joueur saute et meurt en même temps, le son du saut sera coupé par le son de mort.
    /// </summary>
    /// <param name="sound">Nom de l'effet sonore à jouer</param>
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
                break;
            case "playerWallSlide":
                audioSource.pitch = 1f;
                audioSource.clip = playerWallSlideSound;
                break;
            case "playerDash":
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(playerDashSound);
                break;
            case "playerDeath":
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(playerDeathSound);
                break;
        }
        audioSource.Play();
    }
}
