using Data;
using UnityEngine;

namespace Ability
{
    /// <summary>
    /// Etat du pouvoir de cristal.
    /// Fait appara�tre les plateformes de cristal.
    /// H�rite de la classe <see cref="State"/>.
    /// </summary>
    public class CristalState : State
    {
        /// <summary>
        /// Constructeur de la classe <see cref="CristalState"/>.
        /// </summary>
        /// <param name="newSystem">R�f�rence � l'<see cref="AbilitySystem"/> du joueur.</param>
        public CristalState(AbilitySystem newSystem) : base(newSystem) { }

        /// <summary>
        /// Fonction ex�cut� � chaque frame.
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Fonction ex�cut� un nombre d�terminer de fois par secondes.
        /// </summary>
        public override void FixedUpdate()
        {
            if (system.cristalTilemap.color != system.cristalUnable)
            {
                Color color = Color.Lerp(system.cristalTilemap.color, system.cristalUnable, 0.2f);
                system.cristalTilemap.color = color;
            }
        }

        /// <summary>
        /// Fonction execut� lorsque l'on change d'un �tat � celui-ci.
        /// </summary>
        public override void EnterState()
        {
            system.playerState.AbilityType = PlayerState.Ability.CRYSTAL;
            system.cristalTmCollider.isTrigger = false;
            system.goGround_Cristal.layer = 6;
        }

        /// <summary>
        /// Fonction execut� lorsque l'on remplace cet �tat par un autre.
        /// </summary>
        public override void ExitState()
        {
            system.cristalTmCollider.isTrigger = true;
            system.goGround_Cristal.layer = 0;
        }
    }
}