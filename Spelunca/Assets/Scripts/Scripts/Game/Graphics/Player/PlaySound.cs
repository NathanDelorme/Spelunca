using UnityEngine;

/// <summary>
/// Classe qui implémente StateMachineBehaviour.
/// Cela permet de joeur des sons lorsque l'on entre / sort / update l'animation joué par le joueur.
/// </summary>
public class PlaySound : StateMachineBehaviour
{
    /// <value>
    /// Nom du son à jouer
    /// </value>
    public string soundName;
    /// <value>
    /// Référence au sfxManager qui permet la gestion des effets sonores.
    /// </value>
    public SFXManager sfxManager;

    /// <summary>
    /// OnStateEnter est appelé quand on entre dans un nouvel état.
    /// </summary>
    /// <param name="animator">animator</param>
    /// <param name="stateInfo">informations sur l'état</param>
    /// <param name="layerIndex">layerIndex</param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sfxManager = FindObjectOfType<SFXManager>();

        if (soundName != "playerNone")
            sfxManager.PlaySound(soundName);
        else
            sfxManager.StopSound();
    }

    /// <summary>
    /// OnStateExit est appelé quand on quitte l'état courant.
    /// </summary>
    /// <param name="animator">animator</param>
    /// <param name="stateInfo">informations sur l'état</param>
    /// <param name="layerIndex">layerIndex</param>
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sfxManager != null && soundName != "playerDash" && soundName != "playerJump")
            sfxManager.StopSound();
    }
}
