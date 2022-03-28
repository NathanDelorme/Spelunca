using Data;

namespace Ability
{
    /// <summary>
    /// Etat sans pouvoir.
    /// H�rite de la classe <see cref="State"/>.
    /// </summary>
    public class NoneState : State
    {
        /// <summary>
        /// Constructeur de la classe <see cref="NoneState"/>.
        /// </summary>
        /// <param name="newSystem">R�f�rence � l'<see cref="AbilitySystem"/> du joueur.</param>
        public NoneState(AbilitySystem newSystem) : base(newSystem) { }

        /// <summary>
        /// Fonction ex�cut� � chaque frame.
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Fonction ex�cut� un nombre d�terminer de fois par secondes.
        /// </summary>
        public override void FixedUpdate() { }

        /// <summary>
        /// Fonction execut� lorsque l'on change d'un �tat � celui-ci.
        /// </summary>
        public override void EnterState()
        {
            system.playerState.AbilityType = PlayerState.Ability.NONE;
        }

        /// <summary>
        /// Fonction execut� lorsque l'on remplace cet �tat par un autre.
        /// </summary>
        public override void ExitState() { }
    }
}
