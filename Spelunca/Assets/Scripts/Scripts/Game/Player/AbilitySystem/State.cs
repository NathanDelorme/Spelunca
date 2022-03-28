namespace Ability
{
    /// <summary>
    /// Classe permettant de d�finir les m�thodes et attribut d'un �tat du joueur pour l'<see cref="AbilitySystem"/>.
    /// </summary>
    public abstract class State
    {
        /// <value>
        /// R�f�rence vers l'<see cref="AbilitySystem"/> du joueur.
        /// </value>
        protected AbilitySystem system;

        /// <summary>
        /// Constructeur de la classe <see cref="State"/>.
        /// </summary>
        /// <param name="newSystem">R�f�rence � l'<see cref="AbilitySystem"/> du joueur.</param>
        public State(AbilitySystem newSystem)
        {
            system = newSystem;
        }

        /// <summary>
        /// Fonction ex�cut� � chaque frame.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Fonction ex�cut� un nombre d�terminer de fois par secondes.
        /// </summary>
        public virtual void FixedUpdate() { }

        /// <summary>
        /// Fonction execut� lorsque l'on change d'un �tat � celui-ci.
        /// </summary>
        public virtual void EnterState() { }

        /// <summary>
        /// Fonction execut� lorsque l'on remplace cet �tat par un autre.
        /// </summary>
        public virtual void ExitState() { }
    }
}
