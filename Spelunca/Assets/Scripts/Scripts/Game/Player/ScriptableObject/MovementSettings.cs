using UnityEngine;

namespace Data
{
    /// <summary>
    /// Cette classe est un ScriptableObject qui contient toutes les variables utiles pour le mouvement.
    /// Il s'agit de valeurs "constantes".
    /// </summary>
    [CreateAssetMenu]
    public class MovementSettings : ScriptableObject
    {
        [Header("Movement variables")]
        /// <value>
        /// Accélération maximum du joueur sur le sol.
        /// </value>
        public float maxAcceleration = 50f;
        /// <value>
        /// Vitesse maximum du joueur sur le sol.
        /// </value>
        public float maxMoveSpeed = 10f;
        /// <value>
        /// Vitesse maximum du joueur dans l'air.
        /// </value>
        public float maxSpeed = 30f;

        [Header("Jump variables")]
        /// <value>
        /// Force appliqué sur le joueur lors du saut.
        /// </value>
        public float jumpForce = 12f;
        /// <value>
        /// Force appliqué sur le joueur lors du wall jump.
        /// </value>
        public float wallJumpForce = 8f;
        /// <value>
        /// Durée maximum durant laquelle le joueur peut rester appuyer sur sa touche de saut pour effectuer un saut plus haut.
        /// </value>
        public float maxJumpTime = 0.125f;
        /// <value>
        /// Le coyote time défini le temps durant lequel le joueur peut encore sauté après avoir quitté une plateforme.
        /// </value>
        public float maxCoyoteTime = 0.1f;

        [Header("Drag variables")]
        /// <value>
        /// Friction du sol appliquée sur le joueur lorsqu'il est sur le sol.
        /// </value>
        public float groundLinearDrag = 20f;
        /// <value>
        /// Friction de l'ai appliquée sur le joueur lorsqu'il est en l'air.
        /// </value>
        public float airLinearDrag = 2f;
        /// <value>
        /// Friction murale appliquée sur le joueur lorsqu'il est en train de wall slide.
        /// </value>
        public float wallLinearDrag = 20f;

        [Header("Dash variables")]
        /// <value>
        /// Force appliqué au joueur lors du dash ?
        /// </value>
        public float dashForce = 25f;
        /// <value>
        /// Durée du dash en seconde.
        /// </value>
        public float dashTime = 0.07f;

        /// <summary>
        /// Fonction qui initialise les variables de ce ScriptableObject.
        /// </summary>
        public void Initialize()
        {
            maxAcceleration = 50f;
            maxSpeed = 10f;
            maxSpeed = 30f;

            jumpForce = 12f;
            wallJumpForce = 8f;
            maxJumpTime = 0.125f;
            maxCoyoteTime = 0.1f;

            groundLinearDrag = 20f;
            airLinearDrag = 2f;
            wallLinearDrag = 20f;

            dashForce = 25f;
            dashTime = 0.07f;
        }
    }
}