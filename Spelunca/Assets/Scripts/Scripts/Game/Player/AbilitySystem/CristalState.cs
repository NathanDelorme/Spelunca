using Data;
using UnityEngine;

namespace Ability
{
    /// <summary>
    /// Etat du pouvoir de cristal.
    /// Fait apparaître les plateformes de cristal.
    /// Hérite de la classe <see cref="State"/>.
    /// </summary>
    public class CristalState : State
    {
        /// <summary>
        /// Constructeur de la classe <see cref="CristalState"/>.
        /// </summary>
        /// <param name="newSystem">Référence à l'<see cref="AbilitySystem"/> du joueur.</param>
        public CristalState(AbilitySystem newSystem) : base(newSystem) { }

        /// <summary>
        /// Fonction exécuté à chaque frame.
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Fonction exécuté un nombre déterminer de fois par secondes.
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
        /// Fonction executé lorsque l'on change d'un état à celui-ci.
        /// </summary>
        public override void EnterState()
        {
            system.playerState.AbilityType = PlayerState.Ability.CRYSTAL;
            system.cristalTmCollider.isTrigger = false;
            system.goGround_Cristal.layer = 6;
        }

        /// <summary>
        /// Fonction executé lorsque l'on remplace cet état par un autre.
        /// </summary>
        public override void ExitState()
        {
            system.cristalTmCollider.isTrigger = true;
            system.goGround_Cristal.layer = 0;
        }
    }
}