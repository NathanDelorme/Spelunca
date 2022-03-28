using Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Ability
{
    /// <summary>
    /// Etat du pouvoir Spike. Les piques deviennent le sol et le sol les piques.
    /// Hérite de la classe <see cref="State"/>.
    /// </summary>
    public class SpikeState : State
    {
        /// <summary>
        /// Constructeur de la classe <see cref="SpikeState"/>.
        /// </summary>
        /// <param name="newSystem">Référence à l'<see cref="AbilitySystem"/> du joueur.</param>
        public SpikeState(AbilitySystem newSystem) : base(newSystem) { }

        /// <summary>
        /// Fonction exécuté à chaque frame.
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Fonction exécuté un nombre déterminer de fois par secondes.
        /// </summary>
        public override void FixedUpdate()
        {
            if (system.tilemaps[0].color != system.killGroundColor)
            {
                Color color = Color.Lerp(system.tilemaps[0].color, system.killGroundColor, 0.2f);

                foreach (Tilemap t in system.tilemaps)
                    t.color = color;
            }
        }

        /// <summary>
        /// Fonction executé lorsque l'on change d'un état à celui-ci.
        /// </summary>
        public override void EnterState()
        {
            system.playerState.AbilityType = PlayerState.Ability.SPIKE;
            system.spikesCollider.isTrigger = false;
            system.navigationController.groundLayers = system.spikes;
            system.winDeathCondition.reverseSpikeZone = true;
        }

        /// <summary>
        /// Fonction executé lorsque l'on remplace cet état par un autre.
        /// </summary>
        public override void ExitState()
        {
            system.spikesCollider.isTrigger = true;
            system.navigationController.groundLayers = system.ground;
            system.winDeathCondition.reverseSpikeZone = false;
        }
    }
}
